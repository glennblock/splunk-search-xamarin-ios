using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using OxyPlot.XamarinIOS;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System.Linq;

namespace SplunkSearch
{
	partial class ChartViewController : UIViewController
	{   
		public ChartViewController (IntPtr handle) : base (handle)
		{
		}
           
        public override void ViewDidAppear (bool animated)
        {
            base.ViewDidAppear (animated);
            try {
                var model = CreateStackedBar();
                model.Padding = new OxyThickness(10,20,0,70);
                var plotView = new PlotView ();
                plotView.Model = model;
                this.View = plotView;
                plotView.SetNeedsDisplay ();
            }
            catch {
            }
        }
            
        private PlotModel CreatePie()
        {
            var model = new PlotModel ();

            var ps = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 0,
            };

            // http://www.nationsonline.org/oneworld/world_population.htm
            // http://en.wikipedia.org/wiki/Continent

            foreach (var viewModel in Application.Items) {
                ps.Slices.Add (new PieSlice (viewModel.Fields [0], double.Parse (viewModel.Fields [1])) { IsExploded = true });
            }
            model.Series.Add(ps);
            var series = (PieSeries)model.Series [0];
            series.InsideLabelColor = OxyColors.White;

            return model;
        }

        private PlotModel CreateStackedBar() 
        {
            var model = new PlotModel
            {
                LegendItemAlignment = HorizontalAlignment.Left,
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.RightBottom,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,
                LegendFontSize = 12,
                LegendFont = "Verdana"
            };

            Dictionary<string, BarSeries> seriesDictionary = new Dictionary<string, BarSeries> ();
            bool first = true;

            var categoryAxis = new CategoryAxis {Title="_time", TitlePosition = .5, Position = AxisPosition.Left };

            foreach (var viewModel in Application.Items) {
                foreach (var field in viewModel.Fields.AllKeys.Where(f=>!f.StartsWith("_"))) {
                    if (first) {
                        var barSeries = new BarSeries {
                            Title = field,
                            IsStacked = true,
                            StrokeColor = OxyColors.Black,
                            StrokeThickness = 1
                        };
                        seriesDictionary [field] = barSeries;
                    }
                    var series = seriesDictionary [field];
                    var val = float.Parse (viewModel.Fields [field]);
                    series.Items.Add(new BarItem { Value = val});

                }
                first = false;
 
                var time = viewModel.Fields ["_time"];
                categoryAxis.Labels.Add (time.Substring (0, 10));
            }

            foreach (var series in seriesDictionary.Values) {
                model.Series.Add (series);
            }
                
            model.Axes.Add(categoryAxis);
            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0};
            model.Axes.Add(valueAxis);

            return model;
        }
    }
}