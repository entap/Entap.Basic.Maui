using System;
namespace Entap.Basic.Maui.Chat
{
    [Preserve(AllMembers = true)]
    public class ChatMemberBase
	{
        public ChatMemberBase(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public string UserId { get; set; }
        public string? UserIcon { get; set; }
        public string UserName { get; set; }
        public int RoomAdmin { get; set; }
    }
}

