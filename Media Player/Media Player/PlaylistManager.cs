using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MusicPlayer
{
    /// <summary>
    /// Klasa odpowiedzialna za zarządzanie playlistami i utworami
    /// Zapisuje i wczytuje playlisty z pliku JSON
    /// </summary>
    public class PlaylistManager
    {
        private readonly string _playlistsFilePath;

        /// <summary>
        /// Konstruktor - ustawia ścieżkę do pliku z playlistami
        /// </summary>
        public PlaylistManager()
        {
            // Ścieżka do pliku w folderze Dokumenty
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string musicPlayerFolder = Path.Combine(documentsFolder, "MusicPlayer");

            // Upewnij się, że folder istnieje
            if (!Directory.Exists(musicPlayerFolder))
            {
                Directory.CreateDirectory(musicPlayerFolder);
            }

            _playlistsFilePath = Path.Combine(musicPlayerFolder, "playlists.json");
        }

        /// <summary>
        /// Zapisuje wszystkie playlisty do pliku JSON
        /// </summary>
        /// <param name="playlists">Kolekcja playlist do zapisania</param>
        public void SavePlaylists(ObservableCollection<Playlist> playlists)
        {
            try
            {
                // Tworzymy strukturę danych do serializacji
                var playlistsToSave = playlists.Select(playlist => new
                {
                    Name = playlist.Name,
                    CreatedDate = playlist.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    Songs = playlist.Songs.Select(song => new
                    {
                        Name = song.Name,
                        Author = song.Author,
                        Duration = song.Duration.ToString(),
                        Path = song.Path
                    }).ToList()
                }).ToList();

                string jsonString = JsonConvert.SerializeObject(playlistsToSave, Formatting.Indented);

                // Zapisz do pliku
                File.WriteAllText(_playlistsFilePath, jsonString);

                Console.WriteLine($"Zapisano {playlists.Count} playlist do: {_playlistsFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania playlist: {ex.Message}",
                               "Błąd zapisu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Wczytuje wszystkie playlisty z pliku JSON
        /// </summary>
        /// <returns>Kolekcja wczytanych playlist</returns>
        public ObservableCollection<Playlist> LoadPlaylists()
        {
            var playlists = new ObservableCollection<Playlist>();

            try
            {
                // Sprawdź czy plik istnieje
                if (!File.Exists(_playlistsFilePath))
                {
                    Console.WriteLine("Plik playlists.json nie istnieje - tworzę domyślną playlistę.");

                    // Utwórz domyślną playlistę "Wszystkie utwory"
                    var defaultPlaylist = new Playlist("Wszystkie utwory");
                    playlists.Add(defaultPlaylist);

                    return playlists;
                }

                // Wczytaj JSON
                string jsonString = File.ReadAllText(_playlistsFilePath);
                var loadedPlaylists = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);

                int loadedPlaylistsCount = 0;
                int errorCount = 0;

                foreach (var playlistData in loadedPlaylists)
                {
                    try
                    {
                        // Wczytaj dane playlisty
                        string name = playlistData.Name?.ToString() ?? "Nienazwana playlista";

                        DateTime createdDate = DateTime.Now;
                        if (playlistData.CreatedDate != null)
                        {
                            DateTime.TryParse(playlistData.CreatedDate.ToString(), out createdDate);
                        }

                        Playlist playlist = new Playlist(name)
                        {
                            CreatedAt = createdDate
                        };

                        // Wczytaj utwory w playliście
                        if (playlistData.Songs != null)
                        {
                            int songsLoaded = 0;
                            int songsError = 0;

                            foreach (var songData in playlistData.Songs)
                            {
                                try
                                {
                                    string songName = songData.Name?.ToString() ?? "";
                                    string author = songData.Author?.ToString() ?? "";
                                    string durationString = songData.Duration?.ToString() ?? "";
                                    string path = songData.Path?.ToString() ?? "";

                                    TimeSpan duration = TimeSpan.Parse(durationString);

                                    // Sprawdź czy plik nadal istnieje
                                    if (File.Exists(path))
                                    {
                                        Song song = new Song(songName, author, duration, path);
                                        playlist.AddSong(song);
                                        songsLoaded++;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Plik nie istnieje: {path}");
                                        songsError++;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Błąd podczas wczytywania utworu: {ex.Message}");
                                    songsError++;
                                }
                            }

                            Console.WriteLine($"Playlista '{name}': wczytano {songsLoaded} utworów, błędów: {songsError}");
                        }

                        playlists.Add(playlist);
                        loadedPlaylistsCount++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd podczas wczytywania playlisty: {ex.Message}");
                        errorCount++;
                    }
                }

                // Pokaż wynik
                if (loadedPlaylistsCount > 0 || errorCount > 0)
                {
                    string message = $"Wczytano {loadedPlaylistsCount} playlist.";
                    if (errorCount > 0)
                    {
                        message += $"\nBłędów playlist: {errorCount}";
                    }

                    MessageBox.Show(message, "Wczytywanie zakończone",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Jeśli nie ma żadnych playlist, utwórz domyślną
                if (playlists.Count == 0)
                {
                    var defaultPlaylist = new Playlist("Wszystkie utwory");
                    playlists.Add(defaultPlaylist);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania playlist: {ex.Message}",
                               "Błąd wczytywania", MessageBoxButton.OK, MessageBoxImage.Error);

                // W przypadku błędu, utwórz domyślną playlistę
                var defaultPlaylist = new Playlist("Wszystkie utwory");
                playlists.Add(defaultPlaylist);
            }

            return playlists;
        }

        /// <summary>
        /// Znajduje playlistę o podanej nazwie
        /// </summary>
        /// <param name="playlists">Kolekcja playlist</param>
        /// <param name="name">Nazwa playlisty</param>
        /// <returns>Znaleziona playlista lub null</returns>
        public Playlist FindPlaylistByName(ObservableCollection<Playlist> playlists, string name)
        {
            return playlists.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Sprawdza czy nazwa playlisty już istnieje
        /// </summary>
        /// <param name="playlists">Kolekcja playlist</param>
        /// <param name="name">Nazwa do sprawdzenia</param>
        /// <returns>True jeśli nazwa już istnieje</returns>
        public bool PlaylistNameExists(ObservableCollection<Playlist> playlists, string name)
        {
            return playlists.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }


        /// <summary>
        /// Waliduje nazwę playlisty pod kątem poprawności i unikalności
        /// </summary>
        /// <param name="name">Nazwa playlisty do zwalidowania</param>
        /// <param name="allPlaylists">Kolekcja wszystkich istniejących playlist</param>
        /// <returns>Krotka zawierająca informację o poprawności nazwy oraz komunikat błędu w przypadku niepowodzenia</returns>
        public (bool isValid, string errorMessage) ValidatePlaylistName(string name, ObservableCollection<Playlist> allPlaylists)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return (false, "Wartość nie może być pusta");
            }
            if (name.Length <= 0 || name.Length > 20)
            {
                return (false, "Wartość nie może być pusta lub nie może przekraczać 20 znaków");
            }
            if (PlaylistNameExists(allPlaylists, name))
            {
                return (false, $"Playlista o nazwie '{name}' już istnieje");
            }
            return (true, string.Empty);
        }


        /// <summary>
        /// Tworzy nową playlistę i dodaje ją do kolekcji
        /// </summary>
        /// <param name="name">Nazwa nowej playlisty</param>
        /// <param name="allPlaylists">Kolekcja wszystkich playlist</param>
        /// <returns>Utworzona playlista</returns>
        public Playlist CreateNewPlaylist(string name, ObservableCollection<Playlist> allPlaylists)
        {
            Playlist newPlaylist = new Playlist(name);
            allPlaylists.Add(newPlaylist);

            Console.WriteLine($"Utworzono nową playlistę: {name}");
            return newPlaylist;
        }

        /// <summary>
        /// Bezpiecznie usuwa playlistę z odpowiednimi sprawdzeniami
        /// </summary>
        /// <param name="playlist">Playlista do usunięcia</param>
        /// <param name="allPlaylists">Kolekcja wszystkich playlist</param>
        /// <returns>Informacje o rezultacie operacji</returns>
        public (bool wasDeleted, bool wasCurrentPlaylist, string message) DeletePlaylistSafely(
            Playlist playlist,
            Playlist currentPlaylist,
            ObservableCollection<Playlist> allPlaylists)
        {
            if (playlist == null)
            {
                return (false, false, "Nie wybrano playlisty do usunięcia");
            }

            if (playlist.IsSystemPlaylist)
            {
                return (false, false, "Nie można usunąć systemowej playlisty!");
            }


            bool wasCurrentPlaylist = (currentPlaylist == playlist);

            bool removed = allPlaylists.Remove(playlist);

            if (removed)
            {
                Console.WriteLine($"Usunięto playlistę: {playlist.Name}");
                return (true, wasCurrentPlaylist, $"Usunięto playlistę: {playlist.Name}");
            }
            else
            {
                return (false, false, "Nie udało się usunąć playlisty");
            }
        }
    }
}