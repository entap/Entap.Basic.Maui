namespace Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Entap.Basic.Maui.Auth.Apple.Core.Init();

		MainPage = new AppShell();
	}
}

