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
        // �I���ς݃A�C�e���i�J�e�S���j
        public static List<NursingCategories> SelectedItems { get; set; } = new List<NursingCategories>();
        //���C�A�E�g
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

        // �o�b�N���ꂽ�Ƃ��͍ŐV�A�C�e�����폜����
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
                // �����T�[�`
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
                // �c�������烊�X�g�ڍs
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