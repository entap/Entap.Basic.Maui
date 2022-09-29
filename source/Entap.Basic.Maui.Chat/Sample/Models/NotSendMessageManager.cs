using System;
using Entap.Basic.SQLite;
using SQLite;

namespace Sample
{
	public class NotSendMessageManager
	{
        private static Lazy<NotSendMessageManager> _instance = new Lazy<NotSendMessageManager>();
        public static NotSendMessageManager Instance => _instance.Value;

        static readonly object Locker = new object();
        SQLiteConnection Connection => SQLiteConnectionManager.Connection;

        public NotSendMessageManager()
        {
            Connection.CreateTable<NotSendMessage>();
        }

        public NotSendMessage GetItem()
        {
            lock (Locker)
            {
                var item = Connection.Table<NotSendMessage>().FirstOrDefault();
                return item;
            }
        }

        public NotSendMessage GetItem(int id)
        {
            lock (Locker)
            {
                var item = Connection.Table<NotSendMessage>().Where(w => w.Id == id).FirstOrDefault();
                return item;
            }
        }

        public IEnumerable<NotSendMessage> GetItems(int roomId)
        {
            lock (Locker)
            {
                return Connection.Table<NotSendMessage>().Where(w => w.RoomId == roomId).OrderBy(o => o.Id);
            }
        }

        // アイテムを保存する
        public int SaveItem(NotSendMessage item)
        {
            lock (Locker)
            {
                var id = Connection.Insert(item);
                var lastItem = Connection.Table<NotSendMessage>().OrderByDescending(o => o.Id).FirstOrDefault();
                if (lastItem != null)
                    id = lastItem.Id;
                return id;
            }
        }

        // 指定したIDのアイテムを削除する
        public void DeleteItem(int id)
        {
            lock (Locker)
            {
                if (id != 0)
                {
                    Connection.Delete<NotSendMessage>(id);
                }
            }
        }
    }
}

