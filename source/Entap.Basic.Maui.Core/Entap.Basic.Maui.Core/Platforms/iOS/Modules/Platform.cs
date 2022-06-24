using System;
using UIKit;

namespace Entap.Basic.Maui.Core.iOS
{
    public class Platform
    {
        public Platform()
        {
        }


        public static UINavigationController? GetCurrentNavigationViewController()
        {
            var viewController = Microsoft.Maui.ApplicationModel.Platform.GetCurrentUIViewController();
            if (viewController is null) return null;
            return GetCurrentNavigationViewController(viewController);
        }

        public static UINavigationController? GetCurrentNavigationViewController(UIViewController currentViewController)
        {
            if (currentViewController is UITabBarController tabBarController)
                return tabBarController.SelectedViewController as UINavigationController;
            return
                currentViewController.ChildViewControllers?
                    .LastOrDefault((vc) => vc.NavigationController is not null)?.NavigationController;
        }

        public static UIWindow? GetKeyWindow()
        {
            if (OperatingSystem.IsIOSVersionAtLeast(15, 0))
            {
                var scene = UIApplication.SharedApplication.ConnectedScenes.ToArray().FirstOrDefault();
                return (scene as UIWindowScene)?.Windows?.FirstOrDefault();
            }

            if (OperatingSystem.IsIOSVersionAtLeast(13, 0))
            {
                return UIApplication.SharedApplication.Windows.FirstOrDefault((arg) => arg.IsKeyWindow);
            }

            return UIApplication.SharedApplication.KeyWindow;
        }
    }
}

