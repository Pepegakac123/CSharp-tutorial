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
    /// Klasa odpowiedzialna za zarządzanie listą utworów
    /// Zapisuje i wczytuje utwory z pliku JSON
    /// </summary>
    public class SongManager
    {
        private readonly string _songsFilePath;

        /// <summary>
        /// Konstruktor - ustawia ścieżkę do pliku z utworami
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

            _songsFilePath = Path.Combine(musicPlayerFolder, "songs.json");
        }

        /// <summary>
        /// Zapisuje listę utworów do pliku JSON
        /// </summary>
        /// <param name="songs">Kolekcja utworów do zapisania</param>
        public void SaveSongs(ObservableCollection<Song> songs)
        {
            try
            {
                // Tworzymy listę prostych obiektów do serializacji
                var songsToSave = songs.Select(song => new
                {
                    Name = song.Name,
                    Author = song.Author,
                    Duration = song.Duration.ToString(),
                    Path = song.Path
                }).ToList();


                string jsonString = JsonConvert.SerializeObject(songsToSave, Formatting.Indented);

                // Zapisz do pliku
                File.WriteAllText(_songsFilePath, jsonString);

                Console.WriteLine($"Zapisano {songs.Count} utworów do: {_songsFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania: {ex.Message}",
                               "Błąd zapisu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Wczytuje listę utworów z pliku JSON
        /// </summary>
        /// <returns>Kolekcja wczytanych utworów</returns>
        public ObservableCollection<Song> LoadSongs()
        {
            var songs = new ObservableCollection<Song>();

            try
            {
                // Sprawdź czy plik istnieje
                if (!File.Exists(_songsFilePath))
                {
                    Console.WriteLine("Plik songs.json nie istnieje - to pierwsza uruchomienie.");
                    return songs; // Zwróć pustą kolekcję
                }

                // Wczytaj JSON
                string jsonString = File.ReadAllText(_songsFilePath);


                var loadedSongs = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);

                int loadedCount = 0;
                int errorCount = 0;

                foreach (var songData in loadedSongs)
                {
                    try
                    {

                        string name = songData.Name?.ToString() ?? "";
                        string author = songData.Author?.ToString() ?? "";
                        string durationString = songData.Duration?.ToString() ?? "";
                        string path = songData.Path?.ToString() ?? "";

                        TimeSpan duration = TimeSpan.Parse(durationString);

                        // Sprawdź czy plik nadal istnieje
                        if (File.Exists(path))
                        {
                            Song song = new Song(name, author, duration, path);
                            songs.Add(song);
                            loadedCount++;
                        }
                        else
                        {
                            Console.WriteLine($"Plik nie istnieje: {path}");
                            errorCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd podczas wczytywania utworu: {ex.Message}");
                        errorCount++;
                    }
                }

                // Pokaż wynik tylko jeśli coś się załadowało lub były błędy
                if (loadedCount > 0 || errorCount > 0)
                {
                    string message = $"Wczytano {loadedCount} utworów.";
                    if (errorCount > 0)
                    {
                        message += $"\nNie udało się wczytać: {errorCount}";
                    }

                    MessageBox.Show(message, "Wczytywanie zakończone",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania: {ex.Message}",
                               "Błąd wczytywania", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return songs;
        }
    }
}