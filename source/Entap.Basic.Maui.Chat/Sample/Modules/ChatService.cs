using System;
using System.Collections.ObjectModel;
using Entap.Basic.Maui.Chat;
using Entap.Basic.Maui.Core;

namespace Sample
{
	public class ChatService : IChatService
	{
        public const string DummyMyId = "MyId";
        public const string DummyOthersId = "DummyOthersId";

        const string DummyUserIcon = "https://entap.co.jp/wp-content/uploads/2021/09/IMG_0052-scaled-e1630721195341.jpg";

        public ChatService()
		{
		}

        public void AddNotSendMessages(int roomId, ObservableCollection<MessageBase> messageBases)
        {
            var items = NotSendMessageManager.Instance.GetItems(roomId);
            if (items?.Any() != true) return;

            messageBases.AddRange(items);
        }

        public void DeleteNotSendMessageData(int id)
        {
            NotSendMessageManager.Instance.DeleteItem(id);
        }

        public void Dispose()
        {
        }

        public Task<IEnumerable<MessageBase>> GetMessagesAsync(int roomId, int messageId, int messageDirection)
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(GetMessagesAsync)} : {messageId} - {messageDirection}");
            var messages = messageDirection == (int)Entap.Basic.Maui.Chat.MessageDirection.New ?
                CreateNewMessage(messageId) :
                CreateOldMessage(messageId);
            return Task.FromResult(messages);
        }

        IEnumerable<MessageBase> CreateNewMessage(int messageId)
        {
            if (messageId > 20) return null;
            var result = new List<MessageBase>();
            for (int i=messageId; i < messageId + 10; i++)
            {
                result.Add(new MessageBase() { SendUserId = GetDummyUserId(i), MessageId = i, MessageType = (int)MessageType.Text, Text = i.ToString(), SendDateTime = DateTime.Now, AlreadyReadCount = 2, UserIcon = DummyUserIcon, UserName = "テスト 太郎" });
            }
            return result;
        }

        IEnumerable<MessageBase> CreateOldMessage(int messageId)
        {
            if (messageId == 0)
                return null;

            var result = new List<MessageBase>();
            for (int i = messageId; i > 0 && i > messageId - 10; i--)
            {
                result.Add(new MessageBase() { SendUserId = GetDummyUserId(i), MessageId = i, MessageType = (int)MessageType.Text, Text = i.ToString(), SendDateTime = DateTime.Now, AlreadyReadCount = 2, UserIcon = DummyUserIcon, UserName = "テスト 太郎" });
            }
            return result;
        }

        string GetDummyUserId(int messageId) => messageId % 2 == 0 ? DummyMyId : DummyOthersId;

        public string GetNotSendMediaSaveFolderPath()
        {
            return "";
        }

        public Task<List<ChatMemberBase>> GetRoomMembers(int roomId)
        {
            return null;
        }

        public string GetSendImageSaveFolderPath()
        {
            return "";
        }

        public string GetSendVideoSaveFolderPath()
        {
            return "";
        }

        public string GetUserId()
        {
            return DummyMyId;
        }

        public void SaveNotSendMessageData(int roomId, MessageBase messageBase, string fileName = null)
        {
            var notSendMessage = new NotSendMessage(roomId, messageBase, fileName);
            NotSendMessageManager.Instance.SaveItem(notSendMessage);
        }

        public Task<bool> SendAlreadyRead(int roomId, int messageId)
        {
            return Task.FromResult(true);
        }

        public Task<SendMessageResponseBase> SendMessage(int roomId, MessageBase msg, int notSendMessageId, CancellationTokenSource cts = null)
        {
            throw new NotImplementedException();
        }

        public Task SetRoomMembers(int roomId)
        {
            return Task.CompletedTask;
        }

        public void UpdateData(ObservableCollection<MessageBase> messageBases, int roomId)
        {
        }
    }
}

