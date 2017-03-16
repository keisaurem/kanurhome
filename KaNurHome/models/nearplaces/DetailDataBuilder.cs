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
using System.Xml.Linq;

namespace KaNurHome.models.nearplaces
{
    public class DetailDataBuilder
    {
        public static  XElement CreateHtml(INearPlaces[] target, string title)
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
    }
}