using System;
namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LoginResult
    {
        public LoginResult()
        {
        }

        public bool IsCanceled { get; internal set; }

        public Exception? Exception { get; internal set; }

        public bool IsFaulted => Exception is not null;

        public LineAccessTokenResponse? LineAccessToken { get; internal set; }

        public UserProfile? UserProfile { get; internal set; }
    }
}
