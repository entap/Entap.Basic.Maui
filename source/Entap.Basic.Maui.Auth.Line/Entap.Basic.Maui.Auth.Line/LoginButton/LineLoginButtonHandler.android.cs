using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Com.Linecorp.Linesdk;
using Com.Linecorp.Linesdk.Auth;
using Com.Linecorp.Linesdk.Internal;
using Com.Linecorp.Linesdk.Widget;
using Java.Interop;
using Microsoft.Maui.Handlers;
using static Android.Views.View;

namespace Entap.Basic.Maui.Auth.Line
{
    /// <summary>
    /// LineLoginButtonHandler(Android)
    /// https://developers.line.biz/ja/docs/android-sdk/integrate-line-login/#use-button
    /// </summary>
	public partial class LineLoginButtonHandler : ViewHandler<LineLoginButton, LoginButton>
    {
        static LoginDelegateImpl? _loginDelegate;
        public LineLoginButtonHandler() : base(ViewHandler.ViewMapper)
        {
            
        }

        protected override LoginButton CreatePlatformView()
        {
            return new LoginButton(Platform.CurrentActivity);
        }

        protected override void ConnectHandler(LoginButton platformView)
        {
            base.ConnectHandler(platformView);

            platformView.SetChannelId(LineAuthService.ChannelId);
            platformView.EnableLineAppAuthentication(true);

            var param = new LineAuthenticationParams
                .Builder()
                .Scopes(LineAuthService.GetScopes(VirtualView.Scopes))?
                .Build();
            if (param is null)
            {
                // LineAuthenticationParams.Builder.ScopesはNull非許容
                // https://developers.line.biz/en/reference/android-sdk/reference/com/linecorp/linesdk/auth/LineAuthenticationParams.Builder.html
                throw new NullReferenceException(nameof(LineAuthenticationParams.Builder.Scopes));
            }
            platformView.SetAuthenticationParams(param);

            _loginDelegate = new LoginDelegateImpl();
            platformView.SetLoginDelegate(_loginDelegate);
            platformView.AddLoginListener(new LineLoginListener(this));
        }

        protected override void DisconnectHandler(LoginButton platformView)
        {
            _loginDelegate?.Dispose();
            base.DisconnectHandler(platformView);
        }

        public static bool OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (_loginDelegate is null) return false;
            return _loginDelegate.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }

    public class LineLoginListener : Java.Lang.Object, ILoginListener
    {
        readonly LineLoginButtonHandler _handler;
        public LineLoginListener(LineLoginButtonHandler handler)
        {
            _handler = handler;
        }

        public void OnLoginFailure(LineLoginResult? result)
        {
            _handler.VirtualView.SendClicked(new LoginResult(result));
        }

        public void OnLoginSuccess(LineLoginResult result)
        {
            _handler.VirtualView.SendClicked(new LoginResult(result));
        }
    }
}

