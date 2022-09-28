using System;
using Entap.Basic.Maui.Chat;
using System.Collections.ObjectModel;
using Entap.Basic.Maui.Core;

namespace Sample
{
	public class ChatPageViewModel : PageViewModelBase
	{
        readonly ChatRoom _chatRoom;
        public ChatPageViewModel(ChatRoom chatRoom)
		{
            _chatRoom = chatRoom;
        }

		public ChatRoom ChatRoom => _chatRoom;

        public ObservableCollection<MessageBase> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }
        ObservableCollection<MessageBase> _messages = new ObservableCollection<MessageBase>();

        string _inputText;
        public string InputText
        {
            get => _inputText;
            set => SetProperty(ref _inputText, value);
        }

        public Command MenuCommand => new Command(() =>
        {
            var messageId = GetNextMessageId();
            var message = new MessageBase(ChatService.DummyMyId) { MessageId = messageId, MessageType = (int)MessageType.Image, MediaUrl = "https://entap.co.jp/wp-content/uploads/2020/12/top.png", SendDateTime = DateTime.Now };
            Messages.Add(message);
        });

        public Command SendCommand => new Command(() =>
        {
            if (string.IsNullOrWhiteSpace(InputText)) return;

            var messageId = GetNextMessageId();
            var message = new MessageBase(ChatService.DummyMyId) { MessageId = messageId, MessageType = (int)MessageType.Text, Text = InputText, SendDateTime = DateTime.Now };
            Messages.Add(message);
            InputText = null;
        });

        int GetNextMessageId()
        {
            var lastMessageId = Messages.LastOrDefault()?.MessageId ?? 0;
            return lastMessageId + 1;
        }
    }
}

