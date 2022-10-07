using System;
using Foundation;
using LineSDKObjC;
using Microsoft.Maui.Handlers;
using UIKit;

namespace Entap.Basic.Maui.Auth.Line
{
    /// <summary>
    /// LineLoginButtonHandler(iOS)
    /// https://developers.line.biz/ja/docs/ios-sdk/objective-c/integrate-line-login/#use-button
    /// </summary>
	public partial class LineLoginButtonHandler : ViewHandler<LineLoginButton, UIButton>
    {
        LineSDKLoginButton? _loginButton;

        public LineLoginButtonHandler() : base(ViewHandler.ViewMapper)
        {
        }

        protected override UIButton CreatePlatformView()
        {
            _loginButton = new LineSDKLoginButton();
            return _loginButton.Button;
        }

        protected override void ConnectHandler(UIButton platformView)
        {
            base.ConnectHandler(platformView);
            AddTouchUpInsideAction();
        }

        protected override void DisconnectHandler(UIButton platformView)
        {
            if (_loginButton is not null)
            {
                _loginButton.Button.TouchUpInside -= Button_TouchUpInside;
                _loginButton.Dispose();
            }
            base.DisconnectHandler(platformView);   
        }

        /// <summary>
        /// LoginDelegateが動作しないため
        /// デフォルトのTouchUpInsideを無効化し、ログイン処理を実行する
        /// </summary>
        void AddTouchUpInsideAction()
        {
            if (_loginButton is null) return;
            _loginButton.Button.RemoveTarget(null, null, UIControlEvent.TouchUpInside);
            _loginButton.Button.TouchUpInside += Button_TouchUpInside;
        }

        async void Button_TouchUpInside(object? sender, EventArgs e)
        {
            var authService = new LineAuthService();
            var result = await authService.PlatformLoginAsync(VirtualView.Scopes);
            VirtualView.SendClicked(result);
        }
    }
}

