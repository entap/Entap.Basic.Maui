using System;

namespace Entap.Basic.Maui.Core
{
    public partial class DialogService : IPlatformDialogService
    {
        public Task<bool> PlatformDisplayAlertAsync(string? title, string? message, DialogItem? acceptItem, DialogItem cancelItem)
        {
            throw new NotImplementedException();
        }
    }
}