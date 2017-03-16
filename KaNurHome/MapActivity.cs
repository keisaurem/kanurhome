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
using KaNurHome.models.layouts;
using KaNurHome.models.xmls;

namespace KaNurHome
{
    [Activity(Label = "MapActivity")]
    public class MapActivity : Activity
    {
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var xMap = new XMapHtml(Application.Context);
            xMap.SetSelectedItem(ListActivity.SelectedItem);

            SetContentView(lay.Layout);
            lay.SetHtml(xMap);
        }
    }
}