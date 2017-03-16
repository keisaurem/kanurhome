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
using KaNurHome.models.questions;

namespace KaNurHome.xmls
{
    // question.html controller
    public class XQuestionHtml : XHtmlModels
    {
        public const string DEFAULT_ITEM_ID = "none_answer";

        public XQuestionHtml(Context context) : base(context) { }

        public void SetQuestionItems(QuestionSheets sheet)
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "wrap_answers");
            target.Add(sheet.Models.Select(m => m.ToHTML()).ToArray());
        }

        public void SetTitle(string text)
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "question_title");
            target.Add(text);
        }

        public void HiddenDefaultItem()
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == DEFAULT_ITEM_ID);
            target.SetAttributeValue("class", "disnon");
        }

        protected override string GetHtmlFileName()
        {
            return "html/question.html";
        }
    }
}