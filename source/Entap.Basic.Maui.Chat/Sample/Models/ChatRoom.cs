using System;

namespace Sample
{
	public class ChatRoom
	{
		public ChatRoom(int roomId, bool isGroup = false)
        {
			Id = roomId;
            IsGroup = isGroup;
        }

		public int Id { get; private set; }

        public bool IsGroup { get; private set; }

        public int LastReadMessageId { get; set; }

    }
}

