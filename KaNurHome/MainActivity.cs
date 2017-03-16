using Android.App;
using Android.Widget;
using Android.OS;
using KaNurHome.models.layouts;
using Android.Content;

namespace KaNurHome
{
    [Activity(Label = "かなほーむ", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    public class MainActivity : Activity
    {
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            lay.JSInterface.SelectedItem += itemID =>
            {
                var intent = new Intent(Application.Context, typeof(NursingHomeSelectActivity));
                StartActivity(intent);
            };

            SetContentView(lay.Layout);
            lay.SetUrl("html/tutorial.html");
        }
    }
}

