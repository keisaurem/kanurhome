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
using KaNurHome.attributes;

namespace KaNurHome.models.nearplaces
{
    public class ShelterModels : INearPlaces
    {
        [CsvColumn("施設名称")]
        public string Name { get; set; }
        [CsvColumn("所在地")]
        public string Address { get; set; }
        [CsvColumn("緯度")]
        public string Lat { get; set; }
        [CsvColumn("経度")]
        public string Lng { get; set; }

        public double Distance { get; set; }

        public double LatVal
        {
            get
            {
                double val = 0;
                double.TryParse(Lat, out val);
                return val;
            }
        }

        public double LngVal
        {
            get
            {
                double val = 0;
                double.TryParse(Lng, out val);
                return val;
            }
        }

        public static ShelterModels[] GetModels(Context context)
        {

            var csvNames = new string[] {
                "csv/shelters/shelter_01.csv",
                "csv/shelters/shelter_02.csv",
                "csv/shelters/shelter_03.csv"
            };

            var shelterSources = csvNames.AsParallel().SelectMany(m =>
            {
                using (var am = context.Resources.Assets.Open(m))
                {
                    var dict = CsvLoader.GetCsvDictionaly(am);
                    var ret = new List<ShelterModels>();
                    for (var i = 0; i < dict.First().Value.Count; ++i)
                    {
                        var model = new ShelterModels();
                        var members = model.GetType().GetProperties();
                        foreach (var mem in members)
                        {
                            var mamAttr = (CsvColumnAttribute)System.Attribute.GetCustomAttribute(mem, typeof(CsvColumnAttribute));
                            if (mamAttr == null) continue;
                            model.GetType().GetProperty(mem.Name).SetValue(model, dict[mamAttr.Column][i]);
                        }
                        ret.Add(model);
                    }
                    return ret;
                }
            });

            return shelterSources.ToArray();
        }
    }
}