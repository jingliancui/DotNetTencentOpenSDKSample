using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using TencentOpenAPI.QQApiInterface;
using TencentOpenAPI.QQApiInterfaceObject;
using TencentOpenAPI.TencentOAuth;
using UIKit;
using Xamarin.Forms;

namespace SampleApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {       
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {                        
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            var appid = "1110584378";

            MessagingCenter.Subscribe<object>(this, MainPage.QQShare, o =>
            {
                var tencentOAuth = new TencentOAuth(appid, new OAuthTencentSessionDelegate());                
                var textObj = new QQApiTextObject("这是来自Xamarin的QQ分享");
                var req = SendMessageToQQReq.ReqWithContent(textObj);
                QQApiInterface.SendReq(req);
            });
            MessagingCenter.Subscribe<object>(this, MainPage.QQLogin, o =>
            {                
                var k1 = TencentOpenAPI.sdkdef.Constants.kOPEN_PERMISSION_GET_USER_INFO;
                var k2 = TencentOpenAPI.sdkdef.Constants.kOPEN_PERMISSION_GET_SIMPLE_USER_INFO;                
                var para = new NSObject[] { k1, k2 };
                var tencentOAuth = new TencentOAuth(appid, new OAuthTencentSessionDelegate());
                var result = tencentOAuth.Authorize(para);
            });

            return base.FinishedLaunching(app, options);
        }

        public override bool HandleOpenURL(UIApplication application, NSUrl url)
        {
            return TencentOAuth.HandleOpenURL(url);
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return TencentOAuth.HandleOpenURL(url);
        }
    }    

    public class OAuthTencentSessionDelegate : TencentSessionDelegate 
    {       
        public override void TencentDidLogin()
        {
        }

        public override void TencentDidNotLogin(bool cancelled)
        {
            
        }

        public override void TencentDidNotNetWork()
        {
            
        }
        public override NSObject[] GetAuthorizedPermissions(NSObject[] permissions, NSDictionary extraParams)
        {
            return base.GetAuthorizedPermissions(permissions, extraParams);
        }
    }
}
