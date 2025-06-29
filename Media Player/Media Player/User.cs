using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MusicPlayer
{

    public class User
    {
        public string Name { get; set; }
        public ObservableCollection<Playlist> UserPlaylists { get; set; }
        public UserSettings Settings { get; set; }
        public bool IsActive { get; set; }

        public User()
        {
            Name = "Nowy Użytkownik";
            UserPlaylists = new ObservableCollection<Playlist>();
            Settings = new UserSettings();
            IsActive = false;
        }

        public User(string name)
        {
            Name = name;
            UserPlaylists = new ObservableCollection<Playlist>();
            Settings = new UserSettings();
            IsActive = false;
        }
        public bool AddPlaylist(Playlist playlist)
        {
            if (playlist == null) return false;

            // Sprawdź czy playlista już istnieje
            if (UserPlaylists.Any(p => p.Name.Equals(playlist.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            UserPlaylists.Add(playlist);
            return true;
        }

        public bool RemovePlaylist(Playlist playlist)
        {
            if (playlist == null) return false;

            if (playlist.IsSystemPlaylist) return false;

            return UserPlaylists.Remove(playlist);
        }


        public int PlaylistCount => UserPlaylists.Count;

        public bool HasPlaylist(string playlistName)
        {
            return UserPlaylists.Any(p => p.Name.Equals(playlistName, StringComparison.OrdinalIgnoreCase));
        }


        public Playlist FindPlaylistByName(string playlistName)
        {
            return UserPlaylists.FirstOrDefault(p =>
                p.Name.Equals(playlistName, StringComparison.OrdinalIgnoreCase));
        }


        public void CreateDefaultPlaylist()
        {
            if (!HasPlaylist("Wszystkie utwory"))
            {
                var defaultPlaylist = new Playlist("Wszystkie utwory")
                {
                    IsSystemPlaylist = true
                };
                AddPlaylist(defaultPlaylist);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}