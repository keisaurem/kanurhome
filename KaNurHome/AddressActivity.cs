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
using KaNurHome.models.nursinghomes;

namespace KaNurHome
{
    [Activity(Label = "AddressActivity")]
    public class AddressActivity : Activity
    {
        public static List<string> SelectedAddress { get; set; } = new List<string>();
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // 結果
            var resultTypes = typeof(NursingTypes).GetFields().Where(m => m.IsStatic)
                .Select(m => new
                {
                    val = (NursingTypes)m.GetValue(null),
                    attr = (NursingTypeAttribute)Attribute.GetCustomAttribute(m, typeof(NursingTypeAttribute))
                })
                .Where(m => m.attr.Categories.Where(c => QuestionActivity.SelectedItems.Contains(c)).Count() == QuestionActivity.SelectedItems.Count())
                .Select(m => m.val).ToArray();

            var xAddress = new XAddressHtml(Application.Context);

            var nursings = NursingHomeModels.GetModels(Application.Context,
                resultTypes);
            

            var sheet = new QuestionSheets(nursings);

            xAddress.SetQuestionItems(sheet);

            lay.JSInterface.SelectedItem += itemID => {
                if(itemID == "submit")
                {
                    var intent = new Intent(Application.Context, typeof(ListActivity));
                    StartActivity(intent);
                    return;
                }
                var itemText = sheet.Models.Single(m => m.ID == itemID).Text;
                if (SelectedAddress.Contains(itemText))
                {
                    SelectedAddress.Remove(itemText);
                }else
                {
                    SelectedAddress.Add(itemText);
                }

            };
            SetContentView(lay.Layout);
            lay.SetHtml(xAddress);

        }

        // バックされたときは最新アイテムを削除する
        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            SelectedAddress.RemoveAll(m => true);
            QuestionActivity.SelectedItems.Remove(QuestionActivity.SelectedItems.Last());
            return base.OnKeyDown(keyCode, e);
        }

        private void sendActivity()
        {
            var intent = new Intent(Application.Context, typeof(ListActivity));
            StartActivity(intent);
        }
    }
}