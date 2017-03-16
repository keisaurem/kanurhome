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

namespace KaNurHome.attributes
{
    // éøñ‚ï∂ÇÃëÆê´
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class QuestionAttribute : Attribute
    {
        private string _QuestionString = null;
        public string QuestionString
        {
            get
            {
                return _QuestionString;
            }
        }

        public QuestionAttribute(string question)
        {
            this._QuestionString = question;
        }
    }
}