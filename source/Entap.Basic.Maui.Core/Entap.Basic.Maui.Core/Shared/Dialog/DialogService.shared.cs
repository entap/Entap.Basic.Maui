using System;
namespace Entap.Basic.Maui.Core
{
	public partial class DialogService
    {
        static readonly Lazy<DialogService> _instance = new Lazy<DialogService>(() => new DialogService());
        public static DialogService Current => _instance.Value;

        public DialogService()
		{
		}

        public Task DisplayAlertAsync(string? title, string? message, string cancel)
            => PlatformDisplayAlertAsync(title, message, null, new DialogItem(cancel, DialogItemStyle.Cancel));

        public Task DisplayAlertAsync(string? title, string? message, string accept, string cancel)
            => PlatformDisplayAlertAsync(title, message, new DialogItem(accept), new DialogItem(cancel, DialogItemStyle.Cancel));

        public Task DisplayAlertAsync(string? title, string? message, DialogItem cancelItem)
            => PlatformDisplayAlertAsync(title, message, null, cancelItem);

        public Task<bool> DisplayAlertAsync(string? title, string? message, DialogItem? acceptItem, DialogItem cancelItem)
            => PlatformDisplayAlertAsync(title, message, acceptItem, cancelItem);

    }
}

