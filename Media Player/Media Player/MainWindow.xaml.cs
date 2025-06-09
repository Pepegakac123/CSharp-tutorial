using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using TagLib;


namespace MusicPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Song> allSongs;
        public MainWindow()
        {
            InitializeComponent();
            InitializePlayer();
        }

        // Inicjalizacja Odtwarzacza
        private void InitializePlayer()
        {
            allSongs = new ObservableCollection<Song>();
            SongsList.ItemsSource = allSongs;
            AddSongsButton.Click += AddSongsButton_Click;

            // ✅ Dodaj event handler dla zamykania okna
            this.Closing += MainWindow_Closing;

            UpdateSongsCount();

            // ✅ Automatycznie wczytaj utwory przy starcie
            LoadSongsFromJson();
        }

        /// <summary>
        /// Zapisuje listę utworów do pliku JSON
        /// </summary>
        /// <summary>
        /// Zapisuje listę utworów do pliku JSON
        /// </summary>
        private void SaveSongsToJson()
        {
            try
            {
                // Tworzymy listę prostych obiektów do serializacji
                var songsToSave = allSongs.Select(song => new
                {
                    Name = song.Name,
                    Author = song.Author,
                    Duration = song.Duration.ToString(), // TimeSpan jako string
                    Path = song.Path
                }).ToList();

                // Ścieżka do pliku w folderze dokumentów
                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsFolder, "MusicPlayer", "songs.json");

                // Utwórz folder jeśli nie istnieje
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filePath));

                // Serializuj do JSON z ładnym formatowaniem (Newtonsoft.Json)
                string jsonString = JsonConvert.SerializeObject(songsToSave, Formatting.Indented);

                // Zapisz do pliku
                System.IO.File.WriteAllText(filePath, jsonString);

                Console.WriteLine($"Zapisano {allSongs.Count} utworów do: {filePath}");
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
        private void LoadSongsFromJson()
        {
            try
            {
                // Ścieżka do pliku
                string filePath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "MusicPlayer",
                    "songs.json"
                );

                // Sprawdź czy plik istnieje
                if (!System.IO.File.Exists(filePath))
                {
                    Console.WriteLine("Plik songs.json nie istnieje - to pierwsza uruchomienie.");
                    return;
                }

                // Wczytaj JSON
                string jsonString = System.IO.File.ReadAllText(filePath);

                // Deserializuj do listy obiektów dynamicznych (Newtonsoft.Json)
                var loadedSongs = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);

                int loadedCount = 0;
                int errorCount = 0;

                foreach (var songData in loadedSongs)
                {
                    try
                    {
                        // Pobierz dane z JSON (Newtonsoft.Json używa dynamic)
                        string name = songData.Name?.ToString() ?? "";
                        string author = songData.Author?.ToString() ?? "";
                        string durationString = songData.Duration?.ToString() ?? "";
                        string path = songData.Path?.ToString() ?? "";

                        // Konwertuj duration z string na TimeSpan
                        TimeSpan duration = TimeSpan.Parse(durationString);

                        // Sprawdź czy plik nadal istnieje
                        if (System.IO.File.Exists(path))
                        {
                            // Sprawdź czy już nie ma tego utworu
                            bool alreadyExists = allSongs.Any(s => s.Path == path);

                            if (!alreadyExists)
                            {
                                Song song = new Song(name, author, duration, path);
                                allSongs.Add(song);
                                loadedCount++;
                            }
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

                UpdateSongsCount();

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
        }

        /// <summary>
        /// Automatyczne zapisywanie przy zamykaniu aplikacji
        /// </summary>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSongsToJson(); // Automatycznie zapisz przy zamykaniu
        }

        // Dodawanie Piosenek
        private void AddSongsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title="Wybierz pliki MP3",
                Filter="PLIKI MP3 (*.mp3)|*.mp3",
                Multiselect=true,
            };

            if(dialog.ShowDialog() == true)
            {
                int addedCount = 0;
                int errorCount = 0;
                foreach (string filePath in dialog.FileNames)
                {


                    try
                    {
                        bool alreadyExists = false;
                        foreach (Song existingSong in allSongs)
                        {
                            if (existingSong.Path == filePath)
                            {
                                alreadyExists = true;
                                break;
                            }
                        }

                        if (!alreadyExists)
                        {
                            Song newSong = CreateSongFromFile(filePath);

                            allSongs.Add(newSong);
                            addedCount++;
                        }

                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        MessageBox.Show($"Błąd podczas dodawania pliku {Path.GetFileName(filePath)}: {ex.Message}",
                                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
  
                    
                }
                UpdateSongsCount();


                string message = $"Dodano {addedCount} nowych utworów.";
                if (errorCount > 0)
                {
                    message += $"\nBłędów: {errorCount}";
                }

                MessageBox.Show(message, "Rezultat", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        /// Tworzy obiekt Song na podstawie sciezki do pliku
        private Song CreateSongFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("Plik nie istnieje");

             }
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string title = fileName;
            string author = "Nieznany Wykonawaca";
            TimeSpan duration = TimeSpan.Zero;

            try
            {
                using(var file = TagLib.File.Create(filePath))
                {
                    if (!string.IsNullOrEmpty(file.Tag.Title))
                    {
                        title = file.Tag.Title;
                    }

                    if (file.Tag.Performers != null && file.Tag.Performers.Length > 0)
                    {
                        author = file.Tag.Performers[0]; // Pierwszy wykonawca
                    }
                    else if (!string.IsNullOrEmpty(file.Tag.FirstPerformer))
                    {
                        author = file.Tag.FirstPerformer;
                    }

                    if (file.Properties != null)
                    {
                        duration = file.Properties.Duration;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Song song = new Song(title, author, duration, filePath);

            return song;
            
        }

        private void UpdateSongsCount()
        {
            SongsCountLabel.Text = $"Liczba utworów: {allSongs.Count}";
        }
    }

    ///<summary>
    /// Klasa bazowa dla danych piosenki
    ///</summary>

    public abstract class SongData
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; }

        protected SongData() {
            Name = string.Empty;
            Author = string.Empty;
            Duration = TimeSpan.Zero;
            IsActive = false;
        }

        public abstract void Play();
        public abstract void Stop();
    } 

    public class Song : SongData
    {
        public string Path { get; set; }
        public Song() : base() { 
            Path = string.Empty;
        }

        public Song(string name, string author, TimeSpan duration, string path) : base()
        {
            Name = name;
            Author = author;
            Duration = duration;
            Path = path;
        }

        public override void Play()
        {
            IsActive = true;
            Console.WriteLine($"Odtwarzam: {Author} - {Name}");
        }
        public override void Stop() {
            IsActive = false;
            Console.WriteLine($"Zatrzymuję: {Author} - {Name}");
        }

        public override string ToString()
        {
            return $"{Author} - {Name} ({Duration.Minutes:D2}:{Duration.Seconds:D2})";
        }
    }

}
