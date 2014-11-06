using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Splunk.Client;

namespace SplunkSearch
{
	partial class SettingsViewController : UIViewController
	{
        public SettingsViewController (IntPtr handle) : base (handle)
        {
        }

        public static string Host { get; private set; }
        public static int Port { get; private set; }
        public static string User { get; private set; }
        public static string Password { get; private set; }
 
        public override void ViewDidLoad ()
        {
            LoadSettings ();

            try {
                Application.TabBar = this.TabBarController.TabBar;
                Application.DisableChart ();
            }
            catch {}

            HostText.Text = Host;
            PasswordText.Text = Password;
            UserText.Text = User;
            PortText.Text = Port.ToString();

            HostText.ShouldReturn += (textField) => {
                textField.ResignFirstResponder ();
                return true;
            };

            PortText.ShouldReturn += (textField) => {
                textField.ResignFirstResponder ();
                return true;
            };

            UserText.ShouldReturn += (textField) => {
                textField.ResignFirstResponder ();
                return true;
            };

            PasswordText.ShouldReturn += (textField) => {
                textField.ResignFirstResponder ();
                return true;
            };
        }

        private void LoadSettings() {
            var tempHost = NSUserDefaults.StandardUserDefaults.StringForKey ("Host");
            var tempUser = NSUserDefaults.StandardUserDefaults.StringForKey ("User");
            var tempPassword = NSUserDefaults.StandardUserDefaults.StringForKey ("Password");
            var tempPort = NSUserDefaults.StandardUserDefaults.IntForKey("Port");

            Host = tempHost ?? HostText.Text;
            User = tempUser ?? UserText.Text;
            Password = tempPassword ?? PasswordText.Text;
            Port = (int)(tempPort != 0 ? tempPort : int.Parse(PortText.Text)); 
        }


        partial void HostText_Changed (UITextField sender)
        {
            SettingsViewController.Host = sender.Text;
            NSUserDefaults.StandardUserDefaults.SetString(Host, "Host");
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        partial void PasswordText_Changed (UITextField sender)
        {
            SettingsViewController.Password = sender.Text;
            NSUserDefaults.StandardUserDefaults.SetString(Password, "Password");
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        partial void UserText_Changed (UITextField sender)
        {
            SettingsViewController.User = sender.Text;
            NSUserDefaults.StandardUserDefaults.SetString(User, "Password");
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        partial void PortText_Changed (UITextField sender)
        {
            SettingsViewController.Port = Int32.Parse(sender.Text);
            NSUserDefaults.StandardUserDefaults.SetInt(Port, "Port");
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }

        async partial void ConnectButton_TouchUpInside (UIButton sender)
        {
            UIAlertView alert = null;

            try {
                await Application.InitializeService();
                alert = new UIAlertView("Splunk Search", "Connected", null, "OK");
                alert.Show();
            }
            catch(Exception ex) {
                alert = new UIAlertView("Error", ex.ToString(), null, "OK");
                alert.Show();
            }

        }

	}
}
