using System;
using Entap.Basic.Maui.Chat;
using SQLite;

namespace Sample
{
	public class NotSendMessage : MessageBase
    {
        public NotSendMessage()
        {
        }
        public NotSendMessage(int roomId, MessageBase messageBase, string fileName = null)
        {
            MessageId = messageBase.MessageId;
            SendDateTime = messageBase.SendDateTime;
            Text = messageBase.Text;
            MediaUrl = messageBase.MediaUrl;
            UserIcon = messageBase.UserIcon;
            MessageType = messageBase.MessageType;
            SendUserId = messageBase.SendUserId;
            AlreadyReadCount = 0;
            ResendVisible = true;
            RoomId = roomId;
            FileName = fileName;
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string FileName { get; set; }
    }
}

