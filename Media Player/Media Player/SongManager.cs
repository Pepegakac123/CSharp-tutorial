using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Windows;

namespace MusicPlayer
{
    /// <summary>
    /// Klasa odpowiedzialna za zarządzanie playlistami i utworami
    /// Zapisuje i wczytuje playlisty z pliku JSON
    /// </summary>
    public class SongManager
    {
        private readonly string _playlistsFilePath;

        /// <summary>
        /// Konstruktor - ustawia ścieżkę do pliku z playlistami
        /// </summary>
        public SongManager()
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
                    CreatedDate = playlist.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
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
                            CreatedDate = createdDate
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
    }
}