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
using KaNurHome.models.nursinghomes;
using KaNurHome.models.questions;
using KaNurHome.models.layouts;
using KaNurHome.xmls;
using KaNurHome.sharedatas;

namespace KaNurHome
{
    [Activity(Label = "NursingHomeSelectActivity")]
    public class NursingHomeSelectActivity : Activity
    {
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            var sheet = new QuestionSheets(
                typeof(NursingTypes).GetFields().Where(m => m.IsStatic)
                    .Select(m => (NursingTypes)m.GetValue(null)).ToArray());

            var xQuestions = new XQuestionHtml(Application.Context);
            xQuestions.SetQuestionItems(sheet);
            xQuestions.SetTitle("対象の施設種類を選択してください");
            xQuestions.HiddenDefaultItem();

            lay.JSInterface.SelectedItem += itemID => {
                var selected = ((NursingTypeQuestionModels)sheet.Models
                    .Single(m => m.ID == itemID)).TargetType;
                if (ShareDatas.SelectedNursingTypes.Contains(selected))
                {
                    ShareDatas.SelectedNursingTypes.Remove(selected);
                }
                else
                {
                    ShareDatas.SelectedNursingTypes.Add(selected);
                }
            };

            lay.JSInterface.RaiseSubmit += () =>
            {
                var intent = new Intent(Application.Context, typeof(HospitalSelectActivity));
                StartActivity(intent);
            };

            SetContentView(lay.Layout);
            lay.SetHtml(xQuestions);
        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back) ShareDatas.SelectedNursingTypes.RemoveAll(m => true);
            return base.OnKeyDown(keyCode, e);
        }
    }
}