using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config;
using System.Windows;
namespace Email.WPF {
    public class EmailWPFHelper {
        public static string getEmail(Window parent, ASettings settings) {
            if (settings.email == null) {
                EmailWindow window = new EmailWindow(parent);
                while(window.ShowDialog()==true) {
                    settings.email = window.email;
                    if (settings.email != null) {
                        return settings.email;
                    }
                }
            } else {
                return settings.email;
            }

            return null;
        }
    }
}
