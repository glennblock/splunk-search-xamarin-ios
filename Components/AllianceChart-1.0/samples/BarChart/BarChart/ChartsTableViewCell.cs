using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BarChart
{
	public class ChartsTableViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("ChartsTableViewCell");

		public ChartsTableViewCell () : base (UITableViewCellStyle.Value1, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			TextLabel.Text = "TextLabel";
		}
	}
}

