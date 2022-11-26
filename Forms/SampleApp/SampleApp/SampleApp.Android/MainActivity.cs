using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Tencent.Tauth;
using Xamarin.Forms;
using Com.Tencent.Connect.Share;
using Android.Content;

namespace SampleApp.Droid
{
    [Activity(Label = "SampleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Tencent tencent;
        private IUiListener loginUIListener;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            MessagingCenter.Subscribe<object>(this, MainPage.QQShare, o => 
            {
                var para = new Bundle();
                para.PutInt(QQShare.ShareToQqKeyType, QQShare.ShareToQqTypeDefault);
                para.PutString(QQShare.ShareToQqTitle, "这是来自Xamarin的QQ分享");
                para.PutString(QQShare.ShareToQqTargetUrl, "https://mp.weixin.qq.com/s?__biz=MzUyMjMyNDkyNA==&mid=2247483866&idx=1&sn=51892c4dbca89f3a370a3404184c003a&chksm=f9ccdf09cebb561f6fcc6fa22a5e0c347966cb4dc9c35468ac492d45832327f6f709deb40f18&token=532895856&lang=zh_CN#rd");
                tencent.ShareToQQ(this, para, new UiListener());
            });
            MessagingCenter.Subscribe<object>(this, MainPage.QQLogin, o=>
            {
                loginUIListener = new UiListener();
                var result = tencent.Login(this, "all", loginUIListener);
            });
            tencent = Tencent.CreateInstance("1110576152", ApplicationContext, "com.tencent.sample.fileprovider");            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {            
            
            if (requestCode == Com.Tencent.Connect.Common.Constants.RequestLogin) {               
                var result=Tencent.OnActivityResultData(requestCode, (int)resultCode, data, loginUIListener);                           
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }
    }

    public class UiListener : Java.Lang.Object, IUiListener
    {
        public void OnCancel()
        {
        }

        public void OnError(UiError p0)
        {
        }

        public void OnIUiListenerComplete(Java.Lang.Object p0)
        {
            
        }
    }
}