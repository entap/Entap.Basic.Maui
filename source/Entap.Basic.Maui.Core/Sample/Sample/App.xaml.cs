using Entap.Basic.Maui.Core;

namespace Sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
#if IOS
        iOSEntryTextContentTypeMapper.Apply();
#endif
#if ANDROID
        AndroidAutofillHintsMapper.Apply();
#endif

        PageManager.Navigation.SetNavigationMainPage<MainPage>(new MainPageViewModel());
    }
}