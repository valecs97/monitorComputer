using System;
using System.Windows;
using System.Windows.Input;

namespace Samples.Commands
{
    /// <summary>
    /// Shows the main window.
    /// </summary>
    public class ShowSampleWindowCommand : CommandBase<ShowSampleWindowCommand>
    {
        public override void Execute(object parameter)
        {
                GetTaskbarWindow(parameter).Show();
                GetTaskbarWindow(parameter).WindowState = WindowState.Normal;
                GetTaskbarWindow(parameter).Activate();
                GetTaskbarWindow(parameter).Topmost = true;  // important
                GetTaskbarWindow(parameter).Topmost = false; // important
                GetTaskbarWindow(parameter).Focus();
                CommandManager.InvalidateRequerySuggested();
        }


        public override bool CanExecute(object parameter)
        {
            Window win = GetTaskbarWindow(parameter);
            return win != null && !win.IsVisible;
        }
    }
}