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
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class NursingTypeAttribute : Attribute
    {
        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
        }

        private string _CsvName;
        public string CsvName
        {
            get
            {
                return _CsvName;
            }
        }

        private string _Icon;
        public string Icon
        {
            get
            {
                return _Icon;
            }
        }

        public NursingTypeAttribute(string text, string csvName, string icon)
        {
            this._Text = text;
            this._CsvName = csvName;
            this._Icon = icon;
        }
    }
}