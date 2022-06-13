using System;
using Android.App;
using Android.OS;
using Android.Views;

namespace Entap.Basic.Maui.Core.Android
{
    /// <summary>
    /// 自動入力のハンドリングを行う
    /// </summary>
	public class AutofillHandler
	{
        /// <summary>
        /// 自動入力の無効化
        /// 自動入力により、Android 8.0、8.1でアプリがクラッシュ
        /// https://developer.android.com/guide/topics/text/autofill-optimize#autofill_causes_apps_to_crash_on_android_80_81
        /// </summary>
        public static void DisableAutofill(Activity activity)
        {
            if (!OperatingSystem.IsAndroidVersionAtLeast(26)) return;
            var window = activity.Window;
            if (window is null) return;
            
            if (Build.VERSION.SdkInt == BuildVersionCodes.O ||
                Build.VERSION.SdkInt == BuildVersionCodes.OMr1)
            {
                window.DecorView.ImportantForAutofill = ImportantForAutofill.NoExcludeDescendants;
            }
        }
    }
}

