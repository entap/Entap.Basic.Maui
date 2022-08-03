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
				// https://www.notion.so/entap/MAUI-iOS-Editor-6c3c43325ea14e98920795b94db2538e
				handlers.AddCompatibilityRenderer(typeof(Editor), typeof(Microsoft.Maui.Controls.Compatibility.Platform.iOS.EditorRenderer));
#endif
            });

        return builder.Build();
	}
}

