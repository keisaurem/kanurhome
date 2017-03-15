using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using KaNurHome.models.xmls;

namespace KaNurHome.models.layouts
{
    public class WebViewWrapper
    {
        private View _Layout;
        public View Layout
        {
            get
            {
                return _Layout;
            }
        }

        private WebView _WebViewInstance;
        public WebView WebViewInstance
        {
            get
            {
                return _WebViewInstance;
            }
        }

        private MyJSInterface _jsIf;
        public MyJSInterface JSInterface
        {
            get
            {
                return _jsIf;
            }
        }

        public WebViewWrapper(Context _context)
        {
            var linl = new LinearLayout(_context);

            linl.Orientation = Orientation.Vertical;
            linl.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent,
                LinearLayout.LayoutParams.MatchParent);

            var webv = new WebView(_context);
            webv.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent,
                LinearLayout.LayoutParams.MatchParent);

            webv.Settings.JavaScriptEnabled = true;

            this._jsIf = new MyJSInterface(_context);
            webv.AddJavascriptInterface(_jsIf, "Android");

            linl.AddView(webv);

            this._Layout = linl;
            this._WebViewInstance = webv;
        }

        public void ChangeNewWebView(Context _context)
        {
            var webv = new WebView(_context);
            webv.LayoutParameters = new LinearLayout.LayoutParams(
                LinearLayout.LayoutParams.MatchParent,
                LinearLayout.LayoutParams.MatchParent);
            webv.Settings.JavaScriptEnabled = true;

            var linl = ((LinearLayout)this._Layout);
            linl.RemoveView(this._WebViewInstance);
            linl.AddView(webv);
        }

        public void SetHtml(XHtmlModels model)
        {
            this.WebViewInstance.LoadUrl("about:blank");
            this.WebViewInstance.LoadDataWithBaseURL(null, model.ToString(), "text/html", "utf-8", null);
        }

        public void SetUrl(string filename)
        {
            WebViewInstance.LoadUrl("file:///android_asset/" + filename);
        }
    }
}