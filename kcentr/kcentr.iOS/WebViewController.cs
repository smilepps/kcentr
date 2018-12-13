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
            webView.ShouldStartLoad = (webView, request, navType) =>
            {
//                System.Console.WriteLine("###" + request.Url.AbsoluteString);
                if (request.Url.AbsoluteString == "myapp://scan") {
                    var options = new ZXing.Mobile.MobileBarcodeScanningOptions
                    {
                        PossibleFormats = new List<ZXing.BarcodeFormat>() {
                        ZXing.BarcodeFormat.EAN_8, ZXing.BarcodeFormat.EAN_13, ZXing.BarcodeFormat.CODE_128
                    }
                    };

                    var scanner = new ZXing.Mobile.MobileBarcodeScanner
                    {
                        //                    scanner.ScanContinuously(HandleScanResult);
                        TopText = "Поднесите камеру к штрих-коду для сканирования",
                        BottomText = "Штрих-код будет автоматически отсканирован"
                    };
                    scanner.Scan(options).ContinueWith(t => {
                        var result = t.Result;

                        var format = result?.BarcodeFormat.ToString() ?? string.Empty;
                        var value = result?.Text ?? string.Empty;

                        BeginInvokeOnMainThread(() => {
                            if (value != null && !string.IsNullOrEmpty(value))
                                webView.EvaluateJavascript("location.href = '/search?q=" + value + "';");
//                            var av = UIAlertController.Create("Barcode Result", format + "|" + value, UIAlertControllerStyle.Alert);
//                            av.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));
//                            PresentViewController(av, true, null);
                        });
                    });
                }
                return true;
            };
            var url = "https://mobile.kcentr.ru";
            webView.LoadRequest(new NSUrlRequest(new NSUrl(url)));

            // if this is false, page will be 'zoomed in' to normal size
            webView.ScalesPageToFit = false;

        }



    }

}