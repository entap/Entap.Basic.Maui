using Entap.Basic.Maui.Core;

namespace Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		Entap.Basic.Maui.Auth.Line.LineAuthService.Init("1655277852");

		PageManager.Navigation.SetNavigationMainPage<MainPage>(new MainPageViewModel());
    }
}

