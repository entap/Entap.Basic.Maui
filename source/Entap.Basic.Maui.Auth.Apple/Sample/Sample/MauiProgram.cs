namespace Sample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.ConfigureMauiHandlers(handlers =>
			{
#if IOS || MACCATALYST
				handlers.AddHandler<Entap.Basic.Maui.Auth.Apple.AppleSignInButton, Entap.Basic.Maui.Auth.Apple.AppleSignInButtonHandler>();
#else
                handlers.AddHandler<Entap.Basic.Maui.Auth.Apple.AppleSignInButton, Entap.Basic.Maui.Core.DummyViewHandler>();
#endif

            });

		return builder.Build();
	}
}

