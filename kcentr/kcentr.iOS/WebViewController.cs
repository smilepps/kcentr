using System;
using System.Collections.Generic;
using CoreGraphics;
using System.Linq;
using Foundation;
using UIKit;
using AVFoundation;
using ZXing.Mobile;
using System.Threading.Tasks;
using ZXing;

namespace kcentr.iOS
{

    public class WebViewController : UIViewController
    {

        UIWebView webView;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            webView = new UIWebView(View.Bounds);
            View.AddSubview(webView);
            webView.LoadFinished += WebView_LoadFinished;
            webView.ShouldStartLoad = (webView, request, navType) =>
            {
                System.Console.WriteLine("###" + request.Url.AbsoluteString);
                if (request.Url.AbsoluteString == "myapp://scan") {
                    var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
                    options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
                        ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13
                    };

                    var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                    var result = scanner.Scan(options);
                    HandleScanResult(result);
                }
                return true;
            };
            var url = "https://mobile.kcentr.ru";
            webView.LoadRequest(new NSUrlRequest(new NSUrl(url)));

            // if this is false, page will be 'zoomed in' to normal size
            webView.ScalesPageToFit = false;

        }

        private void WebView_LoadFinished(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandleScanResult(Task<Result> result)
        {
            result = result;
        }

        private void HandleScanResult(ZXing.Result result)
        {
            webView.EvaluateJavascript("location.href = '/search?q=" + result.Text + "';");
        }


    }

}