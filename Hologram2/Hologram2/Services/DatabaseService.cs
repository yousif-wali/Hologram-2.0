using System;
using Hologram2.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace Hologram2.Services
{
    public static class DatabaseService
    {
        private static readonly SQLiteAsyncConnection _database;

        static DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "videos.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<VideoItem>().Wait();
        }

        public static Task<int> SaveVideoItemAsync(VideoItem item)
        {
            return _database.UpdateAsync(item);
            /*
            if (item.Id != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
            */
        }

        public static Task<List<VideoItem>> GetVideoItemsAsync()
        {
            return _database.Table<VideoItem>().ToListAsync();
        }
     
    }

}

