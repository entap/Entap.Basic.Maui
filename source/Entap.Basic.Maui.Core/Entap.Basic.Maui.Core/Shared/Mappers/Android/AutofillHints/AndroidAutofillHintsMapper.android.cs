using System;
using Android.Widget;

namespace Entap.Basic.Maui.Core
{
    public partial class AndroidAutofillHintsMapper
    {
        static void UpdateAutofillHints(Entry entry)
        {
            if (!OperatingSystem.IsAndroidVersionAtLeast(26)) { return; }
            if (entry is null) { return; }
            if (entry.Handler?.PlatformView is not EditText editText) { return; }

            var autofillHints = GetAutofillHints(entry);
            if (autofillHints?.Any() is not true) return;

            editText.ImportantForAutofill = global::Android.Views.ImportantForAutofill.Yes;
            editText.SetAutofillHints(autofillHints.ToArray());
        }
    }
}

