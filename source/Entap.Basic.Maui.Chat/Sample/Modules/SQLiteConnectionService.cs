using System;
using System.IO;
using Entap.Basic.SQLite;
using SQLite;

namespace Sample
{
	public class SQLiteConnectionService : ISQLiteConnectionService
    {
        static readonly string FileName = "Sample.db3";
        static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, FileName);

        static readonly SQLiteOpenFlags OpenFlags =
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.Create;

        static readonly string EncryptionKey = "hoge";

        static readonly TimeSpan TimeOutSpan = TimeSpan.FromSeconds(1);

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var options = new SQLiteConnectionString(DatabasePath, OpenFlags, true, key: EncryptionKey);
            return new SQLiteAsyncConnection(options);
        }
    }
}

