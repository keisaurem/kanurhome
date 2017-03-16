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
using KaNurHome.models;
using KaNurHome.models.nursinghomes;
using System.Xml.Linq;

namespace KaNurHome.xmls
{
    public class XDetailHtml : XHtmlModels
    {
        public XDetailHtml(Context context) : base(context) { }

        public void SetHospitalDatas(INearPlaces[] models)
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "hospitaldata_wrapper");
            var xElm = CreateHtml(models, "‹ß—×‚Ì•a‰@");
            target.Add(xElm);
        }

        public void SetShelterDatas(INearPlaces[] models)
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "disaster_prevent_wrapper");
            var xElm = CreateHtml(models, "‹ß—×‚Ì–hÐŽ{Ý");
            target.Add(xElm);
        }

        private XElement CreateHtml(INearPlaces[] target, string title)
        {
            if (target == null) target = new INearPlaces[] { };
            var xWrap = new XElement("div");
            xWrap.SetAttributeValue("class", "fl pad_10 margL_15");

            var xTitle = new XElement("div");
            xTitle.SetAttributeValue("class", "font_l");
            xTitle.Value = title + " : " + target.Length;

            var xWrapTable = new XElement("div");
            xWrapTable.SetAttributeValue("class", "margT_15, margL_15");

            var xTable = new XElement("table");
            var xRows = target.Select(m => NearPlaces.CreateHtml(m)).ToArray();

            xTable.Add(xRows);
            xWrapTable.Add(xTable);
            xWrap.Add(xTitle, xWrapTable);

            return xWrap;
        }

        public void SetSelectedItem(NursingHomeModels model)
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "content_top");
            var latlng = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "latlng");
            var xElm = model.ToHtmlForInner(true);
            var xLatLng = model.ToLatLngHtml();

            target.Add(xElm);
            latlng.Add(xLatLng);
        }

        protected override string GetHtmlFileName()
        {
            return "html/detail.html";
        }
    }
}