using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Hologram2.Droid; // Replace with your actual namespace
using Android.Webkit;
using System;
using Android.App;

[assembly: ExportRenderer(typeof(Xamarin.Forms.WebView), typeof(CustomWebViewRenderer))]
namespace Hologram2.Droid
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        Activity mActivity;
        public CustomWebViewRenderer(Context context) : base(context)
        {
            mActivity = context as Activity;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Settings.JavaScriptEnabled = true;

                //   Control.SetWebChromeClient(new CustomWebChromeClient(mActivity));
            }
        }



    }
}
