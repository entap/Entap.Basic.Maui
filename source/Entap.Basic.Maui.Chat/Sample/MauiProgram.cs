using Microsoft.Maui.Controls.Compatibility.Hosting;

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
            .UseMauiCompatibility()
			.ConfigureMauiHandlers((handlers) =>
			{
#if IOS
                handlers.AddHandler(typeof(Entap.Basic.Maui.Chat.ChatListView), typeof(Entap.Basic.Maui.Chat.Platforms.iOS.ChatListViewRenderer));
#endif
			});

		return builder.Build();
	}
}

