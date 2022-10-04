using System;
namespace Entap.Basic.Maui.Auth.Line
{
    public partial class LoginResult
    {
        public LoginResult()
        {
        }

        public bool IsCanceled { get; internal set; }

#nullable enable
        public Exception? Exception { get; internal set; }
#nullable disable

        public bool IsFaulted => Exception is not null;

#nullable enable
        public LineAccessTokenResponse? LineAccessToken { get; internal set; }
#nullable disable

#nullable enable
        public UserProfile? UserProfile { get; internal set; }
#nullable disable
    }
}
