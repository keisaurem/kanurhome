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

namespace KaNurHome.models
{
    public interface INearPlaces
    {
        string Name { get; set; }
        double Distance { get; set; }
        double LatVal { get; }
        double LngVal { get; }
    }
    public static class NearPlaces
    {

        public static XElement CreateHtml(INearPlaces model)
        {
            var xRow = new XElement("tr");

            var xName = new XElement("td");
            xName.SetAttributeValue("class", "margB_5 font_m");
            xName.SetAttributeValue("style", "width: 80%;");
            xName.Value = model.Name;

            var xDistance = new XElement("td");
            xDistance.SetAttributeValue("class", "font_m");
            xDistance.SetAttributeValue("style", "width: 20%;");
            xDistance.Value = model.Distance + " m";

            xRow.Add(xName, xDistance);

            return xRow;
        }

        public static XElement CreateLatLngHtml(INearPlaces model)
        {
            var xWrap = new XElement("div");
            xWrap.SetAttributeValue("class", "sub_latlng");

            var xLat = new XElement("div");
            xLat.SetAttributeValue("class", "lat");
            xLat.Value = model.LatVal.ToString();

            var xLng = new XElement("div");
            xLng.SetAttributeValue("class", "lng");
            xLng.Value = model.LngVal.ToString();

            xWrap.Add(xLat, xLng);

            return xWrap;
        }
    }
}