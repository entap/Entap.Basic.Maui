using System;
using Android.App;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Point = Android.Graphics.Point;
using Rect = Android.Graphics.Rect;

namespace Entap.Basic.Maui.Core.Android
{
	public partial class DisplaySizeService : IDisplaySizeService
	{
		public DisplaySizeService()
		{
		}

        Activity CurrentActivity => Microsoft.Maui.ApplicationModel.Platform.CurrentActivity ?? Android.Platform.Activity;

        public double GetAndroidNavigationBarHeight()
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(30))
            {
                var currentWindowMetrics = CurrentActivity.WindowManager?.CurrentWindowMetrics;
                if (currentWindowMetrics is null) return 0;
                var insets = currentWindowMetrics.WindowInsets.GetInsetsIgnoringVisibility(WindowInsets.Type.SystemBars());
                return insets.Bottom / DisplaySizeManager.Density;
            }

            return DisplaySizeManager.ScreenSize.Height - GetDisplayHeight() - GetStatusBarHeight();
        }

        public double GetTopNavigationHeight()
        {
            double defaultSize = 56;
            TypedValue tv = new TypedValue();
            if (CurrentActivity.Theme is null)
                return defaultSize;

            if (CurrentActivity.Theme.ResolveAttribute(Resource.Attribute.actionBarSize, tv, true))
            {
                using var metrix = GetDisplayMetrics();
                var actionBarHeight = TypedValue.ComplexToDimensionPixelSize(tv.Data, metrix);
                return actionBarHeight / DisplaySizeManager.Density;
            }
            return defaultSize;
        }

        public double GetStatusBarHeight()
        {
            var context = CurrentActivity.ApplicationContext;
            if (context is null)
                return GetWindowFrameTop();

            try
            {
                var resources = context.Resources;
                if (resources is null)
                    return GetWindowFrameTop();

                var resourceId = resources.GetIdentifier("status_bar_height", "dimen", "android");
                if (resourceId < 1)
                    return GetWindowFrameTop();

                return resources.GetDimensionPixelSize(resourceId) / DisplaySizeManager.Density;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetDisplaySize_Android GetStatusBarHeight: " + e);
                return GetWindowFrameTop();
            }
            
            int GetWindowFrameTop()
            {
                // onCreateで呼び出すとまだViewの計算が終わってないので0になる
                var rect = new Rect();
                CurrentActivity.Window?.DecorView.GetWindowVisibleDisplayFrame(rect);
                return rect.Top;
            }
        }

        double GetDisplayHeight()
        {
            using var metrics = GetDisplayMetrics();
            return (double)metrics.HeightPixels / (double)metrics.Density;
        }

        Display? GetDefaultDisplay() => CurrentActivity.WindowManager?.DefaultDisplay;

        DisplayMetrics GetDisplayMetrics()
        {
            var metrics = new DisplayMetrics();
            var display = GetDefaultDisplay();
            if (display is null)
            {
                metrics.Density = 1;
                return metrics;
            }
#pragma warning disable CS0618 // Type or member is obsolete
            display.GetMetrics(metrics);
#pragma warning restore CS0618 // Type or member is obsolete
            return metrics;
        }
    }
}

