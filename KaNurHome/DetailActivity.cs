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
using KaNurHome.models.nearplaces;
using KaNurHome.models.nursinghomes;
using KaNurHome.models.xmls;

namespace KaNurHome
{
    [Activity(Label = "DetailActivity")]
    public class DetailActivity : Activity
    {
        //���C�A�E�g
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // �h�Ў{�ݎ擾
            INearPlaces[] shelters = ShelterModels.GetModels(Application.Context)
                .Where(m => FilteringShelters(m, ListActivity.SelectedItem)).OrderBy(m => m.Distance).ToArray();
            INearPlaces[] openShelters = OpenDataShelterModels.GetModels(Application.Context)
                .Where(m => FilteringShelters(m, ListActivity.SelectedItem)).OrderBy(m => m.Distance).ToArray();

            // �a�@�擾
            var hospitals = HospitalModels.GetModels(Application.Context)
                .Where(m => FilteringShelters(m, ListActivity.SelectedItem)).OrderBy(m => m.Distance).ToArray();

            // Html
            var xDetail = new XDetailHtml(Application.Context);

            xDetail.SetSelectedItem(ListActivity.SelectedItem);
            xDetail.SetHospitalDatas(hospitals);
            xDetail.SetShelterDatas(shelters.Concat(openShelters).ToArray());

            lay.JSInterface.SelectedItem += itemID => {
                var intent = new Intent(Application.Context, typeof(MapActivity));
                StartActivity(intent);
            };

            SetContentView(lay.Layout);
            lay.SetHtml(xDetail);
        }

        // �t�B���^�����O
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