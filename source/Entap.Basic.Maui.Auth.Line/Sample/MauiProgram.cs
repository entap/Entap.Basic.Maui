﻿using Entap.Basic.Maui.Auth.Line;

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
			.ConfigureMauiHandlers((handlers) =>
			{
				handlers.AddHandler(typeof(LineLoginButton), typeof(LineLoginButtonHandler));
			});

		return builder.Build();
	}
}

