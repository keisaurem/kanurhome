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
using KaNurHome.enums;
using KaNurHome.attributes;
using System.Xml.Linq;
using KaNurHome.models.xmls;

namespace KaNurHome.models.nursinghomes
{
    public class NursingHomeModels
    {
        public NursingTypes Type { get; set; }

        [CsvColumn("Ž–‹ÆŽÒ”Ô†")]
        public string ID { get; set; }
        [CsvColumn("Ž–‹ÆŽÒ–¼")]
        public string Name { get; set; }
        [CsvColumn("ZŠ")]
        public string Address { get; set; }
        [CsvColumn("“d˜b")]
        public string Tel { get; set; }
        [CsvColumn("ˆÜ“x")]
        public string Lat { get; set; }
        [CsvColumn("Œo“x")]
        public string Lng { get; set; }

        public int Rating { get; set; }


        public XElement ToHtml()
        {
            var xWrapItem = new XElement("div");
            xWrapItem.SetAttributeValue("id", this.ID);
            xWrapItem.SetAttributeValue("class", "card_button ripple_target ovhid");

            xWrapItem.Add(ToHtmlForInner(), XHtmlModels.RippleEffectItem);

            return xWrapItem;
        }

        public XElement ToHtmlForInner(bool writecolor = false)
        {
            var typeAttr = (NursingTypeAttribute)Attribute.GetCustomAttribute(typeof(NursingTypes).GetField(this.Type.ToString()), typeof(NursingTypeAttribute));

            var xTable = new XElement("table");
            if (writecolor) xTable.SetAttributeValue("style", "color:#fff;");
            var xRow = new XElement("tr");
            var xCell1 = new XElement("td");
            xCell1.SetAttributeValue("style", "width: 20%;" + (writecolor ? " color:#fff;" : ""));
            var xCell2 = new XElement("td");
            xCell2.SetAttributeValue("style", "width: 80%;" + (writecolor ? " color:#fff;" : ""));

            var xWrapDatas = new XElement("div");
            xWrapDatas.SetAttributeValue("class", "pad_15");

            var xIcon = new XElement("img");
            xIcon.SetAttributeValue("class", "fl");
            xIcon.SetAttributeValue("style", "width:90%; padding: 5%;");
            xIcon.SetAttributeValue("src", typeAttr.Icon);

            xCell1.Add(xIcon);

            var xName = new XElement("div");
            xName.SetAttributeValue("class", "font_l margB_15 nursinghomename");
            if (writecolor) xName.SetAttributeValue("style", "color:#fff;");
            xName.Value = this.Name;

            //var xPost = new XElement("div");
            //xPost.Value = this.Post;
            var xType = new XElement("div");
            xType.SetAttributeValue("class", "margB_10");
            if (writecolor) xType.SetAttributeValue("style", "color:#fff;");
            xType.Value = typeAttr.Text;

            var xAddress = new XElement("div");
            if (writecolor) xAddress.SetAttributeValue("style", "color:#fff;");
            xAddress.Value = this.Address;

            var xTel = new XElement("div");
            if (writecolor) xTel.SetAttributeValue("style", "color:#fff;");
            xTel.Value = "‡„ " + this.Tel;

            var xRating = new XElement("div");
            xRating.SetAttributeValue("class", "margT_10 font_s");
            if (writecolor) xRating.SetAttributeValue("style", "color:#fff;");

            if (typeAttr.Categories.Contains(NursingCategories.Helper))
            {
                xRating.Value = "–K–â‰îŒì‚Ì‚½‚ßA–hÐ•]‰¿‚Í‚ ‚è‚Ü‚¹‚ñ";
            }
            else
            {
                xRating.Value = "–hÐ•]‰¿ " + new string(Enumerable.Range(0, this.Rating).Select(m => 'š').ToArray());
            }

            xCell2.Add(xName, xType, xAddress, xTel, xRating);

            xRow.Add(xCell1, xCell2);
            xTable.Add(xRow);
            xWrapDatas.Add(xTable);

            return xWrapDatas;
        }

        //public static NursingHomeModels[] GetModels(Context context)
        //{
        //    var sources = typeof(NursingTypes).GetFields().AsParallel().Where(m => m.IsStatic).Select(m =>
        //    {
        //        var attr = (NursingTypeAttribute) Attribute.GetCustomAttribute(m, typeof(NursingTypeAttribute));
        //        return new Tuple<NursingTypes, string>((NursingTypes)m.GetValue(null), attr.CsvName);
        //    });
        //    var shelterSourcve = sources.AsParallel().SelectMany(m =>
        //    {
        //        using (var am = context.Resources.Assets.Open(m.Item2))
        //        {
        //            var dict = CsvLoader.GetCsvDictionaly(am);
        //            var ret = new List<NursingHomeModels>();
        //            for (var i = 0; i < dict.First().Value.Count; ++i)
        //            {
        //                var model = new NursingHomeModels();
        //                var members = model.GetType().GetProperties();
        //                foreach (var mem in members)
        //                {
        //                    var memAttr = (CsvColumnAttribute) Attribute.GetCustomAttribute(mem, typeof(CsvColumnAttribute));
        //                    if (memAttr == null) continue;
        //                    model.GetType().GetProperty(mem.Name).SetValue(model, dict[memAttr.Column][i]);
        //                }
        //                model.Type = m.Item1;
        //                ret.Add(model);
        //            }
        //            return ret;
        //        }
        //    }).GroupBy(m => m.ID).Select(m => m.First());

        //    return shelterSourcve.ToArray();
        //}

        public static NursingHomeModels[] GetModels(Context context, NursingTypes[] types)
        {
            var sources = types.AsParallel().Select(m =>
            {
                var f = typeof(NursingTypes).GetField(m.ToString());
                var attr = (NursingTypeAttribute)Attribute.GetCustomAttribute(f, typeof(NursingTypeAttribute));
                return new Tuple<NursingTypes, string>((NursingTypes)f.GetValue(null), attr.CsvName);
            });
            var shelterSourcve = sources.AsParallel().SelectMany(m =>
            {
                using (var am = context.Resources.Assets.Open(m.Item2))
                {
                    var dict = CsvLoader.GetCsvDictionaly(am);
                    var ret = new List<NursingHomeModels>();
                    for (var i = 0; i < dict.First().Value.Count; ++i)
                    {
                        var model = new NursingHomeModels();
                        var members = model.GetType().GetProperties();
                        foreach (var mem in members)
                        {
                            var memAttr = (CsvColumnAttribute)Attribute.GetCustomAttribute(mem, typeof(CsvColumnAttribute));
                            if (memAttr == null) continue;
                            model.GetType().GetProperty(mem.Name).SetValue(model, dict[memAttr.Column][i]);
                        }
                        model.Type = m.Item1;
                        ret.Add(model);
                    }
                    return ret;
                }
            }).GroupBy(m => m.ID).Select(m => m.First());

            return shelterSourcve.ToArray();
        }


        public XElement ToLatLngHtml()
        {
            var xWrap = new XElement("div");
            xWrap.SetAttributeValue("class", "sub_latlng");

            var xLat = new XElement("div");
            xLat.SetAttributeValue("id", "lat");
            xLat.Value = Lat.ToString();

            var xLng = new XElement("div");
            xLng.SetAttributeValue("id", "lng");
            xLng.Value = Lng.ToString();

            xWrap.Add(xLat, xLng);

            return xWrap;
        }
    }
}