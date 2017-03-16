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
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CsvColumnAttribute : Attribute
    {
        private string _Column;
        public string Column
        {
            get
            {
                return _Column;
            }
        }

        public CsvColumnAttribute(string column)
        {
            this._Column = column;
        }
    }
}