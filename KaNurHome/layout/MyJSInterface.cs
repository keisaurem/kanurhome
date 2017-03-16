using Android.Content;
using Android.Widget;
using Java.Interop;
using Android.Webkit;

namespace KaNurHome.models.layouts
{
    public class MyJSInterface : Java.Lang.Object
    {
        public delegate void SelectItem(string itemId);
        public event SelectItem SelectedItem;

        private Context _context;
        public MyJSInterface(Context context)
        {
            this._context = context;
        }

        /* commons */
        [Export("ViewToast")]
        [JavascriptInterface]
        public void ViewToast(Java.Lang.String message)
        {
            Toast.MakeText(_context, message, ToastLength.Long).Show();
        }

        [Export("SelectItem")]
        [JavascriptInterface]
        public void SelectItemID(Java.Lang.String itemId)
        {
            SelectedItem(itemId.ToString());
        }
    }
}