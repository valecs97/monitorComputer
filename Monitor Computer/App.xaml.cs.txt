using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Monitor_Computer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow wnd;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            wnd = new MainWindow();
            wnd.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            wnd.OnExitCommands();
        }
    }
}
