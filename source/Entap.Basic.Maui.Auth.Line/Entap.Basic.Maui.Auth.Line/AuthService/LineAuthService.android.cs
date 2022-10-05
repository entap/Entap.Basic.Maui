using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Com.Linecorp.Linesdk;
using Com.Linecorp.Linesdk.Auth;
using Entap.Basic.Maui.Core;

namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LineAuthService : ILineAuthService
    {
        public static string? ChannelId => _channelId;
        static string? _channelId;

        static readonly int _requestCode = 1;

        /// <summary>
        /// 初期化
        /// </summary>
        public static void Init(string channelId)
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
            if (ChannelId is null)
            {
                throw new InvalidOperationException($"Please call {nameof(LineAuthService.Init)} method.");
            }

            var context = Platform.AppContext;
            var param = new LineAuthenticationParams
                .Builder()
                .Scopes(GetScopes(scopes))?
                .Build();
            if (param is null)
            {
                // LineAuthenticationParams.Builder.ScopesはNull非許容
                // https://developers.line.biz/en/reference/android-sdk/reference/com/linecorp/linesdk/auth/LineAuthenticationParams.Builder.html
                throw new NullReferenceException(nameof(LineAuthenticationParams.Builder.Scopes));
            }
            var loginIntent = LineLoginApi.GetLoginIntent(context, ChannelId, param);

            var activity = Platform.CurrentActivity;
            if (activity is null)
            {
                throw new InvalidOperationException($"Platform.CurrentActivity is null");
            }
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
            // com.linecorp.linesdk.ScopeはNull非許容のため警告抑制
            // https://developers.line.biz/en/reference/android-sdk/reference/com/linecorp/linesdk/Scope.html
#pragma warning disable CS8603
            return loginScope switch
            {
                LoginScope.OpenID => Scope.OpenidConnect,
                LoginScope.Profile => Scope.Profile,
                LoginScope.Email => Scope.OcEmail,
                _ => throw new ArgumentOutOfRangeException(nameof(LoginScope)),
            };
        }
#pragma warning restore CS8603
    }
}
