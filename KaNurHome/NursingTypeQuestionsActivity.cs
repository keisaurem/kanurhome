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

namespace KaNurHome
{
    [Activity(Label = "NursingHomeActivity")]
    public class NursingHomeActivity : Activity
    {
        //���C�A�E�g
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            

        }
    }
}