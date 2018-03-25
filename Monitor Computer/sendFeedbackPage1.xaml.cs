using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monitor_Computer
{
    /// <summary>
    /// Interaction logic for sendFeedbackPage1.xaml
    /// </summary>
    public partial class sendFeedbackPage1 : Page
    {
        sendFeedback sendFeedbackClass;
        public sendFeedbackPage1(sendFeedback sendFeedbackClass)
        {
            this.sendFeedbackClass = sendFeedbackClass;
            InitializeComponent();
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            if (fromTextBox.Text != "" && titleTextBox.Text != "" && subjectTextBox.Text != "")
            {
                sendFeedbackClass.from = fromTextBox.Text;
                sendFeedbackClass.title = titleTextBox.Text + ' ' + sendFeedbackClass.mainWindowClass.myIpAdress.ToString();
                sendFeedbackClass.subject = subjectTextBox.Text;
                sendFeedbackClass.startSending();
            }
            else
                fieldNotComplete();
        }

        private async void fieldNotComplete()
        {
            await sendFeedbackClass.ShowMessageAsync("Error", "Some fields are empty");
        }
    }
}
