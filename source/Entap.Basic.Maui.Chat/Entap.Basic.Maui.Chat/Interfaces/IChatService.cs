using System;
using System.Collections.ObjectModel;

namespace Entap.Basic.Maui.Chat
{
    [Preserve(AllMembers = true)]
    public interface IChatService
	{
        Task<IEnumerable<MessageBase>> GetMessagesAsync(int roomId, int messageId, int messageDirection);
        Task<bool> SendAlreadyRead(int roomId, int messageId);
        Task<List<ChatMemberBase>> GetRoomMembers(int roomId);
        Task<SendMessageResponseBase> SendMessage(int roomId, MessageBase msg, int notSendMessageId, CancellationTokenSource? cts = null);
        void UpdateData(ObservableCollection<MessageBase> messageBases, int roomId);
        void Dispose();
        void AddNotSendMessages(int roomId, ObservableCollection<MessageBase> messageBases);
        void SaveNotSendMessageData(int roomId, MessageBase messageBase, string? fileName = null);
        void DeleteNotSendMessageData(int id);
        string GetUserId();
        string GetSendImageSaveFolderPath();
        string GetSendVideoSaveFolderPath();
        string GetNotSendMediaSaveFolderPath();
        Task SetRoomMembers(int roomId);
    }
}

