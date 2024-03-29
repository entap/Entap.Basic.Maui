﻿using System;
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
            // 通信成功時
            //var messageId = GetNextMessageId();
            //var message = new MessageBase(ChatService.DummyMyId) { MessageId = messageId, MessageType = (int)MessageType.Image, MediaUrl = "https://entap.co.jp/wp-content/uploads/2020/12/top.png", SendDateTime = DateTime.Now };

            // 通信エラー時
            var messageId = ChatListView.NotSendMessageId;
            var message = new NotSendMessage(_chatRoom.Id, new MessageBase() { SendUserId = ChatService.DummyMyId, MessageId = messageId, MessageType = (int)MessageType.Image, MediaUrl = "https://entap.co.jp/wp-content/uploads/2020/12/top.png", ResendVisible = true });

            SendMessageAsync(message).ConfigureAwait(false);
            Messages.Add(message);
        });

        public Command SendCommand => new Command(() =>
        {
            if (string.IsNullOrWhiteSpace(InputText)) return;

            // 通信成功時
            //var messageId = GetNextMessageId();
            //var message = new MessageBase(ChatService.DummyMyId) { MessageId = messageId, MessageType = (int)MessageType.Text, Text = InputText, SendDateTime = DateTime.Now };

            // 通信エラー時
            var messageId = ChatListView.NotSendMessageId;
            var message = new NotSendMessage(_chatRoom.Id, new MessageBase() { SendUserId = ChatService.DummyMyId, MessageId = messageId, MessageType = (int)MessageType.Text, Text = InputText, ResendVisible = true });

            SendMessageAsync(message).ConfigureAwait(false);
            Messages.Add(message);
            InputText = null;
        });

        async Task SendMessageAsync(MessageBase message)
        {
            if (message.MessageType == (int)MessageType.Image ||
                message.MessageType == (int)MessageType.Video)
            {
                message.ResendVisible = false;
                message.ProgressVisible = true;
                await Task.Run(async() =>
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        await Task.Delay(50);
                        message.UploadProgress = i * 0.01;
                    }
                    message.ProgressVisible = false;
                    if (message.MessageId == ChatListView.NotSendMessageId)
                        message.ResendVisible = true;
                });
            };

            if (message.MessageId == ChatListView.NotSendMessageId)
            {
                Settings.Current.ChatService.SaveNotSendMessageData(_chatRoom.Id, message);
                return;
            }
        }

        public Command<MessageBase> CancelSendingMessageCommand => new Command<MessageBase>((message) =>
        {
            message.ProgressVisible = false;
            message.ResendVisible = true;
        });

        public ProcessCommand<MessageBase> ResendCommand => new ProcessCommand<MessageBase>(async (message) =>
        {
            System.Diagnostics.Debug.WriteLine($"Resend : {message.MessageId}");

            const string Resend = "再送する";
            const string Cancel = "取り消し";

            var options = new string[] { Resend, Cancel };
            var option = await Application.Current.MainPage.DisplayActionSheet(null, "キャンセル", null, options);
            switch (option)
            {
                case Resend:
                    ResendMessage(message);
                    break;
                case Cancel:
                    DeleteMessage(message);
                    break;
                default:
                    break;
            }
        });

        async void ResendMessage(MessageBase message)
        {
            if (message is not NotSendMessage notSendMessage) return;

            Settings.Current.ChatService.DeleteNotSendMessageData(notSendMessage.Id);

            message.ResendVisible = false;
            message.MessageId = GetNextMessageId();
            message.SendDateTime = DateTime.Now;

            await SendMessageAsync(message);
            _messages.Remove(notSendMessage);
            _messages.Add(message);
        }

        void DeleteMessage(MessageBase message)
        {
            if (message is not NotSendMessage notSendMessage) return;
                Settings.Current.ChatService.DeleteNotSendMessageData(notSendMessage.Id);
            Messages.Remove(message);
        }

        public Command<MessageBase> ImageTappedCommand => new Command<MessageBase>((message) =>
        {
            System.Diagnostics.Debug.WriteLine($"Image Tapped : {message.MessageId}");
        });

        int GetNextMessageId()
        {
            var lastMessageId = Messages
                .LastOrDefault((message) => message.MessageId != ChatListView.NotSendMessageId)?
                .MessageId ?? 0;
            return lastMessageId + 1;
        }
    }
}

