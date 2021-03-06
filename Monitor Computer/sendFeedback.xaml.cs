﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using MahApps.Metro.Controls.Dialogs;
using System.Threading;
using System.Windows.Threading;

namespace Monitor_Computer
{
    /// <summary>
    /// Interaction logic for sendFeedback.xaml
    /// </summary>
    public partial class sendFeedback
    {
        public string from,title,subject;
        public MainWindow mainWindowClass;
        public sendFeedback(MainWindow mainWindowClass)
        {
            this.mainWindowClass = mainWindowClass;
            InitializeComponent();
            sendFeedbackWindow.Navigate(new sendFeedbackPage1(this));
        }

        public void startSending()
        {
            sendFeedbackWindow.Navigate(new sendFeedbackPage2());
            Thread trySendEmailThread = new Thread(trySendEmail);
            trySendEmailThread.Start();
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void trySendEmail()
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 0;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("computermonitorvalecs97@gmail.com", Base64Decode("VGhpcyBpcyBub3QgeW91ciB1c3VhbCB0eXBlIG9mIHBhc3N3b3Jk"));
                MailMessage mm = new MailMessage("donotreply@domain.com", "nightfuryva@gmail.com", from + " - " + title, subject);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                client.Send(mm);
                mainWindowClass.succesSend();
                Dispatcher.Invoke(new Action(() => Close()));
            }
            catch (Exception)
            {
                mainWindowClass.unsuccesSend();
                Dispatcher.Invoke(new Action(() => Close()));
            }
        }
    }
}
