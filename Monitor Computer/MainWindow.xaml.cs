using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading;
using System.Windows.Threading;
using System.Xml;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml;
using System.IO;
using Monitor_Computer.Properties;
using System.Resources;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Hardcodet.Wpf.TaskbarNotification;
using System.Net.Mail;
using System.Text;
using System.Reflection;
using System.Drawing;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Linq;

namespace Monitor_Computer
{
    /*
    Legend

    When sending a command be sure to have EXACLY 6 letters , else it wont work

    strmnt = start monitoring
    stpmnt = stop monitoring

    */
    public partial class MainWindow
    {
        private NetworkingClient networkingClientClass;
        private NetworkingServer networkingServerClass;
        private encryption encryptionClass;
        public string[] settings;
        public string homeIp, homePort, homePassword, homeSet = "false";
        public string serverPassword;
        public string saveFolder;
        public int serverPort;
        public bool serverBootUp, keepServerOnAfterClient, didItConnect, serverStatus = false;
        public string ipToConnect, passwordToConnect, myIpAdress;
        public int portToConnect;
        public System.Windows.Controls.Button addButton;
        public System.Windows.Controls.Button[] closeButton = new System.Windows.Controls.Button[5];
        public int[] buttonOrder = new int[5] { -1, -1, -1, -1, -1 };
        public int numberCloseButtons = -1, buttonRealNumber = 0, buttonFakeNumber = 0;
        public bool canCloseTheApplication = true, firstTimeDeclarations = true;
        public bool chatOpen = false, chatAvalible = false;
        sendFeedback sendFeedbackWindow;
        public Chat chatWindow;
        connectPage1 connectPage1Class;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                ((Control)this.taskManagerTab).IsEnabled = false;
                ((Control)this.commandsTab).IsEnabled = false;
                encryptionClass = new encryption(this);
                networkingClientClass = new NetworkingClient(this, encryptionClass);
                networkingServerClass = new NetworkingServer(this, encryptionClass);
                string[] settings = System.IO.File.ReadAllLines(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "computerMonitorSettings.txt"));
                foreach (string line in settings)
                {
                    string stringSave = line.Substring(0, line.IndexOf("="));

                    switch (stringSave)
                    {
                        case "home_ip":
                            homeIp = line.Substring(line.IndexOf("=") + 1); break;
                        case "home_port":
                            homePort = line.Substring(line.IndexOf("=") + 1); break;
                        case "home_password":
                            homePassword = line.Substring(line.IndexOf("=") + 1); break;
                        case "home_set":
                            homeSet = line.Substring(line.IndexOf("=") + 1); break;
                        case "last_ip":
                            ipToConnect = line.Substring(line.IndexOf("=") + 1); break;
                        case "last_port":
                            portToConnect = Int32.Parse(line.Substring(line.IndexOf("=") + 1)); break;
                        case "last_password":
                            passwordToConnect = line.Substring(line.IndexOf("=") + 1); break;
                        case "saveFolder":
                            saveFolder = line.Substring(line.IndexOf("=") + 1); break;
                        case "serverPort":
                            serverPort = Int32.Parse(line.Substring(line.IndexOf("=") + 1)); break;
                        case "serverPassword":
                            serverPassword = line.Substring(line.IndexOf("=") + 1); break;
                        case "startup":
                            {
                                string aux = line.Substring(line.IndexOf("=") + 1);
                                if (aux == "True")
                                    serverBootUp = true;
                                else
                                    serverBootUp = false;
                            }; break;
                    }
                }
            }
            catch (Exception)
            {
                Thread newUserMessage = new Thread(newUser);
                newUserMessage.Start();
            }
            if (serverBootUp == true)
            {
                keepServerOnAfterClient = true;
                startServer();
            }
            connectPage1Class = new connectPage1(this, false,false);
            connectWindow.Navigate(connectPage1Class);
        }


        private void newUser()
        {
            System.Threading.Thread.Sleep(1000);
            this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Welcome !!!", "It seems that you are new here , so please check the help section first :)")));
            saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            instaSaveSettings();
        }
        public void OnExitCommands()
        {
            notificationIcon.Dispose();
            keepServerOnAfterClient = false;
            instaSaveSettings();
            if (statusControl.Content.ToString() != "Status : offline" && !didItConnect)
                stopServer();
            if (statusControl.Content.ToString() != "Status : offline" && didItConnect)
                stopClient();
        }

        public void instaSaveSettings()
        {
            string[] settings = { "home_ip=" + homeIp, "home_port=" + homePort, "home_password=" + homePassword, "home_set=" + homeSet, "last_ip=" + ipToConnect, "last_port=" + portToConnect.ToString(), "last_password=" + passwordToConnect, "saveFolder=" + saveFolder, "serverPort=" + serverPort.ToString(), "serverPassword=" + serverPassword, "startup=" + serverBootUp.ToString() };
            System.IO.File.WriteAllLines(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "computerMonitorSettings.txt"), settings);
        }

      
        public string screenshot()
        {
            Bitmap Screenshot;
            Screenshot = new System.Drawing.Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(Screenshot))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screenshot.Size);
            }
            string fileName = (System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "screenShot.bmp"));
            System.IO.FileStream fs = System.IO.File.Open(fileName, System.IO.FileMode.OpenOrCreate);
            Screenshot.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
            string s = Screenshot.ToString();
            fs.Close();
            return fileName;
        }

        public bool startServer()
        {
            bool portGood=false;
            portGood = networkingServerClass.startPort();
            if (portGood)
            {
                canCloseTheApplication = false;
                Thread startServerThread = new Thread(networkingServerClass.openServer);
                startServerThread.Start();
            }
            return portGood;
        }

        public void restoreConnectPage1Default()
        {
            try {
                Dispatcher.Invoke(() => connectWindow.Navigate(new connectPage1(this, false,false)));
                Dispatcher.Invoke(new Action(() => statusControl.Content = "Status : offline"));
            }
            catch (TaskCanceledException) { };
            
        }

        public void activateChat()
        {
            try
            {
                Dispatcher.Invoke(() => connectWindow.Navigate(new connectPage1(this, false, true)));
            }
            catch (TaskCanceledException) { };

        }

        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Minimized:
                    this.Hide();
                    break;
            }
    }

        public void stopServer()
        {
            Thread stopServerThread = new Thread(this.stopServerThread);
            stopServerThread.Start();
        }

        public void stopServerThread()
        {
            if (networkingServerClass.checkCpuRam())
            {
                networkingServerClass.forceCloseCpuRam();
            }
            networkingServerClass.closeServer();
        }

        private async void aboutInfo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
                await this.ShowMessageAsync("About", "Monitor Computer (build 1)" + "\n" + "Copyright @2016 by Vitoc Alecsandru" + "\n" + "My e-mail : nightfuryva@gmail.com");
        }

        private void sendFeedback_click(object sender, System.Windows.RoutedEventArgs e)
        {
            sendFeedbackWindow = new sendFeedback(this);
            sendFeedbackWindow.Show();
        }

        public void chatButtonClick(string command, bool fromClient)
        {
            if (chatAvalible)
            {
                chatWindow = new Chat(this, command, fromClient);
                chatWindow.Show();
            }
        }

        public void messageToServer(string message)
        {
            networkingClientClass.sendCommand("messag" + message, "c0mm@nds t0 s3nd %!3");
        }

        public void messageToClient(string message)
        {
            
            networkingServerClass.sendCommand("messag" + message, "c0mm@nds t0 r3c3iv3 %!4");
        }

        public async void succesSend()
        {
            await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Succes", "Thank you , for your feedback :)")));
        }

        public async void unsuccesSend()
        {
            await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Failed", "Sorry , it seems that we failed to send the e-mail. Please check your internet connection and try again !!")));
        }

        public async void failedToDecryptAfterPassClient()
        {
            await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Error", "It seems that you have conencted to someone server or we encountered an error. Anyway , you have been disconnected")));
        }

        public async void portIsNotGood()
        {
            await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Error", "The port is already in use . Please choose another one !")));
        }

        public async void failedToDecryptAtPassCheckServer()
        {
            await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Error", "It seems that someone malware tried to connect to your computer. Don't worry now , it wont cause any problem from now on ! P.S Till you close the application :(")));
        }

        public async void incorectPassword()
        {
            await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Password Incorrect", "Please check your password !" + networkingClientClass.attempts)));
        }


        private async void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!canCloseTheApplication)
            {
                e.Cancel = true;
                await passwordChecker();
                if (canCloseTheApplication)
                    Close();
            }
            else
            {
                if (sendFeedbackWindow != null)
                    sendFeedbackWindow.Close();
                if (chatWindow != null)
                    chatWindow.Close();
                OnExitCommands();
            }
        }

        public async Task passwordChecker()
        {
            LoginDialogData data;
            LoginDialogSettings settings = new LoginDialogSettings();
            settings.ShouldHideUsername = true;
            settings.EnablePasswordPreview = true;
            settings.AffirmativeButtonText = "Enter";
            data = await this.ShowLoginAsync("Warning", "Please enter the server password : ", settings);
            if (data != null)
            {
                if (data.Password == serverPassword)
                    canCloseTheApplication = true;
                else if (data.Password == "" && serverPassword == "D3f@u1t p@55w0rd %!0")
                    canCloseTheApplication = true;
            }
            if (!canCloseTheApplication)
                incorectPassword();
        }

        private void screenShotButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            networkingClientClass.stopMonitoring();
            networkingClientClass.sendCommand("scrsht", "c0mm@nds t0 s3nd %!3");
        }

        private void setFolder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.ShowDialog();
            saveFolder = dialog.SelectedPath.ToString();
            instaSaveSettings();
        }

        private void restartComputer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            networkingClientClass.sendCommand("restar", "c0mm@nds t0 s3nd %!3");
        }

        private async void helpButtonCommands_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowMessageAsync("What does these buttons do ?", "Shut down, restart, sleep - it will send to your home computer (server) the command to shut down, restart or sleep;\nSet folder - it will set your saving folder (where the screenshots and files will be saved)\nScreenshot - it will take a screenshot from your home computer\nSend file - it will send a file to your home computer");

        }

        private void sleepComputer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            networkingClientClass.sendCommand("sleepq", "c0mm@nds t0 s3nd %!3");
        }

        private async void atentionButtonCommands_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.ShowMessageAsync("Attention", "Do not turn off the application when the file/screenshot is transfering !!! You might need to force close the server in order to use the application again !");
        }

        private void sendFileButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result==true)
            {
                networkingClientClass.fileLocation = dlg.FileName.ToString();
                Thread sendFileThread = new Thread(startFileTransfering);
                sendFileThread.Start();
            }
        }

        private void startFileTransfering()
        {
            networkingClientClass.sendFile(1024);
        }

        private void shutDownComputer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            networkingClientClass.sendCommand("closec","c0mm@nds t0 s3nd %!3");
        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string file = screenshot();
            encryptionClass.EncryptFile(file);
            encryptionClass.DecryptFile(file);
        }

        int firstTimeWarning = 3;

        private void warning_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (firstTimeWarning == 0)
            {
                int wwarningCpu = 10, wwarningRAM = 10, wwarningTime = 10;
                wwarningCpu = (int)warningCPU.Value;
                wwarningRAM = (int)warningRAM.Value;
                wwarningTime = (int)warningTime.Value;
                networkingClientClass.sendCommand("strchg" + wwarningCpu.ToString() + ' ' + wwarningRAM.ToString() + ' ' + wwarningTime.ToString() + ' ', "c0mm@nds t0 s3nd %!3");
            }
            else
                firstTimeWarning--;
        }

        public void stopClient()
        {
            Thread stopClientThread = new Thread(this.stopClientThread);
            stopClientThread.Start();
        }

        private void stopClientThread()
        {
            chatAvalible = false;
            if (chatOpen)
                this.Dispatcher.Invoke(new Action(() => chatWindow.Close()));
            networkingClientClass.stopMonitoring();
            Thread stopClientThread = new Thread(networkingClientClass.closeClient);
            stopClientThread.Start();
            didItConnect = false;
        }

        public async void startClient()
        {
            int connectionCase = networkingClientClass.makeConnection();
            didItConnect = false;
            switch (connectionCase)
            {
                case -3: await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Max attemps reached", "It seems that you have reached the maximum amount of tries. Restart the server to try again !"))); break;
                case -2: await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Failed", "It seems that you have connected to an invalid server !"))); break;
                case -1: await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Password Incorrect", "Please check your password ! Attempts remained : " + networkingClientClass.attempts))); break;
                case 0:
                    {
                        await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Password Correct", "You are connected")));
                        Dispatcher.Invoke(() => statusControl.Content = "Status : online");
                        didItConnect = true;
                        taskManagerTab.Dispatcher.Invoke(new Action(() => taskManagerTab.IsEnabled = true));
                        commandsTab.Dispatcher.Invoke(new Action(() => commandsTab.IsEnabled = true));
                        shutDownComputer.Dispatcher.Invoke(new Action(() => shutDownComputer.IsEnabled = true));
                        restartComputer.Dispatcher.Invoke(new Action(() => restartComputer.IsEnabled = true));
                        screenShotButton.Dispatcher.Invoke(new Action(() => screenShotButton.IsEnabled = true));
                        sendFileButton.Dispatcher.Invoke(new Action(() => sendFileButton.IsEnabled = true));
                        chatAvalible = true;
                        Thread startThisShiterino = new Thread(networkingClientClass.startMonitoring);
                        startThisShiterino.Start();
                    }
                    break;
                case 1: await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Connection problem", "Connection inexistent"))); break;
                case 2: await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Connection problem", "That's not an IP adress"))); break;
                case 3: await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Connection problem", "Connection time out"))); break;
                case 4: await this.Dispatcher.BeginInvoke(new Action(async () => await this.ShowMessageAsync("Connection problem", "That's not an IP adress"))); break;
            }
            Dispatcher.Invoke(new Action(() => {
                connectPage1 connect1 = new connectPage1(this, didItConnect,false);
                connectWindow.Navigate(connect1);
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
            }));
        }
        public void startThisShit()
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            ThicknessAnimation animation = new ThicknessAnimation();
            Thickness marginFrom = new Thickness();
            marginFrom.Left = 30;
            Thickness marginTo = new Thickness();
            marginTo.Left = 0;
            animation.From = marginFrom;
            animation.To = marginTo;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(400));
            connectPage3 connect3 = new connectPage3(this);
            connectWindow.Navigate(connect3);
            connect3.BeginAnimation(Page.OpacityProperty, da);
            connect3.BeginAnimation(Page.MarginProperty, animation);
            Thread startThisFckingShit = new Thread(startClient);
            startThisFckingShit.Start();
        }

        public void notification(string notificationText)
        {
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText01);
            Windows.Data.Xml.Dom.XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(notificationText));
            Windows.Data.Xml.Dom.XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "notificationPhoto.png");
            ((Windows.Data.Xml.Dom.XmlElement)toastImageAttributes[0]).SetAttribute("src", path);
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier("Computer Monitor").Show(toast);
        }

        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            string data = dateTime.ToString();
            data = data.Replace("-", "");
            data = data.Replace(":", "");
            data = data.Replace(" ", "");
        }

    }
}
