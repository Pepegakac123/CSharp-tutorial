using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    public class Playlist
    {
        public string Name { get; set; }
        public ObservableCollection<Song> Songs { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSystemPlaylist { get; set; } = false; 

        //<summary>
        /// Konstruktor Domyślny
        //</summary>

        public Playlist()
        {
            Name = "Nowa Playlista";
            Songs = new ObservableCollection<Song>();
            CreatedAt = DateTime.Now;

        }

        /// <summary>
        /// Konstruktor z nazwą
        /// </summary>
        /// <param name="name">Nazwa playlisty</param>
       
        public Playlist(string name)
        {
            Name = name;
            Songs = new ObservableCollection<Song>();
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Dodaje utwór do playlisty (jeśli jeszcze nie istnieje)
        /// </summary>
        /// <param name="song">Utwór do dodania</param>
        /// <returns>True jeśli utwór został dodany, False jeśli już istniał</returns>
        public bool AddSong(Song song)
        {
            if (song == null) return false;

            
            if (Songs.Any(s => s.Path == song.Path))
            {
                return false; 
            }

            Songs.Add(song);
            return true;
        }

        /// <summary>
        /// Usuwa utwór z playlisty
        /// </summary>
        /// <param name="song">Utwór do usunięcia</param>
        /// <returns>True jeśli utwór został usunięty</returns>
        public bool RemoveSong(Song song)
        {
            if (song == null) return false;
            return Songs.Remove(song);
        }

        /// <summary>
        /// Usuwa utwór z playlisty na podstawie ścieżki
        /// </summary>
        /// <param name="songPath">Ścieżka do utworu</param>
        /// <returns>True jeśli utwór został usunięty</returns>
        public bool RemoveSongByPath(string songPath)
        {
            var songToRemove = Songs.FirstOrDefault(s => s.Path == songPath);
            if (songToRemove != null)
            {
                return Songs.Remove(songToRemove);
            }
            return false;
        }

        /// <summary>
        /// Zwraca liczbę utworów w playliście
        /// </summary>
        public int SongCount => Songs.Count;

        /// <summary>
        /// Zwraca całkowity czas trwania playlisty
        /// </summary>
        public TimeSpan TotalDuration
        {
            get
            {
                return new TimeSpan(Songs.Sum(song => song.Duration.Ticks));
            }
        }

        /// <summary>
        /// Sprawdza czy playlista zawiera dany utwór
        /// </summary>
        /// <param name="songPath">Ścieżka do utworu</param>
        /// <returns>True jeśli playlista zawiera utwór</returns>
        public bool ContainsSong(string songPath)
        {
            return Songs.Any(s => s.Path == songPath);
        }

        /// <summary>
        /// Wyczyść playlistę
        /// </summary>
        public void Clear()
        {
            Songs.Clear();
        }

        public override string ToString()
        {
            return Name;
        }



    }
}
