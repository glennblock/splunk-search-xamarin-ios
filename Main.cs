using Splunk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;

namespace SplunkSearch
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main (string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) =>
            {
                return true;
            };

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main (args, null, "AppDelegate");
        }

        public static Service Service { get; private set;}

        public static async Task<Service> InitializeService() {
            var service = new Service(Scheme.Https, SettingsViewController.Host, SettingsViewController.Port); 
            await service.LogOnAsync(SettingsViewController.User, SettingsViewController.Password);
            Service = service;
            return service;
        }

        public static IList<EventViewModel> Items {get;set;}

        public static UITabBar TabBar {get;set;}

        public static void EnableChart() {
            TabBar.BeginInvokeOnMainThread (
                delegate {
                    TabBar.Items [2].Enabled = true;
                }
            );
        }

        public static void DisableChart() {
            TabBar.BeginInvokeOnMainThread (
                delegate {
                    TabBar.Items [2].Enabled = false;
                }
            );
        }
    }
}
