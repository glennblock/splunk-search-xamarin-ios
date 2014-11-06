using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Newtonsoft.Json;
using Alliance.Charts;

namespace BarChart
{
	public partial class LineChartViewController : UIViewController
	{

		public PCLineChartView LineChartView;

		public LineChartViewController () : base ("LineChartViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			LoadPieChartView ();

			// Perform any additional setup after loading the view, typically from a nib.
		}


//		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
//		{
//			base.DidRotate (fromInterfaceOrientation);
//
////			this.View.SetNeedsDisplay ();
//			LoadPieChartView ();
//		}

		public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate (toInterfaceOrientation, duration);
			LoadPieChartView ();
		}

		public void LoadPieChartView()
		{
		
			if (LineChartView != null) {

				LineChartView.RemoveFromSuperview ();
			}

			this.View.BackgroundColor = UIColor.White;
			this.Title = "Line Chart";

			LineChartView = new PCLineChartView (new RectangleF(10,10,this.View.Bounds.Size.Width-20,this.View.Bounds.Size.Height-20));
			LineChartView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			LineChartView.MinValue = -40;
			LineChartView.MaxValue = 100;
			this.View.AddSubview (LineChartView);
			DataBag sampleInfo = new DataBag();

			using (StreamReader r = new StreamReader ("sample_linechart_data.json")) {

				string json = r.ReadToEnd ();
				sampleInfo = JsonConvert.DeserializeObject<DataBag> (json);

			}

			List<PCLineChartViewComponent> components = new List<PCLineChartViewComponent>();
			for (int i = 0; i < sampleInfo.data.Count; i++) {
				try
				{
					DataSet point = sampleInfo.data[i];
					PCLineChartViewComponent component = new PCLineChartViewComponent ();
					component.Title = point.title;
					component.Points = point.data;
					component.ShouldLabelValues = false;

					if (i == 0) {
						component.Colour = UIColor.FromRGBA (1.0f, 220f / 255.0f, 0.0f, 1.0f);
					} 
					else if (i == 1) {
						component.Colour = UIColor.FromRGBA (0.0f, 153f / 255.0f, 204f / 255.0f, 1.0f);
					}
					else if (i == 2) {
						component.Colour = UIColor.FromRGBA (1.0f, 153f / 255.0f, 51f / 255.0f, 1.0f);
					}
					else if (i == 3) {
						component.Colour = UIColor.FromRGBA (153f / 255.0f, 204f / 255.0f, 51f / 255.0f, 1.0f);
					}
					else if (i == 4) {
						component.Colour = UIColor.FromRGBA (1.0f, 51f / 255.0f, 51f / 255.0f, 1.0f);
					}

					components.Add(component);

				}
				catch (Exception ex) {
					string str = ex.Message;
				}
			}

			LineChartView.Components = components;
			LineChartView.XLabels = sampleInfo.x_labels;

		}

	}

	public class DataSet
	{
		public string title {get;set;}
		public List<int?> data {get;set;}
	}

	public class DataBag
	{
		public List<DataSet> data { get; set;}
		public List<int?> x_labels {get;set;}
	}
}

