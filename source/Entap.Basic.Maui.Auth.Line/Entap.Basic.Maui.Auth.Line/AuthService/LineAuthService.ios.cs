using System;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using LineSDKObjC;
using UIKit;

namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LineAuthService : ILineAuthService
    {
        /// <summary>
        /// 初期化
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#call-loginmanagersetup-method
        /// </summary>
        public static void Init(string channelId, string? universalLinkUrl = null)
        {
            var url = string.IsNullOrEmpty(universalLinkUrl) ? null : new NSUrl(universalLinkUrl);
            LineSDKLoginManager.SharedManager.SetupWithChannelID(channelId, url);
        }

        /// <summary>
        /// ContinueUserActivity
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/universal-links-support/#modify-app-delegate
        /// </summary>
        public static bool ContinueUserActivity(UIApplication application, NSUserActivity userActivity, UIApplicationRestorationHandler completionHandler)
#pragma warning disable CS8604
            // userActivity.WebPageUrlの警告抑制
            // LineSDKLoginManagerはNullableではないが、LineSDKLoginManagerから実行するLoginManagerはNullableのため
            // https://github.com/line/line-sdk-ios-swift/blob/c68678840b2dcf13e14054d35386c890f00ca103/LineSDK/LineSDKObjC/Login/LineSDKLoginManager.swift#L87
            // https://github.com/line/line-sdk-ios-swift/blob/955322119dbd9d3561804f00e3583118004d3054/LineSDK/LineSDK/Login/LoginManager.swift#L279
            => LineSDKLoginManager.SharedManager.Application(application, userActivity.WebPageUrl, new NSDictionary<NSString, NSObject>());
#pragma warning restore CS8604

        /// <summary>
        /// OpenUrl
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-app-delegate
        /// </summary>
        public static bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
            => LineSDKLoginManager.SharedManager.Application(app, url, new NSDictionary<NSString, NSObject>());

        /// <summary>
        /// OpenUrlContexts
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#modify-scene-delegates
        /// </summary>
#pragma warning disable CS8604
        // userActivity.WebPageUrlの警告抑制
        // LineSDKLoginManagerはNullableではないが、LineSDKLoginManagerから実行するLoginManagerはNullableのため
        // https://github.com/line/line-sdk-ios-swift/blob/c68678840b2dcf13e14054d35386c890f00ca103/LineSDK/LineSDKObjC/Login/LineSDKLoginManager.swift#L87
        // https://github.com/line/line-sdk-ios-swift/blob/955322119dbd9d3561804f00e3583118004d3054/LineSDK/LineSDK/Login/LoginManager.swift#L279
        public static void OpenUrlContexts(UIScene scene, NSSet<UIOpenUrlContext> urlContexts)
            => LineSDKLoginManager.SharedManager.Application(UIApplication.SharedApplication, url: urlContexts?.ToArray()?.FirstOrDefault()?.Url, new NSDictionary<NSString, NSObject>());
#pragma warning restore CS8604

        /// <summary>
        /// ログイン処理
        /// https://developers.line.biz/ja/docs/ios-sdk/swift/integrate-line-login/#use-code
        /// </summary>
        public Task<LoginResult> PlatformLoginAsync(params LoginScope[] scopes)
        {
            var loginPermissions = GetLoginPermissions(scopes);
            var permissions = new NSSet<LineSDKLoginPermission>(loginPermissions);
            var viewController = Platform.GetCurrentUIViewController();

            TaskCompletionSource<LoginResult> tcs = new TaskCompletionSource<LoginResult>();
            LineSDKLoginManager.SharedManager.LoginWithPermissions(permissions, viewController, (LineSDKLoginResult arg1, NSError arg2) =>
            {
                tcs.TrySetResult(new LoginResult(arg1, arg2));
            });
            return tcs.Task;
        }

        internal static LineSDKLoginPermission[] GetLoginPermissions(LoginScope[] scopes)
        {
            return scopes
                .Select((LoginScope scope) => GetLoginPermission(scope))
                .ToArray();
        }

        static LineSDKLoginPermission GetLoginPermission(LoginScope scope)
        {
            return scope switch
            {
                LoginScope.OpenID => LineSDKLoginPermission.OpenID,
                LoginScope.Profile => LineSDKLoginPermission.Profile,
                LoginScope.Email => LineSDKLoginPermission.Email,
                _ => throw new ArgumentOutOfRangeException(nameof(LoginScope)),
            };
        }
    }
}
