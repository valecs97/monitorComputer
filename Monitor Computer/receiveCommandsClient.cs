using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Drawing;
using System.IO;

namespace Monitor_Computer
{
    class receiveCommandsClient
    {
        private NetworkingClient networkingClientClass;
        private MainWindow mainWindowClass;
        public receiveCommandsClient(NetworkingClient networkingClientClass,MainWindow mainWindowClass)
        {
            this.networkingClientClass = networkingClientClass;
            this.mainWindowClass = mainWindowClass;
        }

        //HERE STARTS THE RAM/CPU MONITORING

        public void sendCpuRamTime()
        {
            int warningCpu=10, warningRAM=10, warningTime=10;
            mainWindowClass.warningCPU.Dispatcher.Invoke(new Action(() => warningCpu = (int)mainWindowClass.warningCPU.Value));
            mainWindowClass.warningRAM.Dispatcher.Invoke(new Action(() => warningRAM = (int)mainWindowClass.warningRAM.Value));
            mainWindowClass.warningTime.Dispatcher.Invoke(new Action(() => warningTime = (int)mainWindowClass.warningTime.Value));
            networkingClientClass.sendCommand("strmnt" + warningCpu.ToString()+ ' ' + warningRAM.ToString()+ ' ' + warningTime.ToString() + ' ', "c0mm@nds t0 s3nd %!3");
        }

        public void receiveCpuCounter(string cpu)
        {
            try
            {
                mainWindowClass.MonitoringStatsCPU.Dispatcher.Invoke(new Action(() => mainWindowClass.MonitoringStatsCPU.Content = "Cpu usage : " + cpu + "%"));
            }
            catch (TaskCanceledException) { }
        }

        public void receiveRamCounter(string ram)
        {
            try
            {
                mainWindowClass.MonitoringStatsRAM.Dispatcher.Invoke(new Action(() => mainWindowClass.MonitoringStatsRAM.Content = "Available Ram : " + ram + " mb"));
            }
            catch (TaskCanceledException) { }
        }

        public void stopReceiving()
        {
            try
            {
                mainWindowClass.MonitoringStatsCPU.Dispatcher.Invoke(new Action(() => mainWindowClass.MonitoringStatsCPU.Content = "Cpu usage : -"));
                mainWindowClass.MonitoringStatsRAM.Dispatcher.Invoke(new Action(() => mainWindowClass.MonitoringStatsRAM.Content = "Available Ram : -"));
                mainWindowClass.warningCPU.Dispatcher.Invoke(new Action(() => mainWindowClass.warningCPU.IsEnabled = true));
                mainWindowClass.warningRAM.Dispatcher.Invoke(new Action(() => mainWindowClass.warningRAM.IsEnabled = true));
                mainWindowClass.warningTime.Dispatcher.Invoke(new Action(() => mainWindowClass.warningTime.IsEnabled = true));
            }
            catch (TaskCanceledException) { }
        }

        public async void cpuWarningMessage()
        {
            await mainWindowClass.Dispatcher.BeginInvoke(new Action(async () => await mainWindowClass.ShowMessageAsync("Warning", "CPU is under heavy usage , please consider closing some apps !")));
        }

        public async void ramWarningMessage()
        {
            await mainWindowClass.Dispatcher.BeginInvoke(new Action(async () => await mainWindowClass.ShowMessageAsync("Warning", "RAM is almost full, please consider closing some apps !")));
        }

        //HERE ENDS THE CPU/RAM MONITORING

        //HERE START SCREENSHOT GRABER

        public string screenshotGraber(string screenshot)
        {
            Image img = null;
            byte[] bitmapBytes = Convert.FromBase64String(screenshot);
            using (MemoryStream memorystream = new MemoryStream(bitmapBytes))
            {
                img = Image.FromStream(memorystream);
            }
            string fileName = (System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "screenShot.bmp"));
            System.IO.FileStream fs = System.IO.File.Open(fileName, System.IO.FileMode.OpenOrCreate);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            return fileName;
        }

        //HERE ENDS SCREENSHOT GRABER

    }
}
