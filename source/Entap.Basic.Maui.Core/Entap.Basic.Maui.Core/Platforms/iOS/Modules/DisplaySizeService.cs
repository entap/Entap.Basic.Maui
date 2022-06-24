using System;
using UIKit;

namespace Entap.Basic.Maui.Core.iOS
{
	public partial class DisplaySizeService : IDisplaySizeService
	{   
        public double GetTopNavigationHeight()
        {
            var currentNavigationViewController = Platform.GetCurrentNavigationViewController();
            if (currentNavigationViewController is null) return 0;

            return currentNavigationViewController.NavigationBar.Frame.Height;
        }

        public double GetStatusBarHeight()
        {
            if (OperatingSystem.IsIOSVersionAtLeast(13, 0))
            {
                var keyWindow = Platform.GetKeyWindow();
                return keyWindow?.WindowScene?.StatusBarManager?.StatusBarFrame.Height ?? 0;
            }
            
            return UIApplication.SharedApplication.StatusBarFrame.Height;
        }

        public double GetAndroidNavigationBarHeight()
        {
            throw new NotImplementedException();
        }
    }
}

