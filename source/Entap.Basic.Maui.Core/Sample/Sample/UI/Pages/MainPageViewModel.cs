using System;
using Entap.Basic.Maui.Core;

namespace Sample
{
	public class MainPageViewModel : PageViewModelBase
	{
		public MainPageViewModel()
		{
		}

		public ProcessCommand PageManagerCommand => new(async () =>
		{
			await Task.CompletedTask;
		});
	}
}

