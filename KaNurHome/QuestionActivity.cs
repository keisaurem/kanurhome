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
using KaNurHome.models.layouts;
using KaNurHome.models.xmls;
using KaNurHome.attributes;
using KaNurHome.models.questions;

namespace KaNurHome
{
    [Activity(Label = "QuestionActivity")]
    public class QuestionActivity : Activity
    {
        // 選択済みアイテム（カテゴリ）
        public static List<NursingCategories> SelectedItems { get; set; } = new List<NursingCategories>();
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            var xQuestion = new XQuestionHtml(Application.Context);
            
            var resultTypes = typeof(NursingTypes).GetFields().Where(m => m.IsStatic)
                .Select(m => new {
                        val = (NursingTypes)m.GetValue(null),
                        attr = (NursingTypeAttribute)Attribute.GetCustomAttribute(m, typeof(NursingTypeAttribute))})
                .Where(m => m.attr.Categories.Where(c => SelectedItems.Contains(c)).Count() == SelectedItems.Count() && SelectedItems.Count() != 0)
                .SelectMany(m => m.attr.Categories)
                .Where(m => !SelectedItems.Contains(m))
                .Distinct();

            if (resultTypes.Count() != 0) xQuestion.VisibleExecSearch();

            var sheet = new QuestionSheets(resultTypes.ToArray());

            lay.JSInterface.SelectedItem += itemID => JS_Handler(itemID, sheet);

            xQuestion.SetQuestionItems(sheet);
            SetContentView(lay.Layout);
            lay.SetHtml(xQuestion);

        }

        // バックされたときは最新アイテムを削除する
        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (SelectedItems.Count() != 0 ) SelectedItems.Remove(SelectedItems.Last());
            return base.OnKeyDown(keyCode, e);
        }

        // Javascript handler
        public void JS_Handler(string itemID, QuestionSheets sheet)
        {
            if(itemID == "255")
            {
                // 強制サーチ
                sendActivity();
                return;
            }

            var selectedItem = sheet.Models.Single(m => m.ID == itemID);
            SelectedItems.Add(selectedItem.TargetType);

            var resultNursingTypes = typeof(NursingTypes).GetFields().Where(m => m.IsStatic)
                .Where(m =>
                {
                    var attr = (NursingTypeAttribute)Attribute.GetCustomAttribute(m, typeof(NursingTypeAttribute));
                    return attr.Categories.Where(c => SelectedItems.Contains(c)).Count() == SelectedItems.Count();
                }).ToArray();

            if (resultNursingTypes.Length == 1)
            {
                // 残り一個だからリスト移行
                sendActivity();
                return;
            }
            else
            {
                var intent = new Intent(Application.Context, typeof(QuestionActivity));
                StartActivity(intent);
            }
        }

        private void sendActivity()
        {
            //var intent = new Intent(Application.Context, typeof(ListActivity));
            var intent = new Intent(Application.Context, typeof(AddressActivity));
            StartActivity(intent);
        }
    }
}