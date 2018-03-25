using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monitor_Computer
{
    /// <summary>
    /// Interaction logic for connectPage1.xaml
    /// </summary>
    public partial class connectPage1 : Page
    {
        public MainWindow mainWindowClass;
        public connectPage1(MainWindow mainWindowClass, bool didItConnect, bool justTheChat)
        {
            InitializeComponent();
            localIpAdresLabel.Content += GetLocalIPAddress();
                if (mainWindowClass.firstTimeDeclarations)
                {
                    Thread setExternalIp = new Thread(GetExternalAdress);
                    setExternalIp.Start();
                }
                else if (mainWindowClass.myIpAdress != null)
                {
                    ipAdressLabel.Content = "Public ip adress : " + mainWindowClass.myIpAdress;
                }
                this.mainWindowClass = mainWindowClass;
            if (!justTheChat)
            {
                if (didItConnect)
                {
                    connectButton.Content = "Disconnect";
                    serverButton.IsEnabled = false;
                }
                else
                {
                    connectButton.Content = "Connect";
                    serverButton.IsEnabled = true;
                }
                if (mainWindowClass.serverBootUp)
                {
                    serverButton.Content = "Stop Server";
                    connectButton.IsEnabled = false;
                    serverPortLabel.Content = "Server port : " + mainWindowClass.serverPort.ToString();
                }
                else
                    stopBooting.IsEnabled = false;
            }
            else
            {
                serverButton.Content = "Stop Server";
                connectButton.IsEnabled = false;
                serverPortLabel.Content = "Server port : " + mainWindowClass.serverPort.ToString();
                if (!mainWindowClass.serverBootUp)
                    stopBooting.IsEnabled = false;
            }
            if (mainWindowClass.chatAvalible)
                chatButton.IsEnabled = true;
            else
                chatButton.IsEnabled = false;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private void GetExternalAdress()
        {
            try
            {
                string url = "http://checkip.dyndns.org";
                System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string response = sr.ReadToEnd().Trim();
                string[] a = response.Split(':');
                string a2 = a[1].Substring(1);
                string[] a3 = a2.Split('<');
                string a4 = a3[0];
                mainWindowClass.myIpAdress = a4;
                mainWindowClass.firstTimeDeclarations = false;
                try {
                    this.Dispatcher.Invoke(new Action(() => ipAdressLabel.Content = "Public ip adress : " + a4));
                }
                catch (TaskCanceledException) { }
            }
            catch (Exception)
            {
                try {
                    this.Dispatcher.Invoke(new Action(() => ipAdressLabel.Content = "Public ip adress : not found :("));
                }
                catch (TaskCanceledException) { }
            }
       }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            if (connectButton.Content.ToString() == "Connect")
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(350));
                ThicknessAnimation animation = new ThicknessAnimation();
                Thickness marginFrom = new Thickness();
                marginFrom.Left = 30;
                Thickness marginTo = new Thickness();
                marginTo.Left = 0;
                animation.From = marginFrom;
                animation.To = marginTo;
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(350));
                connectPage2 connect2 = new connectPage2(mainWindowClass);
                connect2.BeginAnimation(Page.OpacityProperty, da);
                connect2.BeginAnimation(Page.MarginProperty, animation);
                this.NavigationService.Navigate(connect2);
            }
            else
            {
                mainWindowClass.stopClient();
                chatButton.IsEnabled = false;
            }
            //this.NavigationService.Navigate(new Uri("connectPage2.xaml", UriKind.Relative));
        }

        private async void serverButton_Click(object sender, RoutedEventArgs e)
        {
            if (serverButton.Content.ToString() == "Server")
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromMilliseconds(350));
                ThicknessAnimation animation = new ThicknessAnimation();
                Thickness marginFrom = new Thickness();
                marginFrom.Left = 30;
                Thickness marginTo = new Thickness();
                marginTo.Left = 0;
                animation.From = marginFrom;
                animation.To = marginTo;
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(350));
                serverPage server1 = new serverPage(mainWindowClass);
                this.NavigationService.Navigate(server1);
                server1.BeginAnimation(Page.MarginProperty, animation);
                server1.BeginAnimation(Page.OpacityProperty, da);
            }
            else
            {
                if (!mainWindowClass.canCloseTheApplication)
                    await mainWindowClass.passwordChecker();
                if (mainWindowClass.canCloseTheApplication)
                {
                    mainWindowClass.serverBootUp = false;
                    string startUpLocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "MonitorComputer.url");
                    File.Delete(startUpLocation);
                    mainWindowClass.instaSaveSettings();
                    serverPortLabel.Content = "Server port : -";
                    mainWindowClass.keepServerOnAfterClient = false;
                    mainWindowClass.stopServer();
                    serverButton.Content = "Server";
                    connectButton.IsEnabled = true;
                    chatButton.IsEnabled = false;
                }
            }
        }

        private void setFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            mainWindowClass.saveFolder = dialog.SelectedPath.ToString();
            mainWindowClass.instaSaveSettings();
        }

        private async void stopBooting_Click(object sender, RoutedEventArgs e)
        {
            await mainWindowClass.passwordChecker();
            if (mainWindowClass.canCloseTheApplication)
            {
                mainWindowClass.serverBootUp = false;
                stopBooting.IsEnabled = false;
                string startUpLocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "MonitorComputer.url");
                File.Delete(startUpLocation);
                if (mainWindowClass.serverStatus)
                    mainWindowClass.canCloseTheApplication = false;
                mainWindowClass.instaSaveSettings();
            }
        }

        private async void helpButtonConnect1_Click(object sender, RoutedEventArgs e)
        {
            await mainWindowClass.ShowMessageAsync("What does these buttons do ?", "Set folder - it will set your saving folder (where the screenshots and files will be saved)\nServer (Stop server) - it will open the server menu / stop the server\nConnect (Disconnect) - It will open the connection menu / disconnect from the host\nStop startup booting - if you can click it, it means that the application will start at windows boot and you can stop this by clicking it");
        }

        private void chatButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindowClass.chatButtonClick("",false);
        }
    }
}
