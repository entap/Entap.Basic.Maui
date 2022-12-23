using System;
using Android.App;
using Android.Content;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Platform;
using Org.Apache.Commons.Logging;

namespace Entap.Basic.Maui.Core
{
    public partial class DialogService : IPlatformDialogService
    {
        DialogStyle? _dialogStyle;

        Activity? CurrentActivity => Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;

        public Task<bool> PlatformDisplayAlertAsync(string title, string message, DialogItem? acceptItem, DialogItem cancelItem)
        {
            if (CurrentActivity is null) return Task.FromException<bool>(new NullReferenceException(nameof(CurrentActivity)));

            var taskCompletionSource = new TaskCompletionSource<bool>();
            var alert = new AlertDialog.Builder(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity).Create();
            if (alert is null) return Task.FromException<bool>(new NullReferenceException(nameof(alert)));

            alert.SetTitle(title);
            alert.SetMessage(message);

            if (acceptItem != null)
                alert.SetButton((int)DialogButtonType.Positive, acceptItem.Text, (o, args) => taskCompletionSource.SetResult(true));
            alert.SetButton((int)DialogButtonType.Negative, cancelItem.Text, (o, args) => taskCompletionSource.SetResult(false));
            alert.CancelEvent += (o, args) => { taskCompletionSource.SetResult(false); };
            CurrentActivity.RunOnUiThread(() =>
            {
                alert.Show();

                if (_dialogStyle is null) return;

                if (acceptItem is not null)
                {
                    var acceptItemColor = GetTextColor(acceptItem);
                    if (acceptItemColor is not null)
                        alert.GetButton((int)DialogButtonType.Positive)?.SetTextColor(acceptItemColor.ToPlatform());
                }

                var cancelItemColor = GetTextColor(cancelItem);
                if (cancelItemColor is not null)
                    alert.GetButton((int)DialogButtonType.Negative)?.SetTextColor(cancelItemColor.ToPlatform());
            });
            return taskCompletionSource.Task;
        }

        public void SetDialogStyle(DialogStyle dialogStyle)
        {
            _dialogStyle = dialogStyle;
        }

        private Color? GetTextColor(DialogItem item)
        {
            if (_dialogStyle is null) return null;

            switch (item.Style)
            {
                case DialogItemStyle.Cancel:
                    return _dialogStyle.CancelTextColor;
                case DialogItemStyle.Destructive:
                    return _dialogStyle.DestructiveTextColor;
                default:
                    return _dialogStyle.DefaultTextColor;
            }
        }
    }
}

