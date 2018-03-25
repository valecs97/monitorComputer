using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.IO;

namespace Monitor_Computer
{
    class NetworkingServer
    {
        private MainWindow mainWindowClass;
        public sendCommandsServer sendCommandsServerClass;
        private encryption encryptionClass;
        public NetworkingServer(MainWindow mainWindowClass, encryption encryptionClass)
        {
            sendCommandsServerClass = new sendCommandsServer(this,mainWindowClass);
            this.mainWindowClass = mainWindowClass;
            this.encryptionClass = encryptionClass;
        }
        private bool isConnected = false;
        private bool canConnect = false;
        private bool closeCommand = false;
        private long bytesPerSecond = 0;
        private bool canMeasureSpeed = false;
        public bool shouldShutdown = false;
        NetworkStream networkStream;
        TcpListener server;
        TcpClient client = default(TcpClient);
        List<string> ipToBlock = new List<string>();
        List<string> malwareToBlock = new List<string>();
        List<string> ipWhichTriedToConnect = new List<string>();
        List<int> passwordTriedPerIpTries = new List<int>();
        public bool startPort()
        {
            server = new TcpListener(System.Net.IPAddress.Any, mainWindowClass.serverPort);
            try
            {
                server.Start();
                return true;
            }
            catch (Exception)
            {
                mainWindowClass.portIsNotGood();
                return false;
            }
        }
        public void openServer()
        {
                mainWindowClass.serverStatus = true;
                checkClient();
                while (isConnected)
                {
                
                    try
                    {
                    
                    networkStream = client.GetStream();
                        byte[] byteFrom = new byte[100250];
                        networkStream.Read(byteFrom, 0, (int)client.ReceiveBufferSize);
                        string dataFromClient = System.Text.Encoding.ASCII.GetString(byteFrom);
                        dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                        dataFromClient = encryptionClass.Decrypt(dataFromClient, "c0mm@nds t0 s3nd %!3");
                    if (dataFromClient != "FailedToDecrypt")
                    {
                        if (dataFromClient == "Closes")
                        {
                            closeServer();
                            isConnected = false;
                        }
                        string command = dataFromClient.Substring(0, 6);
                        switch (command)
                        {
                            case "strmnt":
                                {

                                    command = dataFromClient.Substring(6);
                                    int i = 0;
                                    string aux = "";
                                    while (command[i] != ' ')
                                    {
                                        aux += command[i];
                                        i++;
                                    }
                                    //MessageBox.Show(aux);
                                    sendCommandsServerClass.warningCpu = Int32.Parse(aux);
                                    //MessageBox.Show(sendCommandsServerClass.warningRAM);
                                    aux = "";
                                    i++;
                                    while (command[i] != ' ')
                                    {
                                        aux += command[i];
                                        i++;
                                    }
                                    sendCommandsServerClass.warningRAM = Int32.Parse(aux);
                                    aux = "";
                                    i++;
                                    while (command[i] != ' ')
                                    {
                                        aux += command[i];
                                        i++;
                                    }
                                    sendCommandsServerClass.warningTime = Int32.Parse(aux);
                                    sendCommandsServerClass.monitoringStatus = true;
                                    Thread startMonitoring = new Thread(sendCommandsServerClass.startMonitoring);
                                    startMonitoring.Start();
                                };
                                break;
                            case "strchg":
                                {
                                    command = dataFromClient.Substring(6);
                                    int i = 0;
                                    string aux = "";
                                    while (command[i] != ' ')
                                    {
                                        aux += command[i];
                                        i++;
                                    }
                                    //MessageBox.Show(aux);
                                    if (sendCommandsServerClass.warningCpu != Int32.Parse(aux))
                                    {
                                        sendCommandsServerClass.warningCpu = Int32.Parse(aux);
                                        sendCommandsServerClass.hintsCpu = 0;
                                    }
                                    //MessageBox.Show(sendCommandsServerClass.warningRAM);
                                    aux = "";
                                    i++;
                                    while (command[i] != ' ')
                                    {
                                        aux += command[i];
                                        i++;
                                    }
                                    if (sendCommandsServerClass.warningRAM != Int32.Parse(aux))
                                    {
                                        sendCommandsServerClass.warningRAM = Int32.Parse(aux);
                                        sendCommandsServerClass.hintsRam = 0;
                                    }
                                    aux = "";
                                    i++;
                                    while (command[i] != ' ')
                                    {
                                        aux += command[i];
                                        i++;
                                    }
                                    if (sendCommandsServerClass.warningTime != Int32.Parse(aux))
                                    {
                                        sendCommandsServerClass.warningTime = Int32.Parse(aux);
                                        sendCommandsServerClass.hintsCpu = 0;
                                        sendCommandsServerClass.hintsRam = 0;
                                    }
                                }; break;
                            case "scrsht":
                                {
                                    sendPhoto(sendCommandsServerClass.screenshot());
                                };
                                break;
                            case "sndfil":
                                {
                                    command = dataFromClient.Substring(6);
                                    int i = 0;
                                    long fileLenght;
                                    string fileName = "",aux="";
                                    while (command[i] != '?')
                                    {
                                        fileName += command[i];
                                        i++;
                                    }
                                    i++;
                                    while (command[i] != ' ')
                                    {
                                        aux += command[i];
                                        i++;
                                    }
                                    fileLenght = Int64.Parse(aux);
                                    receiveFile(fileName,fileLenght);
                                }; break;
                            case "stpmnt": sendCommandsServerClass.monitoringStatus = false; break;
                            case "closec": sendCommandsServerClass.closeComputer(); break;
                            case "restar": sendCommandsServerClass.restartComputer(); break;
                            case "sleepq": sendCommandsServerClass.sleepComputer(); break;
                            case "messag":
                                {
                                    command = dataFromClient.Substring(6);
                                    if (mainWindowClass.chatOpen)
                                        mainWindowClass.chatWindow.Dispatcher.Invoke(new Action(()=> mainWindowClass.chatWindow.receiveMessage(command, true)));
                                    else
                                    {
                                        mainWindowClass.Dispatcher.Invoke(new Action(() => mainWindowClass.chatButtonClick(command, true)));
                                    }
                                }; break;
                        }
                    }
                    else
                    {
                        isConnected = false;
                    }
                }
                    catch (Exception)
                    {
                    isConnected = false;
                    }
                }
                sendCommandsServerClass.monitoringStatus = false;
                client.Close();
            if (mainWindowClass.keepServerOnAfterClient)
                openServer();
            else {
                mainWindowClass.canCloseTheApplication = true;
                mainWindowClass.serverStatus = false;
            }
            server.Stop();
            mainWindowClass.chatAvalible = false;
            mainWindowClass.restoreConnectPage1Default();
            if (mainWindowClass.chatWindow != null)
                    mainWindowClass.Dispatcher.Invoke(new Action(() => mainWindowClass.chatWindow.Close()));
        }

        public bool checkCpuRam()
        {
            return sendCommandsServerClass.monitoringStatus;
        }

        public void forceCloseCpuRam()
        {
            sendCommandsServerClass.monitoringStatus = false;
        }
        
        private void checkClient()
        {
            canConnect = false;
            isConnected = false;
            closeCommand = false;
            ipToBlock.Clear();
            malwareToBlock.Clear();
            ipWhichTriedToConnect.Clear();
            mainWindowClass.statusControl.Dispatcher.Invoke(new Action(() => mainWindowClass.statusControl.Content = "Status : waiting for user"));
            while (!canConnect)
            {
                client = server.AcceptTcpClient();
                bool okIp = true;
                string clientIp = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                foreach (string ip in malwareToBlock)
                {
                    if (ip == clientIp)
                    {
                        okIp = false;
                        client.Close();
                    }
                }
                foreach (string ip in ipToBlock)
                {
                    if (ip == clientIp)
                    {
                        networkStream = client.GetStream();
                        string serverResponse = encryptionClass.Encrypt("Max reached", "1s 1t 0k %!1,5") + "$";
                        Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();
                        okIp = false;
                        client.Close();
                    }
                }
                if (okIp) {
                    try
                    {
                        networkStream = client.GetStream();
                        string serverResponse = encryptionClass.Encrypt("NothingWrong","1s 1t 0k %!1,5") + "$";
                        Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();

                        networkStream = client.GetStream();
                        byte[] byteFrom = new byte[100250];
                        networkStream.Read(byteFrom, 0, (int)client.ReceiveBufferSize);
                        string dataFromClient = System.Text.Encoding.ASCII.GetString(byteFrom);
                        dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                        dataFromClient = encryptionClass.Decrypt(dataFromClient, "Th15 p@55w0r4 w111 b3 r3c31v3d %!1");
                        if (dataFromClient == "FailedToDecrypt")
                        {
                            malwareToBlock.Add(clientIp);
                            mainWindowClass.failedToDecryptAtPassCheckServer();
                        }
                        else if (dataFromClient == mainWindowClass.serverPassword)
                        {
                            isConnected = true;
                            canConnect = true;
                            for (int i = 0; i < ipWhichTriedToConnect.Count; i++)
                                if (ipWhichTriedToConnect[i] == clientIp)
                                {
                                    passwordTriedPerIpTries[i]=0;
                                }
                            mainWindowClass.notification("Somebody has connected to your computer !");
                            networkStream = client.GetStream();
                            serverResponse = encryptionClass.Encrypt("YouShallPassqwerty", "Th15 1s th3 r3sp0ns3 %!2") + "$";
                            sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                            networkStream.Write(sendBytes, 0, sendBytes.Length);
                            networkStream.Flush();
                            mainWindowClass.statusControl.Dispatcher.Invoke(new Action(() => mainWindowClass.statusControl.Content = "Status : online"));
                            mainWindowClass.chatAvalible = true;
                            mainWindowClass.activateChat();
                        }
                        else
                        {
                            bool found = false;
                            int whichOne=0;
                            for (int i = 0; i < ipWhichTriedToConnect.Count; i++)
                                if (ipWhichTriedToConnect[i] == clientIp)
                                {
                                    found = true;
                                    passwordTriedPerIpTries[i]++;
                                    whichOne = passwordTriedPerIpTries[i];
                                    if (passwordTriedPerIpTries[i] == 3) { 
                                        ipToBlock.Add(ipWhichTriedToConnect[i]);
                                    }
                                }
                            if (!found)
                            {
                                ipWhichTriedToConnect.Add(clientIp);
                                passwordTriedPerIpTries.Add(1);
                                whichOne = passwordTriedPerIpTries[passwordTriedPerIpTries.Count - 1];
                            }
                            networkStream = client.GetStream();
                            serverResponse = encryptionClass.Encrypt("YouShallNotPass"+(4-whichOne).ToString(), "Th15 1s th3 r3sp0ns3 %!2") + "$";
                            sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                            networkStream.Write(sendBytes, 0, sendBytes.Length);
                            networkStream.Flush();
                        }
                        if (!canConnect)
                            client.Close();
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
        public void sendCommand(string dataToSend, string howToEncrypt)
        {
            try {
                networkStream = client.GetStream();
                dataToSend = encryptionClass.Encrypt(dataToSend, howToEncrypt);
                string serverResponse = dataToSend + "$";
                Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);

                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
            }
            catch (Exception)
            {
                sendCommandsServerClass.monitoringStatus = false;
            }
        }

        public void sendPhoto(string fromWhere)
        {
            encryptionClass.EncryptFile(fromWhere);
            byte[] SendingBuffer = null;
            networkStream = client.GetStream();
            int BufferSize = 1024;
            canMeasureSpeed = true;
            Thread transfSpeed = new Thread(transferSpeed);
            transfSpeed.Start();
            try
            {
                using (FileStream fs = File.Open(fromWhere, FileMode.Open, FileAccess.Read, FileShare.None)) { 
                    int NoOfPackets = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(fs.Length) / Convert.ToDouble(BufferSize)));
                    int TotalLenght = (int)fs.Length, CurrentPacketLengh=0;
                    sendCommand("scrsht" + fs.Length.ToString(), "c0mm@nds t0 r3c3iv3 %!4");
                    for (int i = 0; i < NoOfPackets; i++)
                    {
                        if (TotalLenght > BufferSize)
                        {
                            CurrentPacketLengh = BufferSize;
                            TotalLenght = TotalLenght - CurrentPacketLengh;
                        }
                        else
                            CurrentPacketLengh = TotalLenght;
                        SendingBuffer = new byte[CurrentPacketLengh];
                        fs.Read(SendingBuffer, 0, CurrentPacketLengh);
                        bytesPerSecond += CurrentPacketLengh;
                        networkStream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                    }
                }
            }
            catch (Exception)
            {
                
            }
            canMeasureSpeed = false;
            File.Delete(fromWhere);
        }

        public void receiveFile(string name,long fileLenght)
        {
            string fileName = (System.IO.Path.Combine(mainWindowClass.saveFolder, name));
            byte[] RecData = new byte[1024];
            int RecBytes;
            canMeasureSpeed = true;
            Thread transfSpeed = new Thread(transferSpeed);
            transfSpeed.Start();
            NetworkStream netstream = null;
            try
            {
                netstream = client.GetStream();
                FileStream Fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                while (fileLenght!=0)
                {
                    RecBytes = netstream.Read(RecData, 0, RecData.Length);
                    Fs.Write(RecData, 0, RecBytes);
                    bytesPerSecond += RecBytes;
                    fileLenght -= RecBytes;
                }
                Fs.Close();
            }
            catch (Exception)
            {

            }
            canMeasureSpeed = false;
        }

        private void transferSpeed()
        {
            while (canMeasureSpeed)
            {
                double aux = (double)bytesPerSecond / 1024 / 1024;
                aux = Math.Round(aux, 2);
                mainWindowClass.speedControl.Dispatcher.Invoke(new Action(() => mainWindowClass.speedControl.Content = "Speed : " + aux.ToString() + " mb/s"));
                bytesPerSecond = 0;
                Thread.Sleep(1000);
            }
            mainWindowClass.speedControl.Dispatcher.Invoke(new Action(() => mainWindowClass.speedControl.Content = "Speed : -"));
        }

        public void closeServer()
        {
            mainWindowClass.canCloseTheApplication = true;
            string statusCheck="";
            try {
                mainWindowClass.statusControl.Dispatcher.Invoke(new Action(() => statusCheck = mainWindowClass.statusControl.Content.ToString()));
            }
            catch (TaskCanceledException)
            { };
            if (checkCpuRam())
            {
                forceCloseCpuRam();
                
            }
            if (statusCheck == "Status : waiting for user")
            {
                canConnect = true;
                NetworkStream server;
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                client = new TcpClient();
                client.Connect("localhost", mainWindowClass.serverPort);
                server = client.GetStream();
                string toSend = encryptionClass.Encrypt("I hope no one will use this password... P.S reeallyyy hopeeee", "Th15 p@55w0r4 w111 b3 r3c31v3d %!1");
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(toSend + "$");
                server.Write(outStream, 0, (int)outStream.Length);
                server.Flush();
                try
                {
                    mainWindowClass.Dispatcher.Invoke(new Action(() => mainWindowClass.statusControl.Content = "Status : offline"));
                }
                catch (TaskCanceledException)
                { };
            }
            else if (statusCheck == "Status : online" && !closeCommand)
            {
                try
                {
                    mainWindowClass.Dispatcher.Invoke(new Action(() => mainWindowClass.statusControl.Content = "Status : offline"));
                }
                catch (TaskCanceledException)
                { };
                closeCommand = true;
                sendCommand("Closes", "c0mm@nds t0 r3c3iv3 %!4");
            }
            else if (shouldShutdown)
                sendCommandsServerClass.shutdownComputer();
        }
    }
}
