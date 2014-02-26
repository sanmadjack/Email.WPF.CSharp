using System;
using System.Windows;
using System.Windows.Controls;
using Translator;
namespace Email.WPF {
    /// <summary>
    /// Interaction logic for EmailButton.xaml
    /// </summary>
    public partial class EmailButton : UserControl {
        public string From { get; set; }
        public string To { get; set; }
        public IEmailSource Source = null;
        public string Subject { get; set; }
        public string Message { get; set; }


        public bool Sent { get; protected set; }

        public EmailButton() {
            InitializeComponent();
            EmailHandler email = new EmailHandler("sanmadjack@gmail.com", "masgau@masgau.org");
            button.Text = Strings.GetLabelString("CheckingConnection");
            email.checkAvailability(checkAvailabilityDone);
            Sent = false;
        }
        private void checkAvailabilityDone(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            if ((EmailResponse)e.Result == EmailResponse.ServerReachable) {
                button.IsEnabled = true;
                button.Text = Strings.GetLabelString("SendReport");
            } else {
                button.IsEnabled = false;
                button.Text = Strings.GetLabelString("CantSendReport");
                button.ToolTip = Strings.GetToolTipString("CantSendReport");
            }
        }
        private void button_Click(object sender, RoutedEventArgs e) {
            if (!checkEmails())
                return;

            button.IsEnabled = false;
            button.Text = Strings.GetLabelString("SendingReport");

            EmailHandler email = new EmailHandler(this.From, this.To);
            email.sendEmail(this.Subject, this.Message, sendEmailDone);
        }


        private void sendEmailDone(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                button.Text = Strings.GetLabelString("SendFailed");
                button.ToolTip = Strings.GetToolTipString("SendFailed", e.Error.Message);
            } else {
                button.Text = Strings.GetLabelString("ReportSent");
                Sent = true;
            }
            button.IsEnabled = false;
        }

        private bool checkEmails() {
            Window parentWindow = Window.GetWindow(this);
            if (Source != null) {
                if (String.IsNullOrEmpty(To))
                    To = Source.EmailRecipient;

                if (String.IsNullOrEmpty(From))
                    From = Source.EmailSender;

                if (String.IsNullOrEmpty(From)) {
                    EmailWindow window = new EmailWindow(parentWindow);
                    while (window.ShowDialog() == true) {
                        Source.EmailSender = window.email;
                        if (!String.IsNullOrEmpty(Source.EmailSender)) {
                            From = Source.EmailSender;
                            return true;
                        }
                    }
                    return false;
                }

            } else {
                if (String.IsNullOrEmpty(To) || String.IsNullOrEmpty(From)) {
                    throw new  Exception("There source for e-mail data is not found. Please report this via e-mail to sanmadjack@gmail.com");
                }
            }
            return true;
        }

    }
}
