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
using KaNurHome.models.layouts;
using KaNurHome.xmls;
using KaNurHome.models.nursinghomes;
using KaNurHome.sharedatas;
using System.Threading.Tasks;
using KaNurHome.models;
using System.Collections;
using KaNurHome.models.shelters;
using KaNurHome.attributes;

namespace KaNurHome
{
    [Activity(Label = "ListActivity")]
    public class ListActivity : Activity
    {
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            IEnumerable<NursingHomeModels> nursinghomes = NursingHomeModels.GetModels(Application.Context, ShareDatas.SelectedNursingTypes);

            if (ShareDatas.SelectedHospitalModels.Count != 0)
            {
                var lstNursings = new List<NursingHomeModels>();
                Parallel.ForEach(ShareDatas.SelectedHospitalModels, item =>
                {
                    var items = nursinghomes.Where(m =>
                    {
                        double nlat = 0, nlng = 0;
                        if (!(double.TryParse(m.Lat, out nlat) && double.TryParse(m.Lng, out nlng)))
                        {
                            return false;
                        }
                        return GeoMath.calcDistHubeny(item.LatVal, item.LngVal, nlat, nlng) < 1000;
                    });
                    lock (((ICollection)lstNursings).SyncRoot)
                    {
                        lstNursings.AddRange(items);
                    }
                });
                nursinghomes = lstNursings;
            }

            if (ShareDatas.Addresses.Count != 0)
                nursinghomes = nursinghomes.Where(m => ShareDatas.Addresses.Where(n => m.Address.Contains(n)).Count() > 0);

            var shelters = ShelterModels.GetModels(Application.Context);

            Parallel.ForEach(nursinghomes, item =>
            {
                var shelcount = shelters.Where(n => FilteringShelters(n, item)).Count();
                var rate = (short)(shelcount);
                if (shelcount == 0) rate = 0;
                item.Rating = rate;
            });
            nursinghomes = nursinghomes.OrderByDescending(m => m.Rating).Distinct();

            var xList = new XListHtml(Application.Context);
            var keywords = ShareDatas.SelectedNursingTypes.Select(m =>
            {
                var attr = (NursingTypeAttribute)Attribute.GetCustomAttribute(
                    typeof(NursingTypes).GetField(m.ToString()), typeof(NursingTypeAttribute));
                return attr.Text;
            }).Concat(ShareDatas.SelectedHospitalModels.Select(m => m.Name)).Concat(ShareDatas.Addresses).ToArray();

            xList.SetListItems(nursinghomes.ToArray());
            xList.SetKeywords(keywords);

            lay.JSInterface.SelectedItem += itemID => {
                ShareDatas.SelectedNursingHome = nursinghomes.Single(m => m.ID == itemID);
                var intent = new Intent(Application.Context, typeof(DetailActivity));
                StartActivity(intent);
            };

            SetContentView(lay.Layout);
            lay.SetHtml(xList);
        }

        // フィルタリング
        private bool FilteringShelters(INearPlaces near, NursingHomeModels nurse)
        {
            double nlat = 0, nlng = 0;
            if (!(double.TryParse(nurse.Lat, out nlat) && double.TryParse(nurse.Lng, out nlng)))
            {
                return false;
            }
            return (int)models.GeoMath.calcDistHubeny(near.LatVal, near.LngVal, nlat, nlng) < 500;
        }
    }
}