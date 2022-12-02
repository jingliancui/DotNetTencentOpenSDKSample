#if IOS
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TencentOpenAPI_Api;

namespace SampleApp.TencentStuff
{
    public class OAuthTencentSessionDelegate: TencentSessionDelegate
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
#endif