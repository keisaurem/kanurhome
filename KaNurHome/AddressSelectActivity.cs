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
using KaNurHome.sharedatas;
using KaNurHome.models.nursinghomes;
using System.Threading.Tasks;
using KaNurHome.models;
using System.Collections;
using KaNurHome.models.questions;
using KaNurHome.xmls;
using KaNurHome.models.layouts;

namespace KaNurHome
{
    [Activity(Label = "AddressSelectActivity")]
    public class AddressSelectActivity : Activity
    {
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var nursinghomes = NursingHomeModels.GetModels(Application.Context, ShareDatas.SelectedNursingTypes);

            if(ShareDatas.SelectedHospitalModels.Count != 0)
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
                nursinghomes = lstNursings.Distinct().ToArray();
            }

            var sheet = new QuestionSheets(nursinghomes);

            var xQuestions = new XQuestionHtml(Application.Context);
            xQuestions.SetQuestionItems(sheet);
            xQuestions.SetTitle("Šó–]‚ÌŠÝ’n‚ð‘I‘ð‚µ‚Ä‚­‚¾‚³‚¢");

            lay.JSInterface.SelectedItem += itemID => {
                if (itemID == XQuestionHtml.DEFAULT_ITEM_ID)
                {
                    ShareDatas.Addresses.RemoveAll(m => true);
                    var intent = new Intent(Application.Context, typeof(ListActivity));
                    StartActivity(intent);
                }
                var selected = sheet.Models
                    .Single(m => m.ID == itemID).Text;
                if (ShareDatas.Addresses.Contains(selected))
                {
                    ShareDatas.Addresses.Remove(selected);
                }
                else
                {
                    ShareDatas.Addresses.Add(selected);
                }
            };

            lay.JSInterface.RaiseSubmit += () =>
            {
                var intent = new Intent(Application.Context, typeof(ListActivity));
                StartActivity(intent);
            };

            SetContentView(lay.Layout);
            lay.SetHtml(xQuestions);
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back) ShareDatas.Addresses.RemoveAll(m => true);
            return base.OnKeyDown(keyCode, e);
        }
    }
}