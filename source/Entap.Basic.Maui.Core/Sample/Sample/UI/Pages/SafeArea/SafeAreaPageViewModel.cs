using System;
using Entap.Basic.Maui.Core;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace Sample
{
	public class SafeAreaPageViewModel : PageViewModelBase
    {
		public SafeAreaPageViewModel()
		{
		}

		public Command<ContentPage> SafeAreaCommand => new((page) =>
		{
			var isUsing = page.On<iOS>().UsingSafeArea();
			Device.BeginInvokeOnMainThread(() =>
			{
				page.On<iOS>().SetUseSafeArea(!isUsing);
			});
        });

    }
}

