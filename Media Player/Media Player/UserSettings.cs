using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicPlayer
{
    public class UserSettings
    {
        private int _volume;
        public int Volume
        {
            get => _volume;
            set => _volume = Math.Max(0, Math.Min(100, value)); 
        }
        public string SelectedPlaylistName { get; set; }
        public string CurrentSongPath { get; set; }
        public TimeSpan CurrentPosition { get; set; }
        public DateTime LastSaved { get; set; }

        public UserSettings()
        {
            Volume = 50; // Domyślna głośność
            SelectedPlaylistName = string.Empty;
            CurrentSongPath = string.Empty;
            CurrentPosition = TimeSpan.Zero;
            LastSaved = DateTime.Now;
        }

        public UserSettings(int volume, string selectedPlaylistName, string currentSongPath, TimeSpan currentPosition)
        {
            Volume = volume;
            SelectedPlaylistName = selectedPlaylistName;
            CurrentSongPath = currentSongPath;
            CurrentPosition = currentPosition;
            LastSaved = DateTime.Now;
        }

    }
   
}
