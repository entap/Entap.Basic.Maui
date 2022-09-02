namespace Sample;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    void AppleSignInButton_Clicked(System.Object sender, System.EventArgs e)
    {
        if (OperatingSystem.IsIOS() ||
            OperatingSystem.IsMacOS())
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
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
}


