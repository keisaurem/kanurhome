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

namespace KaNurHome.attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class NursingTypeAttribute : Attribute
    {
        private NursingCategories[] _categories;
        public NursingCategories[] Categories
        {
            get
            {
                return _categories;
            }
        }

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

        public NursingTypeAttribute(string text, string csvName, string icon, params NursingCategories[] categories)
        {
            this._Text = text;
            this._CsvName = csvName;
            this._categories = categories;
            this._Icon = icon;
        }
    }
}