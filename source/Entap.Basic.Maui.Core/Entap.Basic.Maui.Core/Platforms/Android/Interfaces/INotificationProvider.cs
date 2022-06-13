using System;
using Android.App;

namespace Entap.Basic.Maui.Core.Android
{
    /// <summary>
    /// 通知プロバイダー
    /// </summary>
    public interface INotificationProvider
	{
        /// <summary>
        /// 通知ID
        /// </summary>
        int NotificationId { get; }

        /// <summary>
        /// 通知チャネル生成処理
        /// https://developer.android.com/training/notify-user/channels?hl=ja#CreateChannel
        /// </summary>
        void CreateNotificationChannel();

        /// <summary>
        /// 通知
        /// https://developer.android.com/training/notify-user/build-notification?hl=ja
        /// </summary>
        Notification Notification { get; }
    }
}

