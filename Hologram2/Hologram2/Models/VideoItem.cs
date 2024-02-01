using System;
using SQLite;

namespace Hologram2.Models
{
    public class VideoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string VideoPath { get; set; }
        // Add other properties if needed, like Title, Duration, etc.
    }

}

