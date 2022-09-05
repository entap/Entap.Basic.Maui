using System;
using AuthenticationServices;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;

namespace Entap.Basic.Maui.Auth.Apple
{
	public class AppleSignInButtonHandler : ViewHandler<IAppleSignInButton, ASAuthorizationAppleIdButton>
	{
        public static IPropertyMapper<IAppleSignInButton, AppleSignInButtonHandler> Mapper =
            new PropertyMapper<IAppleSignInButton, AppleSignInButtonHandler>(ViewMapper)
            {
                [nameof(AppleSignInButton.CornerRadius)] = MapCornerRadius,
            };

        public AppleSignInButtonHandler() : base(Mapper, null)
        {
        }

        protected override ASAuthorizationAppleIdButton CreatePlatformView()
        {
            return new ASAuthorizationAppleIdButton(
                ToASAuthorizationAppleIdButtonType(VirtualView.ButtonType),
                ToASAuthorizationAppleIdButtonStyle(VirtualView.ButtonStyle)
            );
        }

        protected override void ConnectHandler(ASAuthorizationAppleIdButton platformView)
        {
            base.ConnectHandler(platformView);
            platformView.TouchUpInside += OnTouchUpInside;
        }

        protected override void DisconnectHandler(ASAuthorizationAppleIdButton platformView)
        {
            platformView.TouchUpInside -= OnTouchUpInside;
            base.DisconnectHandler(platformView);
        }

        private void OnTouchUpInside(object? sender, EventArgs e)
        {
            VirtualView.SendClicked();
        }

        private static void MapCornerRadius(AppleSignInButtonHandler handler, IAppleSignInButton view)
        {
            if (handler.PlatformView is not ASAuthorizationAppleIdButton appleIdButton) return;
            if (view.CornerRadius < 0) return;
            appleIdButton.CornerRadius = (float)view.CornerRadius;
        }

        private ASAuthorizationAppleIdButtonType ToASAuthorizationAppleIdButtonType(ButtonType buttonType) =>
            buttonType switch
            {
                ButtonType.SignIn => ASAuthorizationAppleIdButtonType.SignIn,
                ButtonType.SignUp => ASAuthorizationAppleIdButtonType.SignUp,
                ButtonType.Continue => ASAuthorizationAppleIdButtonType.Continue,
                _ => throw new ArgumentOutOfRangeException(nameof(ButtonType))
            };

        private ASAuthorizationAppleIdButtonStyle ToASAuthorizationAppleIdButtonStyle(ButtonStyle buttonStyle) =>
            buttonStyle switch
            {
                ButtonStyle.Black => ASAuthorizationAppleIdButtonStyle.Black,
                ButtonStyle.White => ASAuthorizationAppleIdButtonStyle.White,
                ButtonStyle.WhiteOutline => ASAuthorizationAppleIdButtonStyle.WhiteOutline,
                _ => throw new ArgumentOutOfRangeException(nameof(ButtonStyle))
            };
    }
}