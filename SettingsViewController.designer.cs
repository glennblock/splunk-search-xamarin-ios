// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace SplunkSearch
{
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ConnectButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField HostText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField PasswordText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField PortText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField UserText { get; set; }

		[Action ("ConnectButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void ConnectButton_TouchUpInside (UIButton sender);

		[Action ("HostText_Changed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void HostText_Changed (UITextField sender);

		[Action ("PasswordText_Changed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void PasswordText_Changed (UITextField sender);

		[Action ("PortText_Changed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void PortText_Changed (UITextField sender);

		[Action ("UserText_Changed:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UserText_Changed (UITextField sender);

		void ReleaseDesignerOutlets ()
		{
			if (ConnectButton != null) {
				ConnectButton.Dispose ();
				ConnectButton = null;
			}
			if (HostText != null) {
				HostText.Dispose ();
				HostText = null;
			}
			if (PasswordText != null) {
				PasswordText.Dispose ();
				PasswordText = null;
			}
			if (PortText != null) {
				PortText.Dispose ();
				PortText = null;
			}
			if (UserText != null) {
				UserText.Dispose ();
				UserText = null;
			}
		}
	}
}
