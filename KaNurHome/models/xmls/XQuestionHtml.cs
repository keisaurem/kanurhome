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

namespace KaNurHome.models.xmls
{
    // question.html controller
    public class XQuestionHtml : XHtmlModels
    {
        public XQuestionHtml(Context context) : base(context) { }

        public void SetQuestionItems(QuestionSheets sheet)
        {
            var target = base.HtmlSource.Root.Descendants()
                .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "wrap_answers");
            target.Add(sheet.Models.Select(m => m.ToHTML()).ToArray());
        }

        public void VisibleExecSearch()
        {
            var target = base.HtmlSource.Root.Descendants()
                   .Single(m => m.Attribute("id") != null && m.Attribute("id").Value == "exec_search");
            target.Attribute("class").Value = "margT_20";
        }

        protected override string GetHtmlFileName()
        {
            return "html/question.html";
        }
    }
}