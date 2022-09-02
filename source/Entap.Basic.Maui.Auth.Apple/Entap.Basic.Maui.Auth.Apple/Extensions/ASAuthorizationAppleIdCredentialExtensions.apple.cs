using System;
using AuthenticationServices;
using Foundation;

namespace Entap.Basic.Maui.Auth.Apple
{
    public static class ASAuthorizationAppleIdCredentialExtensions
    {
        public static AppleIdCredential ToAppleIdCredential(this ASAuthorizationAppleIdCredential credential)
        {
            return new AppleIdCredential(credential.User, credential.RealUserStatus.ToUserDetectionStatus())
            {
                AuthorizationCode = (credential.AuthorizationCode is null) ? null: new NSString(credential.AuthorizationCode, NSStringEncoding.UTF8).ToString(),
                IdToken = (credential.IdentityToken is null) ? null : new NSString(credential.IdentityToken, NSStringEncoding.UTF8).ToString(),
                Email = credential.Email,
                PersonName = credential.FullName?.ToPersonName(),
            };
        }
    }
}
