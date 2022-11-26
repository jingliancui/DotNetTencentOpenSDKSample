using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Com.Tencent.Connect.Share;
using Com.Tencent.Tauth;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using SampleApp.Messages;
using SampleApp.TencentStuff;

namespace SampleApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    private Tencent mTencent;

    private CustomizeUiListener mCustomizeUiListener;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        mCustomizeUiListener = new CustomizeUiListener();

        WeakReferenceMessenger.Default.Register<ShareMessage>(this, (r, m) =>
        {
            var para = new Bundle();
            para.PutInt(QQShare.ShareToQqKeyType, QQShare.ShareToQqTypeDefault);
            para.PutString(QQShare.ShareToQqTitle, "这是来自MAUI的QQ分享");
            para.PutString(QQShare.ShareToQqTargetUrl, "https://www.zhihu.com/column/c_1231174330576011264");
            mTencent.ShareToQQ(this, para, mCustomizeUiListener);
        });

        Tencent.SetIsPermissionGranted(true);
        mTencent = Tencent.CreateInstance("你的AppID", ApplicationContext, "com.tencent.sample.fileprovider");
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {        
        if (requestCode == Com.Tencent.Connect.Common.Constants.RequestQqShare)
        {
            Tencent.OnActivityResultData(requestCode, (int)resultCode, data, mCustomizeUiListener);
        }
    }
}
