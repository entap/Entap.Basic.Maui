using System;
namespace Entap.Basic.Maui.Auth.Apple
{
    /// <summary>
    /// ユーザ検出ステータス
    /// https://developer.apple.com/documentation/authenticationservices/asuserdetectionstatus
    /// </summary>
    public enum UserDetectionStatus
    {
        Unsupported,
        Unknown,
        LikelyReal
    }
}
