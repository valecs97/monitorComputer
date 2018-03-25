using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MahApps.Metro.Controls.Dialogs;

namespace Monitor_Computer
{
    class NetworkingClient
    {
        private MainWindow mainWindowClass;
        private receiveCommandsClient receiveCommandsClientClass;
        private encryption encryptionClass;
        public NetworkingClient(MainWindow mainWindowClass, encryption encryptionClass)
        {
            receiveCommandsClientClass = new receiveCommandsClient(this, mainWindowClass);
            this.mainWindowClass = mainWindowClass;
            this.encryptionClass = encryptionClass;
        }
        NetworkStream server;

        System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
        private bool isConnected = false, closeCommand = false, invalidServer = false, maxattemptsreached = false;
        public string attempts;
        public int makeConnection()
        {
            closeCommand = false;
            client = new System.Net.Sockets.TcpClient();
            try
            {
                client.Connect(mainWindowClass.ipToConnect, mainWindowClass.portToConnect);
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionRefused)
                    return 1;
                else if (ex.SocketErrorCode == SocketError.HostNotFound)
                    return 2;
                else if (ex.SocketErrorCode == SocketError.TimedOut)
                    return 3;
                else if (ex.SocketErrorCode == SocketError.NetworkUnreachable)
                    return 4;
                else MessageBox.Show(ex.ToString());
            }
            passwordCheck();
            Thread receiveCommandsThread = new Thread(receiveCommands);
            receiveCommandsThread.Start();
            //MessageBox.Show(isConnected.ToString() + " " + invalidServer.ToString() + " " + maxattemptsreached.ToString());
            if (isConnected && !invalidServer && !maxattemptsreached)
                return 0;
            else if (!isConnected && !invalidServer && !maxattemptsreached)
                return -1;
            else if (!isConnected && invalidServer && !maxattemptsreached)
                return -2;
            else
                return -3;
        }
        private void passwordCheck()
        {
            maxattemptsreached = false;
            try
            {
                server = client.GetStream();
                byte[] inStream = new byte[100250];
                server.Read(inStream, 0, (int)client.ReceiveBufferSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                returndata = encryptionClass.Decrypt(returndata, "1s 1t 0k %!1,5");
                if (returndata == "NothingWrong")
                {
                    if (mainWindowClass.passwordToConnect != "")
                        sendCommand(mainWindowClass.passwordToConnect, "Th15 p@55w0r4 w111 b3 r3c31v3d %!1");
                    else
                        sendCommand("D3f@u1t p@55w0rd %!0", "Th15 p@55w0r4 w111 b3 r3c31v3d %!1");
                    server = client.GetStream();
                    inStream = new byte[100250];
                    server.Read(inStream, 0, (int)client.ReceiveBufferSize);
                    returndata = System.Text.Encoding.ASCII.GetString(inStream);
                    returndata = returndata.Substring(0, returndata.IndexOf("$"));
                    returndata = encryptionClass.Decrypt(returndata, "Th15 1s th3 r3sp0ns3 %!2");
                    if (returndata != "FailedToDecrypt")
                    {
                        if (returndata.Substring(0, 15) == "YouShallNotPass")
                        {
                            isConnected = false;
                            invalidServer = false;
                            attempts = returndata.Substring(15);
                        }
                        else if (returndata.Substring(0, 12) == "YouShallPass")
                        {

                            isConnected = true;
                            invalidServer = false;
                        }
                    }
                    else
                    {
                        isConnected = false;
                        invalidServer = true;
                    }
                }
                else
                {
                    isConnected = false;
                    invalidServer = false;
                    maxattemptsreached = true;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(e.ToString());
                isConnected = false;
                invalidServer = true;
            }
        }
        public void sendCommand(string dataToSend, string howToEncrypt)
        {
            server = client.GetStream();
            dataToSend = encryptionClass.Encrypt(dataToSend, howToEncrypt);
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(dataToSend + "$");
            server.Write(outStream, 0, (int)outStream.Length);
            server.Flush();
        }
        public void receiveCommands()
        {
            try
            {
                while (isConnected)
                {

                    server = client.GetStream();
                    byte[] inStream = new byte[10025000];
                    server.Read(inStream, 0, (int)client.ReceiveBufferSize);
                    server.Flush();
                    string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                    returndata = returndata.Substring(0, returndata.IndexOf("$"));
                    returndata = encryptionClass.Decrypt(returndata, "c0mm@nds t0 r3c3iv3 %!4");
                    if (returndata == "FailedToDecrypt")
                    {
                        closeClient();
                        isConnected = false;
                        mainWindowClass.failedToDecryptAfterPassClient();
                    }
                    if (returndata == "Closes")
                    {
                        if (!closeCommand)
                            mainWindowClass.notification("The server has shut down. You have been disconnected !");
                        closeClient();
                        isConnected = false;

                    }
                    string command = returndata.Substring(0, 6);
                    switch (command)
                    {
                        case "mntsts":
                            {
                                command = returndata.Substring(6);
                                int i = 0;
                                string aux = "";
                                while (command[i] != ' ')
                                {
                                    aux += command[i];
                                    i++;
                                }
                                i++;
                                receiveCommandsClientClass.receiveCpuCounter(aux);
                                aux = "";
                                while (command[i] != ' ')
                                {
                                    aux += command[i];
                                    i++;
                                }
                                i++;

                                receiveCommandsClientClass.receiveRamCounter(aux);
                                //MessageBox.Show(command[i].ToString());
                                if (command[i] == 'y')
                                    receiveCommandsClientClass.cpuWarningMessage();
                                i += 2;
                                if (command[i] == 'y')
                                    receiveCommandsClientClass.ramWarningMessage();
                            }; break;
                        case "scrsht":
                            {
                                command = returndata.Substring(6);
                                int amount = Int32.Parse(command);
                                receivePhoto(amount);
                            }; break;
                        case "stpmnt": receiveCommandsClientClass.stopReceiving(); break;
                        case "messag":
                            {
                                command = returndata.Substring(6);
                                if (mainWindowClass.chatOpen)
                                    mainWindowClass.chatWindow.Dispatcher.Invoke(new Action(() => mainWindowClass.chatWindow.receiveMessage(command, false)));
                                else
                                {
                                    mainWindowClass.Dispatcher.Invoke(new Action(() => mainWindowClass.chatButtonClick(command, false)));
                                }
                            }; break;
                    }
                    if (!isConnected)
                        receiveCommandsClientClass.stopReceiving();
                }
                client.Close();
                server.Close();

            }
            catch (Exception)
            {
                showForceClosedError();
                isConnected = false;
                closeCommand = true;
                mainWindowClass.restoreConnectPage1Default();
                closeClient();
            }
        }

        private void receivePhoto(int size)
        {
            blockUserFromDoingAnythingStupid();
            canMeasureSpeed = true;
            Thread transfSpeed = new Thread(transferSpeed);
            transfSpeed.Start();
            DateTime dateTime = DateTime.Now;
            string data = dateTime.ToString();
            data = data.Replace("-", "");
            data = data.Replace(":", "");
            data = data.Replace(" ", "");
            data = data.Replace("/", "");
            data = data.Replace(".", "");
            string fileName = (System.IO.Path.Combine(mainWindowClass.saveFolder, data+".bmp"));
            byte[] RecData = new byte[1024];
            int RecBytes;
            NetworkStream netstream = null;
            mainWindowClass.progressBar.Dispatcher.Invoke(new Action (()=>mainWindowClass.progressBar.Maximum = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(size) / Convert.ToDouble(1024)))));
            try
            {
                netstream = client.GetStream();
                FileStream Fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                
                while (size!=0)
                {
                    RecBytes = netstream.Read(RecData, 0, RecData.Length);
                    Fs.Write(RecData, 0, RecBytes);
                    size -= RecBytes;
                    bytesPerSecond += RecBytes;
                    mainWindowClass.progressBar.Dispatcher.Invoke(new Action(() => mainWindowClass.progressBar.Value++));
                }
                Fs.Close();
             }
             catch (Exception)
             {
                 
             }
            canMeasureSpeed = false;
            encryptionClass.DecryptFile(fileName);
            System.Diagnostics.Process.Start(fileName);
            startMonitoring();
            stopBlockingTheUser();
            mainWindowClass.progressBar.Dispatcher.Invoke(new Action(() => mainWindowClass.progressBar.Value=0));
        }

        public string fileLocation;

        private void blockUserFromDoingAnythingStupid()
        {
            mainWindowClass.settingsTab.Dispatcher.Invoke(new Action(() => mainWindowClass.settingsTab.IsEnabled = false));
            mainWindowClass.taskManagerTab.Dispatcher.Invoke(new Action(() => mainWindowClass.taskManagerTab.IsEnabled = false));
            mainWindowClass.shutDownComputer.Dispatcher.Invoke(new Action(() => mainWindowClass.shutDownComputer.IsEnabled = false));
            mainWindowClass.restartComputer.Dispatcher.Invoke(new Action(() => mainWindowClass.restartComputer.IsEnabled = false));
            mainWindowClass.screenShotButton.Dispatcher.Invoke(new Action(() => mainWindowClass.screenShotButton.IsEnabled = false));
            mainWindowClass.sendFileButton.Dispatcher.Invoke(new Action(() => mainWindowClass.sendFileButton.IsEnabled = false));
        }

        private void stopBlockingTheUser()
        {
            mainWindowClass.settingsTab.Dispatcher.Invoke(new Action(() => mainWindowClass.settingsTab.IsEnabled = true));
            mainWindowClass.taskManagerTab.Dispatcher.Invoke(new Action(() => mainWindowClass.taskManagerTab.IsEnabled = true));
            mainWindowClass.shutDownComputer.Dispatcher.Invoke(new Action(() => mainWindowClass.shutDownComputer.IsEnabled = true));
            mainWindowClass.restartComputer.Dispatcher.Invoke(new Action(() => mainWindowClass.restartComputer.IsEnabled = true));
            mainWindowClass.screenShotButton.Dispatcher.Invoke(new Action(() => mainWindowClass.screenShotButton.IsEnabled = true));
            mainWindowClass.sendFileButton.Dispatcher.Invoke(new Action(() => mainWindowClass.sendFileButton.IsEnabled = true));
        }

        private long bytesPerSecond=0;
        private bool canMeasureSpeed = false;

        public void sendFile(int BufferSize)
        {

            blockUserFromDoingAnythingStupid();
            string filename = Path.GetFileName(fileLocation);
            byte[] SendingBuffer = null;
            server = client.GetStream();
            canMeasureSpeed = true;
            Thread transfSpeed = new Thread(transferSpeed);
            transfSpeed.Start();
            try
            {
                using (FileStream fs = File.Open(fileLocation, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    long NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(fs.Length) / Convert.ToDouble(BufferSize)));
                    long TotalLenght = (long)fs.Length, CurrentPacketLengh = 0;
                    mainWindowClass.progressBar2.Dispatcher.Invoke(new Action(()=> mainWindowClass.progressBar2.Maximum = NoOfPackets));
                    sendCommand("sndfil"+ filename +"?"+ fs.Length.ToString()+" ", "c0mm@nds t0 s3nd %!3");
                    for (int i = 0; i < NoOfPackets; i++)
                    {
                        //MessageBox.Show(NoOfPackets.ToString() + " " + i);
                        if (TotalLenght > BufferSize)
                        {
                            CurrentPacketLengh = BufferSize;
                            TotalLenght = TotalLenght - CurrentPacketLengh;
                        }
                        else
                            CurrentPacketLengh = TotalLenght;
                        SendingBuffer = new byte[CurrentPacketLengh];
                        fs.Read(SendingBuffer, 0, (int)CurrentPacketLengh);
                        server.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                        bytesPerSecond += CurrentPacketLengh;
                        mainWindowClass.progressBar2.Dispatcher.Invoke(new Action(() => mainWindowClass.progressBar2.Value++));
                    }
                }
            }
            catch (Exception)
            {
                
            }
            canMeasureSpeed = false;
            stopBlockingTheUser();
            mainWindowClass.progressBar2.Dispatcher.Invoke(new Action(() => mainWindowClass.progressBar2.Value=0));
        }

        private void transferSpeed()
        {
            while (canMeasureSpeed)
            {
                double aux = (double)bytesPerSecond / 1024 / 1024;
                aux = Math.Round(aux,2);
                mainWindowClass.speedControl.Dispatcher.Invoke(new Action(() => mainWindowClass.speedControl.Content = "Speed : " + aux.ToString() + " mb/s"));
                bytesPerSecond = 0;
                Thread.Sleep(1000);
            }
            mainWindowClass.speedControl.Dispatcher.Invoke(new Action(() => mainWindowClass.speedControl.Content = "Speed : -"));
        }

        private async void showForceClosedError()
        {
            await mainWindowClass.Dispatcher.BeginInvoke(new Action(async () => await mainWindowClass.ShowMessageAsync("Whoopss", "It seems that the server was closed !")));
        }

        public void startMonitoring()
        {
            receiveCommandsClientClass.sendCpuRamTime();
        }
        public void stopMonitoring()
        {
            sendCommand("stpmnt", "c0mm@nds t0 s3nd %!3");
        }
        public void closeClient()
        {
            System.Threading.Thread.Sleep(100);
            if (!closeCommand)
            {
                closeCommand = true;
                mainWindowClass.chatAvalible = false;
                
                try
                {
                    mainWindowClass.restoreConnectPage1Default();
                    mainWindowClass.taskManagerTab.Dispatcher.Invoke(new Action(() => mainWindowClass.taskManagerTab.IsEnabled = false));
                    mainWindowClass.commandsTab.Dispatcher.Invoke(new Action(() => mainWindowClass.commandsTab.IsEnabled = false));
                    mainWindowClass.shutDownComputer.Dispatcher.Invoke(new Action(() => mainWindowClass.shutDownComputer.IsEnabled = false));
                    mainWindowClass.restartComputer.Dispatcher.Invoke(new Action(() => mainWindowClass.restartComputer.IsEnabled = false));
                    mainWindowClass.screenShotButton.Dispatcher.Invoke(new Action(() => mainWindowClass.screenShotButton.IsEnabled = false));
                    mainWindowClass.sendFileButton.Dispatcher.Invoke(new Action(() => mainWindowClass.sendFileButton.IsEnabled = false));
                }
                catch (TaskCanceledException) { }
                try
                {
                    mainWindowClass.Dispatcher.Invoke(new Action(() => mainWindowClass.statusControl.Content = "Status : disconnected"));
                }
                catch (TaskCanceledException) { }
                sendCommand("Closes", "c0mm@nds t0 s3nd %!3");

            }
            else
            {
                try {
                    mainWindowClass.Dispatcher.Invoke(new Action(() => mainWindowClass.statusControl.Content = "Status : offline"));
                }
                catch (TaskCanceledException) { }
            }
            if (mainWindowClass.chatWindow != null)
                mainWindowClass.Dispatcher.Invoke(new Action(() => mainWindowClass.chatWindow.Close()));
        }
    }
}
