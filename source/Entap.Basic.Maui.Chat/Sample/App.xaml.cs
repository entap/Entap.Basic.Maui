using Entap.Basic.Maui.Core;
using Entap.Basic.SQLite;

namespace Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        Entap.Basic.Maui.Chat.Settings.Current.Init(new ChatService());
        SQLiteConnectionManager.Init(new SQLiteConnectionService());

        PageManager.Navigation.SetNavigationMainPage<MainPage>(new MainPageViewModel());
	}
}

