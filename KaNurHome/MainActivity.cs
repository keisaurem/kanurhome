using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using KaNurHome.models.layouts;

namespace KaNurHome
{

    [Activity(Label = "KaNurHome", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    public class MainActivity : Activity
    {
        //レイアウト
        private WebViewWrapper lay = new WebViewWrapper(Application.Context);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            lay.JSInterface.SelectedItem += itemID =>
            {
                var intent = new Intent(Application.Context, typeof(QuestionActivity));
                StartActivity(intent);
            };

            SetContentView(lay.Layout);
            lay.SetUrl("html/tutorial.html");
        }
    }
}

