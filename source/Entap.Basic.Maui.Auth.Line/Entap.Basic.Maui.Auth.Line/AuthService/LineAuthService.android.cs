using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Com.Linecorp.Linesdk;
using Com.Linecorp.Linesdk.Auth;

namespace Entap.Basic.Auth.Line
{
    public partial class LineAuthService : ILineAuthService
    {
        public static string ChannelId => _channelId;
        static string _channelId;

        static readonly int _requestCode = 1;

        /// <summary>
        /// 初期化
        /// </summary>
#nullable enable
        public static void Init(string channelId)
#nullable disable
        {
            _channelId = channelId;
        }

        public static bool OnActivityResult(int requestCode, Result resultCode, Intent data)
            => LineLoginButtonRenderer.OnActivityResult(requestCode, resultCode, data);

        /// <summary>
        /// ログイン処理
        /// https://developers.line.biz/ja/docs/android-sdk/integrate-line-login/#starting-login-activity
        /// https://developers.line.biz/ja/docs/android-sdk/integrate-line-login/#handling-login-result
        /// </summary>
        public async Task<LoginResult> PlatformLoginAsync(params LoginScope[] scopes)
        {
            var context = Xamarin.Essentials.Platform.AppContext;
            var param = new LineAuthenticationParams
                .Builder()
                .Scopes(GetScopes(scopes))
                .Build();
            var loginIntent = LineLoginApi.GetLoginIntent(context, ChannelId, param);

            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            var activityResult = await Core.Android.StarterActivity.StartAsync(activity, loginIntent, _requestCode);
            var result = LineLoginApi.GetLoginResultFromIntent(activityResult);
            return new LoginResult(result);
        }

        internal static Scope[] GetScopes(LoginScope[] scopes)
        {
            return scopes
                 .Select((LoginScope scope) => GetScope(scope))
                 .ToArray();
        }

        static Scope GetScope(LoginScope loginScope)
        {
            return loginScope switch
            {
                LoginScope.OpenID => Scope.OpenidConnect,
                LoginScope.Profile => Scope.Profile,
                LoginScope.Email => Scope.OcEmail,
                _ => throw new ArgumentOutOfRangeException(nameof(LoginScope)),
            };
        }

    }
}
