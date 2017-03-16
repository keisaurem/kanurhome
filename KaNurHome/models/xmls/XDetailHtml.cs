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
using KaNurHome.models.nearplaces;
using KaNurHome.models.nursinghomes;

namespace KaNurHome.models.xmls
{
    public class XDetailHtml : XHtmlModels
    {
        public XDetailHtml(Context context) : base(context) { }

        public void SetHospitalDatas(INearPlaces[] models) {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "hospitaldata_wrapper");
            var xElm = DetailDataBuilder.CreateHtml(models, "‹ß—×‚Ì•a‰@");
            target.Add(xElm);
        }

        public void SetShelterDatas(INearPlaces[] models)
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "disaster_prevent_wrapper");
            var xElm = DetailDataBuilder.CreateHtml(models, "‹ß—×‚Ì–hÐŽ{Ý");
            target.Add(xElm);
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