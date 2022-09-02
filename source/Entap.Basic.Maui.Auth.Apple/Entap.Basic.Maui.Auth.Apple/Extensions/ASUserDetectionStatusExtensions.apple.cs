using System;
using AuthenticationServices;

namespace Entap.Basic.Maui.Auth.Apple
{
    public static class ASUserDetectionStatusExtensions
    {
        public static UserDetectionStatus ToUserDetectionStatus(this ASUserDetectionStatus status)
            => status switch
            {
                ASUserDetectionStatus.Unsupported => UserDetectionStatus.Unsupported,
                ASUserDetectionStatus.Unknown => UserDetectionStatus.Unknown,
                ASUserDetectionStatus.LikelyReal => UserDetectionStatus.LikelyReal,
                _ => throw new NotImplementedException(),
            };
    }
}
