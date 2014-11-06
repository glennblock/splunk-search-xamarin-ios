using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;

namespace SplunkSearch
{

    public class EventCell : UITableViewCell {
        UITextView eventTextView;

        public EventCell (string cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            eventTextView = new UITextView () {
                Font = UIFont.FromName ("Arial",12f),
                Editable=false,
                ScrollEnabled = true,
             };
            ContentView.Add (eventTextView);
        }

        public void UpdateCell(EventViewModel item) {
            eventTextView.Text = item.Text;
        }

        public override void LayoutSubviews ()
        {
            base.LayoutSubviews ();
            eventTextView.Frame = new System.Drawing.RectangleF (5, 4, ContentView.Bounds.Width - 5, 45);
        }
    }
       

}
