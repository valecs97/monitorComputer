using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Monitor_Computer
{
    class sendCommandsServer
    {
        private NetworkingServer networkingServerClass;
        private MainWindow mainWindowClass;
        public int hintsCpu, hintsRam;
        public sendCommandsServer(NetworkingServer networkingServerClass, MainWindow mainWindowClass)
        {
            this.networkingServerClass = networkingServerClass;
            this.mainWindowClass = mainWindowClass;
        }

        //HERE STARTS THE RAM/CPU MONITORING

        public int warningCpu, warningRAM, warningTime;
        public bool monitoringStatus = false;

        private float getCPUCounter()
        {

            PerformanceCounter cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            dynamic firstValue = cpuCounter.NextValue();
            System.Threading.Thread.Sleep(750);
            dynamic secondValue = cpuCounter.NextValue();
            return secondValue;

        }

        private float getRAMAvailable()
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            dynamic firstValue = ramCounter.NextValue();
            return firstValue;
        }
        public void startMonitoring()
        {
            hintsCpu = 0;
            hintsRam = 0;
            while (monitoringStatus)
            {
                string toSend = "mntsts";
                float cpu = getCPUCounter();
                cpu = (float)Math.Round(cpu, 2);
                float ram = getRAMAvailable();
                toSend += cpu.ToString();
                toSend += " ";
                toSend += ram.ToString();
                toSend += " ";
                if (cpu >= warningCpu)
                    hintsCpu++;
                else
                    hintsCpu = 0;
                if (hintsCpu == warningTime)
                {
                    toSend += "y";
                    toSend += " ";
                    hintsCpu = -60 + warningTime;
                }
                else
                {
                    toSend += "n";
                    toSend += " ";
                }
                if (ram <= warningRAM)
                    hintsRam++;
                else
                    hintsRam = 0;
                if (hintsRam == warningTime)
                {
                    toSend += "y";
                    toSend += " ";
                    hintsRam = -60 + warningTime;
                }     
                if (monitoringStatus)
                    networkingServerClass.sendCommand(toSend, "c0mm@nds t0 r3c3iv3 %!4");
            }
            //networkingServerClass.sendCommand("stpmnt", "c0mm@nds t0 r3c3iv3 %!4");
            //ACI ESTE PROBLEMA SECOLULUI , SI AM REMEDIAT-O 
        }

        //HERE ENDS THE CPU/RAM MONITORING

        //HERE BEGINS CLOSE COMPUTER


        public void closeComputer()
        {
            mainWindowClass.stopServer();
            networkingServerClass.shouldShutdown = true;
        }

        public void shutdownComputer()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        //HERE ENDS CLOSE COMPUTER

        //HERE BEGINS SCREENSHOT GRABER

        public string screenshot()
        {
            string fileName = (System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "screenShot.bmp"));
            using (System.IO.FileStream fs = System.IO.File.Create(fileName))
            {
                Bitmap Screenshot;
                Screenshot = new System.Drawing.Bitmap((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(Screenshot))
                {
                    g.CopyFromScreen(0, 0, 0, 0, Screenshot.Size);
                }
                Screenshot.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                string s = Screenshot.ToString();
                fs.Close();
                return fileName;
            }
        }

        //HERE ENDS SCREENSHOT GRABER

        //HERE BEGINS RESTART COMPUTER
        public void restartComputer()
        {
            mainWindowClass.stopServer();
            var psi = new ProcessStartInfo("shutdown", "/r /t 0");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }
        //HERE ENDS RESTART COMPUTER

        //HERE BEGINS SLEEP COMPUTER
        public void sleepComputer()
        {
            mainWindowClass.stopServer();
            var psi = new ProcessStartInfo("shutdown", "/h /f");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }
        //HERE ENDS SLEEP COMPUTER
    }
}
