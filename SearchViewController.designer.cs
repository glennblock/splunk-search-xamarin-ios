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
	[Register ("SearchViewController")]
	partial class SearchViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIActivityIndicatorView ActivityIndicator { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton CancelButton { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel CountLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextView QueryText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView ResultsTable { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SearchButton { get; set; }

		[Action ("CancelButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void CancelButton_TouchUpInside (UIButton sender);

		[Action ("SearchButton_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void SearchButton_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (ActivityIndicator != null) {
				ActivityIndicator.Dispose ();
				ActivityIndicator = null;
			}
			if (CancelButton != null) {
				CancelButton.Dispose ();
				CancelButton = null;
			}
			if (CountLabel != null) {
				CountLabel.Dispose ();
				CountLabel = null;
			}
			if (QueryText != null) {
				QueryText.Dispose ();
				QueryText = null;
			}
			if (ResultsTable != null) {
				ResultsTable.Dispose ();
				ResultsTable = null;
			}
			if (SearchButton != null) {
				SearchButton.Dispose ();
				SearchButton = null;
			}
		}
	}
}
