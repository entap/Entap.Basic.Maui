using System;
using System.Threading.Tasks;
using Com.Linecorp.Linesdk;
using Com.Linecorp.Linesdk.Auth;

namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LoginResult
    {
#nullable enable
        public LoginResult(LineLoginResult result)
#nullable disable
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

        LineAccessTokenResponse GetLineAccessToken(LineLoginResult loginResult)
        {
            var lineCredential = loginResult.LineCredential;
            if (lineCredential is null) return null;
            return new LineAccessTokenResponse
            {
                AccessToken = lineCredential.AccessToken.TokenString,
                ExpiresIn = (int)lineCredential.AccessToken.ExpiresInMillis,
                IdToken = loginResult.LineIdToken.RawString
            };
        }

#nullable enable
        UserProfile? GetUserProfile(LineProfile? profile)
#nullable disable
        {
            if (profile is null)
                return null;

            return new UserProfile
            {
                UserId = profile.UserId,
                DisplayName = profile.DisplayName,
                PictureURL = (profile.PictureUrl is null) ? null : new Uri(profile.PictureUrl.ToString()),
                StatusMessage = profile.StatusMessage
            };
        }
    }
}
