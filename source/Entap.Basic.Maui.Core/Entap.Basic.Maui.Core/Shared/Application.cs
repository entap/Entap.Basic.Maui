using System;
using MauiApplication = Microsoft.Maui.Controls.Application;
namespace Entap.Basic.Maui.Core
{
	/// <summary>
    /// アプリケーション
    /// </summary>
	public class Application
	{
		public Application()
		{
		}

		public static MauiApplication Current => MauiApplication.Current ?? throw new InvalidOperationException();
	}
}

