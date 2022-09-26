using System;
using Entap.Basic.Maui.Core;

namespace Sample
{
	public class MainPageViewModel : PageViewModelBase
	{
		public MainPageViewModel()
		{
		}

		int _lastReadMessageId = 10;
		public int LastReadMessageId
		{
			get => _lastReadMessageId;
			set => SetProperty(ref _lastReadMessageId, value);
		}


        public ProcessCommand ChatCommand => new ProcessCommand(async () =>
		{
			var dummyRoomId = 1;
			var chatRoom = new ChatRoom(dummyRoomId)
			{
				LastReadMessageId = LastReadMessageId,
			};
			await PageManager.Navigation.PushAsync<ChatPage>(new ChatPageViewModel(chatRoom));
		});
    }
}

