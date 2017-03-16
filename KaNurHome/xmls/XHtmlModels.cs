using Android.Content;
using System.Xml.Linq;

namespace KaNurHome.xmls
{
    public abstract class XHtmlModels
    {
        private XDocument _HtmlSoruce;
        protected XDocument HtmlSource
        {
            get
            {
                return _HtmlSoruce;
            }
        }

        public XHtmlModels(Context context)
        {
            var htmlName = GetHtmlFileName();
            using (var am = context.Resources.Assets)
            {
                using (var stream = am.Open(htmlName))
                {
                    _HtmlSoruce = XDocument.Load(stream);
                }
            }
        }

        protected abstract string GetHtmlFileName();

        public override string ToString()
        {
            return _HtmlSoruce.ToString();
        }

        public static XElement RippleEffectItem
        {
            get
            {
                var spanRipple = new XElement("span");
                spanRipple.SetAttributeValue("class", "ripple_effect");
                return spanRipple;
            }
        }
    }
}