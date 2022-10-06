using System;
using LineSDKObjC;
using Foundation;
using System.Threading.Tasks;

namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LoginResult
    {
        public LoginResult(LineSDKLoginResult? result, NSError? error)
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

        LineAccessTokenResponse? GetLineAccessTokenResponse(LineSDKAccessToken lineAccessToken)
        {
            try
            {
                var accessToken = GetAccessToken(lineAccessToken);
                return new LineAccessTokenResponse
                {
                    AccessToken = accessToken.AccessTokenAccessToken,
                    TokenType = accessToken.TokenType,
                    ExpiresIn = accessToken.ExpiresIn,
                    Scope = accessToken?.Scope,
                    IdToken = accessToken?.IdToken,
                    RefreshToken = accessToken?.RefreshToken,
                };
            }
            catch (Exception ex)
            {
                Exception = ex;
                return null;
            }
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
            if (accessToken.Json is null)
                throw new ArgumentNullException(nameof(LineSDKAccessToken.Json));
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<AccessToken>(accessToken.Json);
            if (result is null)
                throw new ArgumentNullException("AccessToken Convert Error");

            return result;
        }

        UserProfile? GetUserProfile(LineSDKLoginResult arg1)
        {
            var profile = arg1.GetUserProfile();
            if (profile is null)
                return null;

            return new UserProfile(profile.UserID, profile.DisplayName)
            {
                PictureURL = profile.PictureURL is null ?
                    null :
                    new Uri(profile.PictureURL.ToString()),
                StatusMessage = profile.StatusMessage
            };
        }
    }
}
