using System;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace SplunkSearch
{
    public class ResultsTableSource : UITableViewSource
    {
        List<EventViewModel> _items;
        private const string cellIdentifier = "TableCell";

        public ResultsTableSource (List<EventViewModel> items)
        {
            _items = items;
        }

        public override int RowsInSection (UITableView tableview, int section)
        {
            return _items.Count;
        }

        public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            EventCell cell = (EventCell) tableView.DequeueReusableCell (cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null) {
                cell = new EventCell (cellIdentifier);
            }
            cell.UpdateCell(_items[indexPath.Row]);
            return cell;
        }
    }
}

