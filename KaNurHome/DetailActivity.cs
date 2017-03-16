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
using KaNurHome.models.shelters;
using KaNurHome.sharedatas;
using KaNurHome.models.hospitals;
using KaNurHome.xmls;
using KaNurHome.models.layouts;

namespace KaNurHome
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // 防災施設取得
            var shelters = ShelterModels.GetModels(Application.Context)
                .Where(m => FilteringShelters(m, ShareDatas.SelectedNursingHome)).OrderBy(m => m.Distance).ToArray();
            // 病院取得
            var hospitals = HospitalModels.GetBigHospitalModels(Application.Context).Concat(HospitalModels.GetSmallHospitalModels(Application.Context))
                .Where(m => FilteringShelters(m, ShareDatas.SelectedNursingHome)).OrderBy(m => m.Distance).ToArray();


            // Html
            var xDetail = new XDetailHtml(Application.Context);

            xDetail.SetSelectedItem(ShareDatas.SelectedNursingHome);
            xDetail.SetHospitalDatas(hospitals);
            xDetail.SetShelterDatas(shelters);

            lay.JSInterface.RaiseSubmit += () => {
                var intent = new Intent(Application.Context, typeof(MapActivity));
                StartActivity(intent);
            };

            SetContentView(lay.Layout);
            lay.SetHtml(xDetail);
        }
        // フィルタリング
        private bool FilteringShelters(INearPlaces near, NursingHomeModels nurse)
        {
            double nlat = 0, nlng = 0;
            if (!(double.TryParse(nurse.Lat, out nlat) && double.TryParse(nurse.Lng, out nlng)))
            {
                return false;
            }
            near.Distance = (int)models.GeoMath.calcDistHubeny(near.LatVal, near.LngVal, nlat, nlng);
            return near.Distance < 1000;
        }
    }
}