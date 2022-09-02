namespace Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Entap.Basic.Maui.Auth.Apple.Core.Init();

        if (OperatingSystem.IsIOS() ||
            OperatingSystem.IsMacOS())
        {
            Entap.Basic.Maui.Auth.Apple.AppleSignInService.Init(
                Entap.Basic.Maui.Auth.Apple.AuthorizationScope.Email,
                Entap.Basic.Maui.Auth.Apple.AuthorizationScope.FullName
            );
        }

        MainPage = new AppShell();
	}
}

