using System;
namespace Sample
{
	public class ChatRoom
	{
		public ChatRoom(int roomId)
		{
			Id = roomId;
		}

		public int Id { get; private set; }

		public int LastReadMessageId { get; set; }

    }
}

