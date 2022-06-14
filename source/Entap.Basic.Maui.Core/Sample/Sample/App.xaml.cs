using Entap.Basic.Maui.Core;

namespace Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		PageManager.Navigation.SetNavigationMainPage<MainPage>();
	}
}

