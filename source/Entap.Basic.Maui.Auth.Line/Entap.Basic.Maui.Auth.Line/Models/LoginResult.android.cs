using System;
using System.Threading.Tasks;
using Com.Linecorp.Linesdk;
using Com.Linecorp.Linesdk.Auth;

namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LoginResult
    {
        public LoginResult(LineLoginResult result)
        {
            if (result.ResponseCode == LineApiResponseCode.Success)
            {
                LineAccessToken = GetLineAccessToken(result);
                UserProfile = GetUserProfile(result.LineProfile);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(result);

                // https://developers.line.biz/ja/docs/android-sdk/handling-errors/
                IsCanceled = (result.ResponseCode == LineApiResponseCode.Cancel ||
                    result.ResponseCode == LineApiResponseCode.AuthenticationAgentError);
                Exception = new Exception(result.ErrorData?.ToString());
            }
        }

        LineAccessTokenResponse? GetLineAccessToken(LineLoginResult loginResult)
        {
            var lineCredential = loginResult.LineCredential;
            if (lineCredential is null) return null;
            return new LineAccessTokenResponse
            {
                AccessToken = lineCredential.AccessToken.TokenString,
                ExpiresIn = (int)lineCredential.AccessToken.ExpiresInMillis,
                IdToken = loginResult.LineIdToken?.RawString
            };
        }

        UserProfile? GetUserProfile(LineProfile? profile)
        {
            if (profile is null)
                return null;

            return new UserProfile(profile.UserId, profile.DisplayName)
            {
                PictureURL = (profile.PictureUrl is null) ? null : new Uri(profile.PictureUrl.ToString()),
                StatusMessage = profile.StatusMessage
            };
        }
    }
}
