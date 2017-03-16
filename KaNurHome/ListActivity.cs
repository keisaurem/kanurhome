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
using KaNurHome.models.xmls;
using KaNurHome.models.nursinghomes;
using KaNurHome.enums;
using KaNurHome.attributes;
using KaNurHome.models.nearplaces;

namespace KaNurHome
{
    [Activity(Label = "ListActivity")]
    public class ListActivity : Activity
    {
        public static NursingHomeModels SelectedItem { get; set; } = null;
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var xList = new XListHtml(Application.Context);

            // 結果
            var resultTypes = typeof(NursingTypes).GetFields().Where(m => m.IsStatic)
                .Select(m => new
                {
                    val = (NursingTypes)m.GetValue(null),
                    attr = (NursingTypeAttribute)Attribute.GetCustomAttribute(m, typeof(NursingTypeAttribute))
                })
                .Where(m => m.attr.Categories.Where(c => QuestionActivity.SelectedItems.Contains(c)).Count() == QuestionActivity.SelectedItems.Count())
                .Select(m => m.val).ToArray();

            // キーワード
            var keywords = QuestionActivity.SelectedItems
                .Select(m =>
                {
                    var attr = (QuestionAttribute)Attribute.GetCustomAttribute(
                        typeof(NursingCategories).GetField(m.ToString()),
                        typeof(QuestionAttribute));
                    return attr.QuestionString;
                }).ToArray();


            // 防災施設取得
            var shelters = ShelterModels.GetModels(Application.Context);
            var openShelters = OpenDataShelterModels.GetModels(Application.Context);

            var nursinghomes = NursingHomeModels.GetModels(Application.Context, resultTypes)
                .Where(m => AddressActivity.SelectedAddress.Where(n => m.Address.Contains(n)).Count() > 0)
                //.Where(m => m.Address.Contains(AddressActivity.SelectedAddress))
                .AsParallel().Select(m =>
                {
                    // 半径1KM内の施設を数えてレーティング付け
                    var shelcount = shelters.Where(n => FilteringShelters(n ,m)).Count();
                    var odshelcount = openShelters.Where(n => FilteringShelters(n, m)).Count();

                    var ratebase = shelcount + odshelcount;

                    var rate = (short)(ratebase / 5);
                    if (ratebase == 0) rate = 0;

                    m.Rating = rate;

                    return m;
                }).OrderByDescending(m => m.Rating).ToArray();

            xList.SetKeywords(keywords, AddressActivity.SelectedAddress);
            xList.SetListItems(nursinghomes);

            lay.JSInterface.SelectedItem += itemID => JS_Handler(itemID, nursinghomes);

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
            return (int)models.GeoMath.calcDistHubeny(near.LatVal, near.LngVal, nlat, nlng) < 1000;
        }

        public void JS_Handler(string itemID, NursingHomeModels[] models)
        {
            SelectedItem = models.Single(m => m.ID == itemID);
            var intent = new Intent(Application.Context, typeof(DetailActivity));
            StartActivity(intent);
        }
    }
}