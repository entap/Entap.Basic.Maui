using System;
using LineSDKObjC;
using Foundation;
using System.Threading.Tasks;

namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LoginResult
    {
#nullable enable
        public LoginResult(LineSDKLoginResult? result, NSError? error)
#nullable disable
        {
            if (error is not null)
                SetError(error);

            if (result is not null)
                SetResult(result);

        }

        void SetError(NSError error)
        {
            IsCanceled = error.Code == LineSDKErrorCode.UserCancelled;
            Exception = new NSErrorException(error);
        }

        void SetResult(LineSDKLoginResult result)
        {
            LineAccessToken = GetLineAccessTokenResponse(result.AccessToken);
            UserProfile = GetUserProfile(result);
        }

        LineAccessTokenResponse GetLineAccessTokenResponse(LineSDKAccessToken lineAccessToken)
        {
            var accessToken = GetAccessToken(lineAccessToken);
            return new LineAccessTokenResponse
            {
                AccessToken = accessToken.AccessTokenAccessToken,
                TokenType = accessToken.TokenType,
                ExpiresIn = accessToken.ExpiresIn,
                Scope = accessToken.Scope,
                IdToken = accessToken.IdToken,
                RefreshToken = accessToken.RefreshToken,
            };
        }

        /// <summary>
        /// AccessToken取得処理
        /// LineSDKObjC.LineSDKAccessTokenにでIDTokenRawが取得できないため
        /// LineSDKAccessToken.Jsonから取得する
        /// https://github.com/line/line-sdk-ios-swift/blob/master/LineSDK/LineSDK/Login/Model/AccessToken.swift
        /// https://github.com/line/line-sdk-ios-swift/blob/master/LineSDK/LineSDKObjC/Login/Model/LineSDKAccessToken.swift
        /// </summary>
        AccessToken GetAccessToken(LineSDKAccessToken accessToken)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(accessToken.Json);
        }

#nullable enable
        UserProfile? GetUserProfile(LineSDKLoginResult arg1)
#nullable disable
        {
            var profile = arg1.GetUserProfile();
            if (profile is null)
                return null;

            return new UserProfile(profile.UserID, profile.DisplayName)
            {
                PictureURL = profile.PictureURL,
                StatusMessage = profile.StatusMessage
            };
        }
    }
}
