using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Collections.Generic;
using MonoTouch.CoreText;

namespace BarChart
{
    public class XMLElement
    {
        protected ArrayList children;
        protected NSMutableDictionary attributes;
        protected XMLElement parent;
        protected string name;
        protected string innerText;
        public ArrayList Children
        {
            get
            {
                return children;
            }
            set
            {
                children = value;
            }
        }

        public NSMutableDictionary Attributes
        {
            get
            {
                return attributes;
            }
            set
            {
                attributes = value;
            }
        }

        public XMLElement Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string InnerText
        {
            get
            {
                return innerText;
            }
            set
            {
                innerText = value;
            }
        }

        void init()
        {
            return this.initWithName(null);
        }

        public void FunctionName(string aname)
        {
            if (this = base.init())
            {
                name = aname;
                children = new ArrayList();
                attributes = new NSMutableDictionary();
                innerText = new NSMutableString();
            }

            return this;
        }

        public bool HasAttribute(string aname)
        {
            return (attributes.ObjectForKey(aname) != null);
        }

        public string GetAttribute(string aname)
        {
            return attributes.ObjectForKey(aname);
        }

        public void SetAttributeValue(string aname, string value)
        {
            attributes.SetValueForKey(value, aname);
        }

        string EscapedXmlStringReplaceQuotes(string theString, bool replaceQuotes)
        {
            NSMutableString ms = NSMutableString.TheStringWithString(theString);
            ms.ReplaceOccurrencesOfStringWithStringOptionsRange("&", "&amp;", 0, NSMakeRange(0, ms.Length()));
            ms.ReplaceOccurrencesOfStringWithStringOptionsRange("<", "&lt;", 0, NSMakeRange(0, ms.Length()));
            ms.ReplaceOccurrencesOfStringWithStringOptionsRange(">", "&gt;", 0, NSMakeRange(0, ms.Length()));
            if (replaceQuotes) ms.ReplaceOccurrencesOfStringWithStringOptionsRange("\"", "&quot;", 0, NSMakeRange(0, ms.Length()));

            return ms;
        }

        public XMLElement GetChild(string childName)
        {
            foreach (XMLElement el in children) if (el.Name.IsEqual(childName)) return el;

            return null;
        }

        public XMLElement AppendElement(string aname)
        {
            XMLElement el = new XMLElement();
            el.Name = aname;
            el.Parent = this;
            this.Children.Add(el);
            return el;
        }

        public XMLElement AppendElementWithText(string aname, string text)
        {
            XMLElement el = this.AppendElement(aname);
            el.InnerText = text;
            return el;
        }

        public string ChildText(string childName)
        {
            foreach (XMLElement el in children) if (el.Name.IsEqual(childName)) return el.InnerText;

            return null;
        }

        public string CopyOuterXml()
        {
            NSMutableString xml = new NSMutableString();
            xml.AppendFormat("<%@", name);
            foreach (string aname in attributes)
            {
                string avalue = this.EscapedXmlStringReplaceQuotes(attributes.ObjectForKey(aname), true);
                xml.AppendFormat(" %@=\"%@\"", aname, avalue);
            }
            if ((innerText != null && innerText.Length() > 0) || (children.Count > 0)) xml.AppendString(">");
            else
            {
                xml.AppendString("/>");
                return xml;
            }

            if (innerText != null && innerText.Length() > 0)
            {
                string avalue = this.EscapedXmlStringReplaceQuotes(innerText, false);
                xml.AppendString(avalue);
            }

            for (int i = 0; i < children.Count; i++)
            {
                string childXml = ((XMLElement)children[i]).CopyOuterXml();
                xml.AppendString(childXml);
            }

            xml.AppendFormat("</%@>", name);
            return xml;
        }

        public XMLElement RootElement()
        {
            if (parent == null) return this;
            else return parent.RootElement();

        }

        public XMLElement FirstChild()
        {
            if (children.Count > 0) return children[0];
            else return null;

        }

    }
}

