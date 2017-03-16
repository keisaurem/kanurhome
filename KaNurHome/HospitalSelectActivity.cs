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
using KaNurHome.models.hospitals;
using KaNurHome.models.questions;
using KaNurHome.xmls;
using KaNurHome.sharedatas;
using KaNurHome.models.nursinghomes;
using KaNurHome.models;
using System.Threading.Tasks;
using System.Collections;

namespace KaNurHome
{
    [Activity(Label = "HospitalSelectActivity")]
    public class HospitalSelectActivity : Activity
    {
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var nursinghomes = NursingHomeModels.GetModels(Application.Context, ShareDatas.SelectedNursingTypes);
            var hospitals = HospitalModels.GetBigHospitalModels(Application.Context);

            var lstHospitals = new List<HospitalModels>();
            Parallel.ForEach(nursinghomes, item =>
            {
                double nlat = 0, nlng = 0;
                if (!(double.TryParse(item.Lat, out nlat) && double.TryParse(item.Lng, out nlng)))
                {
                    return;
                }
                var items = hospitals.Where(m => GeoMath.calcDistHubeny(m.LatVal, m.LngVal, nlat, nlng) < 1000);
                lock (((ICollection)lstHospitals).SyncRoot)
                {
                    lstHospitals.AddRange(items);
                }
            });

            hospitals = lstHospitals.Where(m => m != null).Distinct().ToArray();

            var sheet = new QuestionSheets(hospitals);

            var xQuestions = new XQuestionHtml(Application.Context);
            xQuestions.SetQuestionItems(sheet);
            xQuestions.SetTitle("Šó–]‚Ì‹ß—×•a‰@‚ð‘I‘ð‚µ‚Ä‚­‚¾‚³‚¢");


            lay.JSInterface.SelectedItem += itemID => {
                if (itemID == XQuestionHtml.DEFAULT_ITEM_ID)
                {
                    ShareDatas.SelectedHospitalModels.RemoveAll(m => true);
                    var intent = new Intent(Application.Context, typeof(AddressSelectActivity));
                    StartActivity(intent);
                }
                var selected = ((HospitalQuestionModels)sheet.Models
                    .Single(m => m.ID == itemID)).TargetModel;
                if (ShareDatas.SelectedHospitalModels.Contains(selected))
                {
                    ShareDatas.SelectedHospitalModels.Remove(selected);
                }
                else
                {
                    ShareDatas.SelectedHospitalModels.Add(selected);
                }
            };

            lay.JSInterface.RaiseSubmit += () =>
            {
                var intent = new Intent(Application.Context, typeof(AddressSelectActivity));
                StartActivity(intent);
            };

            SetContentView(lay.Layout);
            lay.SetHtml(xQuestions);
        }
        
        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back) ShareDatas.SelectedHospitalModels.RemoveAll(m => true);
            return base.OnKeyDown(keyCode, e);
        }
    }
}