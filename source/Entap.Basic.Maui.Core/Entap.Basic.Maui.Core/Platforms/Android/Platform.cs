using System;
using Android.App;
using Microsoft.Maui.Controls.Internals;

namespace Entap.Basic.Maui.Core.Android
{
    [Preserve(AllMembers = true)]
    public static class Platform
    {
        static Activity? _activity;
        public static Activity Activity
        {
            get
            {
                if (_activity is null)
                    throw new InvalidOperationException($"You MUST call {typeof(Platform)}.{nameof(Init)}; prior to using it.");
                return _activity;
            }
            private set
            {
                _activity = value;
            }
        }

        public static void Init(Activity activity, bool disablePlatformHandling = false)
        {
            if (activity is null)
                throw new ArgumentNullException(nameof(activity));

            Activity = activity;

            if (!disablePlatformHandling)
                PlatformHandler.HandleAll(activity);
        }
    }
}

