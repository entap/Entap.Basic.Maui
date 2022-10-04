using System;
namespace Entap.Basic.Maui.Auth.Line
{
    /// <summary>
	/// ユーザープロフィール
    /// iOS:https://github.com/line/line-sdk-ios-swift/blob/master/LineSDK/LineSDK/Login/Model/UserProfile.swift
    /// Android:https://github.com/line/line-sdk-android/blob/master/line-sdk/src/main/java/com/linecorp/linesdk/LineProfile.java
	/// </summary>
    public class UserProfile
    {
        public UserProfile()
        {
        }

        public string UserId { get; internal set; }

        public string DisplayName { get; internal set; }

        public Uri PictureURL { get; internal set; }

        public string StatusMessage { get; internal set; }
    }
}
