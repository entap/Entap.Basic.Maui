namespace Sample;
﻿using Entap.Basic.Maui.Core;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
		Entap.Basic.Maui.Auth.Line.LineAuthService.Init("1655277852");
}

