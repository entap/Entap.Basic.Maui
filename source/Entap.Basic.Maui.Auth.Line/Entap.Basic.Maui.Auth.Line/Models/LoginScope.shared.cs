using System;
namespace Entap.Basic.Auth.Line
{
	/// <summary>
	/// ログインスコープ
	/// https://developers.line.biz/ja/docs/line-login/integrate-line-login/#scopes
	/// iOS:https://github.com/line/line-sdk-ios-swift/blob/master/LineSDK/LineSDK/Login/LoginPermission.swift
	/// Android:https://github.com/line/line-sdk-android/blob/master/line-sdk/src/main/java/com/linecorp/linesdk/Scope.java
	/// </summary>
	public enum LoginScope
	{
		OpenID,
		Profile,
		Email,
	}
}
