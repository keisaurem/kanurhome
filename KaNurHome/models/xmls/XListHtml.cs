using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KaNurHome.models.nursinghomes;
using System.Xml.Linq;

namespace KaNurHome.models.xmls
{
    public class XListHtml : XHtmlModels
    {
        public XListHtml(Context context) : base(context) { }

        public void SetKeywords(string[] keywords, List<string> areaname)
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "content_top");


            var xWrap = new XElement("div");
            xWrap.SetAttributeValue("id", "wrap_keywords");
            xWrap.SetAttributeValue("class", "pad_15 ovhid");

            var xButtons = keywords.Select(m =>
            {
                var xButton = new XElement("div");
                xButton.SetAttributeValue("class", "toggle_button");
                xButton.Value = m;
                return xButton;
            }).ToArray();

            var xTitle = new XElement("div");
            xTitle.Value = "iž‚ÝðŒ";
            xTitle.SetAttributeValue("class", "margB_10");
            xWrap.Add(xTitle);
            xWrap.Add(xButtons);


            var xWrapArea = new XElement("div");
            xWrapArea.SetAttributeValue("id", "wrap_area");
            xWrapArea.SetAttributeValue("class", "pad_15 ovhid");

            var xTitleArea = new XElement("div");
            xTitleArea.Value = "iž‚ÝƒGƒŠƒA";
            xTitleArea.SetAttributeValue("class", "margB_10");

            var xButtonAreas = areaname.Select(m =>
            {
                var xButtonArea = new XElement("div");
                xButtonArea.SetAttributeValue("class", "toggle_button");
                xButtonArea.Value = m;
                return xButtonArea;
            }).ToArray();


            xWrapArea.Add(xTitleArea);
            xWrapArea.Add(xButtonAreas);

            target.Add(xWrap);
            target.Add(xWrapArea);
        }

        public void SetListItems(NursingHomeModels[] models)
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "home_list");

            target.Add(models.Select(m => m.ToHtml()).ToArray());
        }

        protected override string GetHtmlFileName()
        {
            return "html/list.html";
        }
    }
}