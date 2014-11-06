using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Alliance.Charts;

namespace BarChart
{
	public partial class PieChartViewController : UIViewController
	{
		public PieChartViewController () : base ("PieChartViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.View.BackgroundColor = UIColor.White;
			this.Title = "Pie Chart";

			int height = (int)(this.View.Bounds.Size.Width / 3 * 2);
			int width = (int)(this.View.Bounds.Size.Width);
			PCPieChart pieChart = new PCPieChart (new RectangleF((this.View.Bounds.Size.Width - width)/2,(this.View.Bounds.Size.Height-height)/2,width,height));

			pieChart.Diameter = width / 2;
			pieChart.SameColorLabel = false;

			pieChart.TitleFont = UIFont.FromName ("HelveticaNeue-Bold",15f);
			pieChart.PercentageFont = UIFont.FromName ("HelveticaNeue-Bold",15f);

			List<PCPieComponent> components = new List<PCPieComponent>();


			PCPieComponent component = new PCPieComponent("AAA",12);
			component.Colour = UIColor.FromRGBA (1.0f, 220f / 255.0f, 0.0f, 1.0f);
			components.Add (component);

			PCPieComponent component1 = new PCPieComponent("BBB",10);
			component1.Colour = UIColor.FromRGBA (0.0f, 153f / 255.0f, 204f / 255.0f, 1.0f);
			components.Add (component1);

			PCPieComponent component2 = new PCPieComponent("CCC",10);
			component2.Colour = UIColor.FromRGBA (153f / 255.0f, 204f / 255.0f, 51f / 255.0f, 1.0f);
			components.Add (component2);

			PCPieComponent component3 = new PCPieComponent("DDD",40);
			component3.Colour = UIColor.FromRGBA (1.0f, 153f / 255.0f, 51f / 255.0f, 1.0f);
			components.Add (component3);

			PCPieComponent component4 = new PCPieComponent("EEE",40);
			component4.Colour = UIColor.FromRGBA (1.0f, 51f / 255.0f, 51f / 255.0f, 1.0f);
			components.Add (component4);

			pieChart.Components = components;

			this.View.AddSubview (pieChart);
			pieChart.SetNeedsDisplay ();
		}
	}
}

