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
				handlers.AddCompatibilityRenderer(typeof(Entap.Basic.Maui.Chat.DynamicResizedEditor), typeof(Entap.Basic.Maui.Chat.Platforms.iOS.DynamicResizedEditorRenderer));
#endif
#if ANDROID
                handlers.AddHandler(typeof(Entap.Basic.Maui.Chat.ChatListView), typeof(Entap.Basic.Maui.Chat.Platforms.Android.ChatListViewRenderer));
                handlers.AddCompatibilityRenderer(typeof(Entap.Basic.Maui.Chat.DynamicResizedEditor), typeof(Entap.Basic.Maui.Chat.Platforms.Android.DynamicResizedEditorRenderer));
#endif
            });

		return builder.Build();
	}
}

