using System;
using CoreFoundation;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace Entap.Basic.Maui.Core
{
    public partial class DialogService
    {
        const float AlertPadding = 10.0f;

        public Task<bool> PlatformDisplayAlertAsync(string title, string message, DialogItem? acceptItem, DialogItem cancelItem)
        {
            // https://github.com/dotnet/maui/blob/73a06de15c0b79aa9441e44b3f8bf1ec80920f1a/src/Controls/src/Core/Platform/AlertManager/AlertManager.iOS.cs#L98
            var taskCompletionSource = new TaskCompletionSource<bool>();
            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
                var window = new UIWindow { BackgroundColor = Colors.Transparent.ToPlatform() };
                if (alert.View is not null)
                {
                    var oldFrame = alert.View.Frame;
                    alert.View.Frame = new RectF((float)oldFrame.X, (float)oldFrame.Y, (float)oldFrame.Width, (float)oldFrame.Height - AlertPadding * 2);
                }

                // iOSとMacCatalystではメニューの表示順序が異なるため、追加順序を調整
#if MACCATALYST
                if (acceptItem != null)
                {
                    alert.AddAction(CreateActionWithWindowHide(acceptItem.Text, ToAlertActionStyle(acceptItem.Style), () => taskCompletionSource.SetResult(true), window));
                }
#endif
                if (cancelItem != null)
                {
                    alert.AddAction(CreateActionWithWindowHide(cancelItem.Text, ToAlertActionStyle(cancelItem.Style), () => taskCompletionSource.SetResult(false), window));
                }
#if IOS
                if (acceptItem != null)
                {
                    alert.AddAction(CreateActionWithWindowHide(acceptItem.Text, ToAlertActionStyle(acceptItem.Style), () => taskCompletionSource.SetResult(true), window));
                }
#endif

                PresentPopUp(window, alert, taskCompletionSource);
            });
            return taskCompletionSource.Task;
        }

        // Creates a UIAlertAction which includes a call to hide the presenting UIWindow at the end
        private UIAlertAction CreateActionWithWindowHide(string text, UIAlertActionStyle style, Action setResult, UIWindow window)
        {
            return UIAlertAction.Create(text, style,
                a =>
                {
                    window.Hidden = true;
                    setResult();
                });
        }

        // https://github.com/dotnet/maui/blob/73a06de15c0b79aa9441e44b3f8bf1ec80920f1a/src/Compatibility/Core/src/iOS/Platform.cs#L510
        public static void PresentPopUp<T>(UIWindow window, UIAlertController alert, TaskCompletionSource<T>? arguments = null)
        {
            window.RootViewController = new UIViewController();
            if (window.RootViewController.View is not null)
                window.RootViewController.View.BackgroundColor = Colors.Transparent.ToPlatform();
            window.WindowLevel = UIWindowLevel.Alert + 1;
            window.MakeKeyAndVisible();

            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad && arguments != null)
            {
                UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications();
                var observer = NSNotificationCenter.DefaultCenter.AddObserver(UIDevice.OrientationDidChangeNotification,
                    n =>
                    {
                        if (alert.PopoverPresentationController is null) return;
                        if (window.RootViewController.View is null) return;
                        alert.PopoverPresentationController.SourceRect = window.RootViewController.View.Bounds;
                    });

                arguments.Task.ContinueWith(t =>
                {
                    NSNotificationCenter.DefaultCenter.RemoveObserver(observer);
                    UIDevice.CurrentDevice.EndGeneratingDeviceOrientationNotifications();
                }, TaskScheduler.FromCurrentSynchronizationContext());

                if (alert.PopoverPresentationController is not null)
                {
                    if (window.RootViewController.View != null)
                    {
                        alert.PopoverPresentationController.SourceView = window.RootViewController.View;
                        alert.PopoverPresentationController.SourceRect = window.RootViewController.View.Bounds;
                    }
                    alert.PopoverPresentationController.PermittedArrowDirections = 0; // No arrow
                }
            }

            window.RootViewController.PresentViewController(alert, true, null);

            //var s = Microsoft.Maui.ApplicationModel.Platform.GetCurrentUIViewController();
            //s.PresentViewController(alert, true, null);
        }

        private static UIWindow CreateWindow() => new UIWindow { BackgroundColor = Colors.Transparent.ToPlatform() };

        private UIAlertActionStyle ToAlertActionStyle(DialogItemStyle style)
        {
            switch (style)
            {
                case DialogItemStyle.Cancel:
                    return UIAlertActionStyle.Cancel;
                case DialogItemStyle.Destructive:
                    return UIAlertActionStyle.Destructive;
                default:
                    return UIAlertActionStyle.Default;
            }
        }
    }
}

