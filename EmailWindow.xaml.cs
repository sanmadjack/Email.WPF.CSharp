﻿using System.Windows;
using System.Windows.Controls;
using Translator.WPF;
namespace Email.WPF {
    /// <summary>
    /// Interaction logic for EmailWindow.xaml
    /// </summary>
    public partial class EmailWindow : Window {
        public EmailWindow(Window owner) {
            InitializeComponent();
            TranslationHelpers.translateWindow(this);
            this.Icon = owner.Icon;
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }


        private void button2_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;

        }
        public string email {
            get {
                return emailTxt.Text;
            }
        }

        private void emailTxt_TextChanged(object sender, TextChangedEventArgs e) {
            saveBtn.IsEnabled = EmailHandler.validateEmailAddress(emailTxt.Text);
        }
    }
}
