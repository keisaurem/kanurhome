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

namespace KaNurHome.models.shelters
{
    public class ShelterModels : INearPlaces
    {
        [CsvColumn("ID")]
        public string ID { get; set; }
        [CsvColumn("�ܓx")]
        public string Lat { get; set; }
        [CsvColumn("�o�x")]
        public string Lng { get; set; }
        [CsvColumn("����")]
        public string Name { get; set; }

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
                "csv/shelters/14hinan.csv"
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