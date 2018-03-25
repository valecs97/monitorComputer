using MahApps.Metro.Controls.Dialogs;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Monitor_Computer
{
    /// <summary>
    /// Interaction logic for serverPage.xaml
    /// </summary>
    public partial class serverPage : Page
    {
        private MainWindow mainWindowClass;
        public serverPage(MainWindow mainWindowClass)
        {
            InitializeComponent();
            this.mainWindowClass = mainWindowClass;
        }

        private void portTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int iValue = -1;

            if (Int32.TryParse(textBox.Text, out iValue) == false)
            {
                TextChange textChange = e.Changes.ElementAt<TextChange>(0);
                int iAddedLength = textChange.AddedLength;
                int iOffset = textChange.Offset;

                textBox.Text = textBox.Text.Remove(iOffset, iAddedLength);
            }
            try
            {
                if (Convert.ToInt32(portTextBox.Text) <= 0 || Convert.ToInt32(portTextBox.Text) >= 65535)
                {
                    portTextBox.Text = "1";
                }
            }
            catch (Exception) { }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
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
            connectPage1 connect1 = new connectPage1(mainWindowClass, false,false);
            this.NavigationService.Navigate(connect1);
            connect1.BeginAnimation(Page.OpacityProperty, da);
            connect1.BeginAnimation(Page.MarginProperty, animation);
        }

        private void startServerButton_Click(object sender, RoutedEventArgs e)
        {
            if (portTextBox.Text != "")
            {
                mainWindowClass.serverPort = Int32.Parse(portTextBox.Text);
                mainWindowClass.serverPassword = passwordTextBox.Password;
                if (mainWindowClass.serverPassword == "")
                    mainWindowClass.serverPassword = "D3f@u1t p@55w0rd %!0";
                bool serverStatus = mainWindowClass.startServer();
                connectPage1 connect1 = new connectPage1(mainWindowClass,false,false);
                if (serverStatus)
                {
                    connect1.serverButton.Content = "Stop Server";
                    connect1.serverPortLabel.Content = "Server port : " + mainWindowClass.serverPort.ToString();
                    connect1.connectButton.IsEnabled = false;
                    if (mainWindowClass.serverBootUp)
                    {
                        mainWindowClass.keepServerOnAfterClient = true;
                        string appLocation = System.Reflection.Assembly.GetEntryAssembly().Location;
                        string startUpLocation = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                        using (StreamWriter writer = new StreamWriter(startUpLocation + "\\" + "MonitorComputer" + ".url"))
                        {
                            writer.WriteLine("[InternetShortcut]");
                            writer.WriteLine("URL=file:///" + appLocation);
                            writer.WriteLine("IconIndex=0");
                            string icon = appLocation.Replace('\\', '/');
                            writer.WriteLine("IconFile=" + icon);
                            writer.Flush();
                        }
                        mainWindowClass.instaSaveSettings();
                    }
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
                    connect1.BeginAnimation(Page.OpacityProperty, da);
                    connect1.BeginAnimation(Page.MarginProperty, animation);
                    this.NavigationService.Navigate(connect1);

                }
            }
            else
                fieldNotComplete();
        }
        private async void fieldNotComplete()
        {
            await mainWindowClass.ShowMessageAsync("Error", "Some fields are empty");
        }

        private void keepServerOnCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            mainWindowClass.keepServerOnAfterClient = true;
        }

        private void keepServerOnCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            mainWindowClass.keepServerOnAfterClient = false;
        }

        private async void bootUpCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            mainWindowClass.serverBootUp = true;
            await mainWindowClass.ShowMessageAsync("Warning", "Be careful, if this computer has a password then don't use the restart function !");
        }

        private void bootUpCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            mainWindowClass.serverBootUp = false;
            string startUpLocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup),"MonitorComputer.url");
            File.Delete(startUpLocation);
        }

        private async void helpButtonServer_Click(object sender, RoutedEventArgs e)
        {
            await mainWindowClass.ShowMessageAsync("What do I do here ?", "Everything is self explaining , you choose a port and a password (or no) and you create a server !");
        }
    }
}
