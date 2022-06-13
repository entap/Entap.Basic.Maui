using System;
using Android.App;

namespace Entap.Basic.Maui.Core.Android
{
	/// <summary>
    /// プラットフォーム固有のハンドリングを行う
    /// </summary>
	public static class PlatformHandler
	{
		public static void HandleAll(Activity activity)
        {
			AutofillHandler.DisableAutofill(activity);
        }
	}
}

