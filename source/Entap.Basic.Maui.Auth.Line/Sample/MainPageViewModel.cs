using System;
using Entap.Basic.Maui.Auth.Line;
using Entap.Basic.Maui.Core;

namespace Sample
{
    public class MainPageViewModel : PageViewModelBase
    {
        public MainPageViewModel()
        {
        }

        public ProcessCommand<LoginResult> LineLoginCommand => new (async (result) =>
        {
            await OnLoginAsync(result);
        });

        public ProcessCommand CustomLineLoginCommand => new(async () =>
        {
            try
            {
                var authService = new LineAuthService();
                var result = await authService.LoginAsync(LoginScope.Email, LoginScope.OpenID, LoginScope.Profile);
                await OnLoginAsync(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await App.Current.MainPage.DisplayAlert("LINE認証エラー", null, "OK");
            }
        });

        async Task OnLoginAsync(LoginResult result)
        {
            if (result.IsCanceled)
            {
                System.Diagnostics.Debug.WriteLine("LineLogin canceled");
                return;
            }
            if (result.IsFaulted)
            {
                await App.Current.MainPage.DisplayAlert("LINE認証エラー", result.Exception.ToString(), "OK");
                return;
            }
            await App.Current.MainPage.DisplayAlert("LINE認証成功", null, "OK");
            // ToDo : 認証処理
        }
    }
}

