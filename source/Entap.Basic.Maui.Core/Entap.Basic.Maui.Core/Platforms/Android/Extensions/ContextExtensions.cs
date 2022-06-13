using System;
using System.Runtime.Versioning;
using Android.App;
using Android.Content;
using Android.OS;

namespace Entap.Basic.Maui.Core.Platform.Android
{
	/// <summary>
    /// Context拡張機能
    /// </summary>
	public static class v
	{
        /// <summary>
        /// ForegroundServiceを起動する
        /// https://docs.microsoft.com/ja-jp/xamarin/android/app-fundamentals/services/foreground-services
        /// </summary>
        public static void StartForegroundServiceCompat<T>(this Context context, Bundle? args = null) where T : Service
        {
            var intent = new Intent(context, typeof(T));
            if (args != null)
            {
                intent.PutExtras(args);
            }

            if (OperatingSystem.IsAndroidVersionAtLeast(26))
            {
                // Android 8.0(Oreo)以降
                context.StartForegroundService(intent);
            }
            else
            {
                context.StartService(intent);
            }
        }

        /// <summary>
        /// Serviceを停止する
        /// </summary>
        public static bool StopService<T>(this Context context) where T : Service
        {   
            var intent = new Intent(context, typeof(T));
            return context.StopService(intent);
        }
    }
}

