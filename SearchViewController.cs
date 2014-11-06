using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Splunk.Client;
using System.Reactive;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SplunkSearch
{
	partial class SearchViewController : UIViewController
	{
        private List<EventViewModel> _items;
        private SearchResultStream _searchResults;
        private IDisposable _subscription;
        private long count = 0;

		public SearchViewController (IntPtr handle) : base (handle)
		{

        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
        }

        public override void TouchesBegan (NSSet touches, UIEvent evt)
        {
            QueryText.ResignFirstResponder ();
        }

        async partial void SearchButton_TouchUpInside (UIButton sender)
        {
            ActivityIndicator.StartAnimating();
            _items = new List<EventViewModel> ();
            Application.Items = _items;
            ResultsTable.Source = null;
            ResultsTable.Source = new ResultsTableSource (_items);

            SearchButton.Enabled = false;
            CancelButton.Enabled = true;
            QueryText.ResignFirstResponder ();
            count = 1;

            UIAlertView alert;
            try {
                var service = Application.Service;
                if (service == null)
                  service = await Application.InitializeService();
                    
                _searchResults = await service.ExportSearchResultsAsync("search " + QueryText.Text);

                var observer = Observer.Create<SearchResult>(
                    r=> {
                        _items.Add(new EventViewModel(r));
                        BeginInvokeOnMainThread(
                            delegate {
                                ResultsTable.ReloadData();
                                CountLabel.Text = string.Format("({0})", count++);
                            }
                        );
                    },
                    ex => {
                        if (ex is TaskCanceledException) {
                            return; 
                        }
                        throw ex;
                    },
                    ()=> {
                        _searchResults.Dispose();
                        BeginInvokeOnMainThread(
                            delegate {
                                SearchButton.Enabled = true;
                                CancelButton.Enabled = false;
                                ActivityIndicator.StopAnimating();
                            }
                        );
                        if (_items.Count > 0) {
                            if (_items[0].IsStats) {
                                Application.EnableChart();
                                return;
                            }
                        }

                        Application.DisableChart();
                    }
                );
                _searchResults.Subscribe(observer);
            }
            catch(Exception ex) {
                alert = new UIAlertView("Error", ex.Message, null, "OK");
                alert.Show();
                SearchButton.Enabled = true;
                CancelButton.Enabled = false;
                ActivityIndicator.StopAnimating();
                return;
            } 
 
        }

        partial void CancelButton_TouchUpInside (UIButton sender)
        {
            SearchButton.Enabled = true;
            CancelButton.Enabled = false;
            ActivityIndicator.StopAnimating();

            if (_subscription != null) {
                try {
                    _subscription.Dispose();
                }catch{};
                _subscription = null;
            }

            if (_searchResults != null) {
                try {
                    _searchResults.Dispose();
                }
                catch{};
                _searchResults = null;
            }
        }
	}
}
