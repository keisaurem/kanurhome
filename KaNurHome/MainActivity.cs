using Android.App;
using Android.Widget;
using Android.OS;
using KaNurHome.models.layouts;
using Android.Content;

namespace KaNurHome
{
    [Activity(Label = "KaNurHome", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            lay.JSInterface.Submit += () =>
            {
                var intent = new Intent(Application.Context, typeof(NursingHomeActivity));
                StartActivity(intent);
            };

            SetContentView(lay.Layout);
            lay.SetUrl("html/tutorial.html");
        }
    }
}

