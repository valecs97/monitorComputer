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
using System.Windows.Shapes;

namespace Monitor_Computer
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat
    {
        public MainWindow mainWindowClass;
        private int numberOfMessages = 0;
        private string messages="";
        public Chat(MainWindow mainWindowClass,string message,bool fromClient)
        {
            InitializeComponent();
            this.mainWindowClass = mainWindowClass;
            mainWindowClass.chatOpen = true;
            mainWindowClass.chatAvalible = false;
            if (message != "")
                receiveMessage(message, fromClient);
        }

        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (writeTextBox.Text != "")
                {
                    if (messages != "")
                        messages += "\nYou: " + writeTextBox.Text;
                    else
                        messages += "You: " + writeTextBox.Text;
                    if (numberOfMessages<10)
                    {
                        messagesLabel.Content = "";
                        for (int i = 1; i <= 9 - numberOfMessages; i++)
                            messagesLabel.Content += "\n";
                        messagesLabel.Content += messages;
                        numberOfMessages++;
                    }
                    else
                        messagesLabel.Content += "\nYou: " + writeTextBox.Text;
                    messagesScroll.PageDown();
                    if (mainWindowClass.serverStatus == true)
                        mainWindowClass.messageToClient(writeTextBox.Text);
                    else if (mainWindowClass.didItConnect == true)
                        mainWindowClass.messageToServer(writeTextBox.Text);
                    writeTextBox.Text = "";
                }
            }
            catch (Exception)
            {
                
            }
        }

        public void receiveMessage(string message, bool fromClient)
        {
            if (messages != "")
            {
                if (fromClient)
                    messages += "\nClient: ";
                else
                    messages += "\nServer: ";
                messages += message;
            }
            else {
                if (fromClient)
                    messages += "Client: ";
                else
                    messages += "Server: ";
                messages += message;
            }
            if (numberOfMessages < 10)
            {
                messagesLabel.Content = "";
                for (int i = 1; i <= 9 - numberOfMessages; i++)
                    messagesLabel.Content += "\n";
                messagesLabel.Content += messages;
                numberOfMessages++;
            }
            else {
                if (fromClient)
                    messagesLabel.Content += "\nClient: ";
                else
                    messagesLabel.Content += "\nServer: ";
                messagesLabel.Content += message;
            }
            messagesScroll.PageDown();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindowClass.chatOpen = false;
            mainWindowClass.chatAvalible = true;
        }
    }
}
