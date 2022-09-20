using System;
using Entap.Basic.Maui.Core;

namespace Sample
{
	public class MainPageViewModel : PageViewModelBase
	{
		public MainPageViewModel()
		{
		}

		public ProcessCommand ChatCommand => new ProcessCommand(async () =>
		{
			await PageManager.Navigation.PushAsync<ChatPage>(new ChatPageViewModel());
		});

    }
}

