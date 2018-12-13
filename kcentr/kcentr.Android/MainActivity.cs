using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using Java.Lang;

namespace kcentr.Droid
{
    [Activity(Label = "kcentr", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        WebView webView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.Main);
            webView = FindViewById<WebView>(Resource.Id.webView);
            //webView.SetBackgroundColor(Android.Graphics.Color.Black);
            webView.Settings.JavaScriptEnabled = true;
            webView.SetWebViewClient(new WebViewClient());
            webView.LoadUrl("https://mobile.kcentr.ru");

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

         internal class WebViewClientClass : WebViewClient  
    {  
        //Give the host application a chance to take over the control when a new URL is about to be loaded in the current WebView.  
        public override bool ShouldOverrideUrlLoading(WebView view, string url)  
        {  
            view.LoadUrl(url);  
            return true;  
        }  
    }
    }
}