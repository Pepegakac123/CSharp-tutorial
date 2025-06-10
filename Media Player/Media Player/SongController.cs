using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using TagLib;

namespace MusicPlayer
{
    /// <summary>
    /// Klasa pomocnicza dla operacji na utworach
    /// </summary>
    public class SongController
    {
        /// <summary>
        /// Sprawdza czy utwór już istnieje w kolekcji na podstawie ścieżki
        /// </summary>
        /// <param name="allSongs">Kolekcja utworów</param>
        /// <param name="filePath">Ścieżka do sprawdzenia</param>
        /// <returns>True jeśli utwór już istnieje</returns>
        public bool SongAlreadyExists(ObservableCollection<Song> allSongs, string filePath)
        {
            return allSongs.Any(song => song.Path == filePath);
        }

        /// <summary>
        /// Tworzy obiekt Song na podstawie ścieżki do pliku
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku MP3</param>
        /// <returns>Nowy obiekt Song z metadanymi</returns>
        public Song CreateSongFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("Plik nie istnieje");
            }

            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string title = fileName;
            string author = "Nieznany Wykonawca";
            TimeSpan duration = TimeSpan.Zero;

            try
            {
                using (var file = TagLib.File.Create(filePath))
                {
                    // Pobierz tytuł
                    if (!string.IsNullOrEmpty(file.Tag.Title))
                    {
                        title = file.Tag.Title;
                    }

                    // Pobierz wykonawcę
                    if (file.Tag.Performers != null && file.Tag.Performers.Length > 0)
                    {
                        author = file.Tag.Performers[0]; // Pierwszy wykonawca
                    }
                    else if (!string.IsNullOrEmpty(file.Tag.FirstPerformer))
                    {
                        author = file.Tag.FirstPerformer;
                    }

                    // Pobierz długość utworu
                    if (file.Properties != null)
                    {
                        duration = file.Properties.Duration;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas czytania metadanych dla {fileName}: {ex.Message}");
                // Używamy domyślnych wartości gdy nie można odczytać metadanych
            }

            return new Song(title, author, duration, filePath);
        }

        /// <summary>
        /// Tworzy komunikat z podsumowaniem operacji dodawania plików
        /// </summary>
        /// <param name="addedCount">Liczba dodanych plików</param>
        /// <param name="errorCount">Liczba błędów</param>
        /// <returns>Sformatowany komunikat</returns>
        public string CreateResultMessage(int addedCount, int errorCount)
        {
            string message = $"Dodano {addedCount} nowych utworów.";

            if (errorCount > 0)
            {
                message += $"\nBłędów: {errorCount}";
            }

            return message;
        }

        /// <summary>
        /// Sprawdza czy plik ma prawidłowe rozszerzenie MP3
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku</param>
        /// <returns>True jeśli plik ma rozszerzenie .mp3</returns>
        public bool IsValidMp3Extension(string filePath)
        {
            return Path.GetExtension(filePath).ToLower() == ".mp3";
        }

        /// <summary>
        /// Pobiera nazwę pliku bez ścieżki i rozszerzenia
        /// </summary>
        /// <param name="filePath">Pełna ścieżka do pliku</param>
        /// <returns>Nazwa pliku</returns>
        public string GetFileNameOnly(string filePath)
        {
            return Path.GetFileName(filePath);
        }
    }
}