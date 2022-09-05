namespace Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Entap.Basic.Maui.Auth.Apple.Core.Init();

        if  (Entap.Basic.Maui.Auth.Apple.AppleSignInService.IsSupported)
        {
            Entap.Basic.Maui.Auth.Apple.AppleSignInService.Init(
                Entap.Basic.Maui.Auth.Apple.AuthorizationScope.Email,
                Entap.Basic.Maui.Auth.Apple.AuthorizationScope.FullName
            );

            // AppleID使用停止時処理
            Entap.Basic.Maui.Auth.Apple.AppleSignInService.RegisterCredentialRevokedActionAsync(() =>
            {
                System.Diagnostics.Debug.WriteLine("CredentialRevoked");
            });
        }

        MainPage = new AppShell();
	}
}

