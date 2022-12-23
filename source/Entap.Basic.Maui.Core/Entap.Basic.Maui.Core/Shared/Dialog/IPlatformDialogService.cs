namespace Entap.Basic.Maui.Core
{
    public interface IPlatformDialogService
    {
        Task<bool> PlatformDisplayAlertAsync(string? title, string? message, DialogItem? acceptItem, DialogItem cancelItem);
    }
}