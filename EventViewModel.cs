using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Collections.Specialized;
using Splunk.Client;
using System.Text;
using System.Xml;

namespace SplunkSearch
{
    public class EventViewModel {
        private object _lock = new object();
        private SearchResult _result;

        public EventViewModel (SearchResult result)
        {
            _result = result;
            object rawValue = result.GetValue ("_raw");
                
            if (rawValue == null) {
                IsStats = true;
                //is stats
                var builder = new StringBuilder();
                foreach(var fieldName in result.FieldNames) {
                    object val = result.GetValue(fieldName);
                    var str = val.ToString ();
                    builder.AppendFormat("{0}:{1} ", fieldName, str);
                }
                Text = builder.ToString();
            }
            else {
                var raw = rawValue.ToString ();
                Text = raw; 
            }
        }

        public string Text {get;private set;}
        public bool IsStats { get; private set; }

        private NameValueCollection _fields;
        public NameValueCollection Fields {
            get {
                if (_fields == null) {
                    lock (_lock) {
                        _fields = new NameValueCollection();
                        foreach (var fieldName in _result.FieldNames) {
                            if (fieldName != "_raw") {
                                var val = _result.GetValue (fieldName) as string;
                                if (val != null) {
                                    _fields [fieldName] = val;
                                }
                            }
                        }
                    }
                }
                return _fields;
            }
        }
    }
}
