using System;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace Entap.Basic.Maui.Core
{
	/// <summary>
	/// HttpClient拡張機能
	/// </summary>
	public static class HttpClientExtensions
	{
        /// <summary>
        /// JsonAcceptヘッダを設定する
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        public static void SetJsonAcceptHeader(this HttpClient httpClient)
        {
            SetAcceptHeader(httpClient, MediaTypeNames.Application.Json);
        }

        /// <summary>
        /// Acceptヘッダを設定する
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="mediaType"></param>
        public static void SetAcceptHeader(this HttpClient httpClient, string mediaType)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }

        /// <summary>
        /// 認証情報を設定する
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="type">認証方法</param>
        /// <param name="token">トークン</param>
        public static void SetAuthorization(this HttpClient httpClient, string type, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, token);
        }

        /// <summary>
        /// 認証情報を削除する
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        public static void ClearAuthorization(this HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}

