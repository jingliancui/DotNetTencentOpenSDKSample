using CommunityToolkit.Mvvm.Messaging;
using CoreFoundation;
using Foundation;
using SampleApp.Messages;
using SampleApp.TencentStuff;
using TencentOpenAPI_Api;
using UIKit;

namespace SampleApp;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    private TencentOAuth mTencent;

    protected override MauiApp CreateMauiApp() 
	{
        WeakReferenceMessenger.Default.Register<ShareMessage>(this, (r, m) =>
        {
            var textObj = new QQApiTextObject("https://www.zhihu.com/column/c_1231174330576011264");
            var req = SendMessageToQQReq.ReqWithContent(textObj);
            QQApiInterface.SendReq(req);            
        });

        TencentOAuth.IsUserAgreedAuthorization = true;
        mTencent = new TencentOAuth("你的AppID", new OAuthTencentSessionDelegate());

        return MauiProgram.CreateMauiApp();
	}

    public override bool OpenUrl(UIApplication application, NSUrl url, NSDictionary options)
    {
		return TencentOAuth.HandleOpenURL(url);
    }
}
