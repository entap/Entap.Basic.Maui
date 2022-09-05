namespace Sample;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    void AppleSignInButton_Clicked(System.Object sender, System.EventArgs e)
    {
        if (Entap.Basic.Maui.Auth.Apple.AppleSignInService.IsSupported)
            AppleSignInAsync().ConfigureAwait(false);
    }

    async Task AppleSignInAsync()
	{
        try
        {
            var service = new Entap.Basic.Maui.Auth.Apple.AppleSignInService();
            var result = await service.SignInAsync();
            await DisplayAlert("認証済しました。", $"{result.Email}{Environment.NewLine}{result.PersonName.FullName}", "OK");
        }
        catch (InvalidOperationException ex)
        {
            // 初期化未済
        }
        catch (NotSupportedException ex)
        {
            // サポート外
        }
        catch (OperationCanceledException ex)
        {
            // ユーザによるキャンセル
        }
        catch (Exception ex)
        {
            // 認証エラー
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
}


