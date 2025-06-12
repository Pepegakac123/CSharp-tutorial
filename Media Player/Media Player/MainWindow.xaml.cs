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
        private ObservableCollection<string> playlists;
        private SongManager songManager;
        private SongController songController;

        public MainWindow()
        {
            InitializeComponent();
            InitializePlayer();
        }

        // Inicjalizacja Odtwarzacza
        private void InitializePlayer()
        {
            
            songManager = new SongManager();
            songController = new SongController();

            allSongs = new ObservableCollection<Song>();
            SongsList.ItemsSource = allSongs;
            AddSongsButton.Click += AddSongsButton_Click;

            // Playlisty
            playlists = new ObservableCollection<string>();
            playlists.Add("AllSongs");
            PlaylistsList.ItemsSource = playlists;
            PlaylistsList.SelectedIndex = 0;

            this.Closing += MainWindow_Closing;

           UpdateSongsCount();

            // Automatycznie wczytaj utwory przy starcie używając SongManager
            LoadSongsFromFile();
        }

        /// <summary>
        /// Wczytuje utwory z pliku używając SongManager
        /// </summary>
        private void LoadSongsFromFile()
        {
            
            var loadedSongs = songManager.LoadSongs();

            allSongs.Clear();
            foreach (var song in loadedSongs)
            {
                allSongs.Add(song);
            }

            UpdateSongsCount();
        }

        /// <summary>
        /// Automatyczne zapisywanie przy zamykaniu aplikacji
        /// </summary>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            songManager.SaveSongs(allSongs);
        }

        /// <summary>
        /// Dodaje piosenki do listy
        /// </summary>
        private void AddSongsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "Wybierz pliki MP3",
                Filter = "PLIKI MP3 (*.mp3)|*.mp3",
                Multiselect = true,
            };

            if (dialog.ShowDialog() != true) return;


            int addedCount = 0;
            int errorCount = 0;

            foreach (string filePath in dialog.FileNames)
            {
                try
                {
                    if (!songController.SongAlreadyExists(allSongs, filePath))
                    {
                        Song newSong = songController.CreateSongFromFile(filePath);
                        allSongs.Add(newSong);
                        addedCount++;
                    }
                }
                catch (Exception ex)
                {
                    errorCount++;
                    string fileName = songController.GetFileNameOnly(filePath);
                    MessageBox.Show($"Błąd podczas dodawania pliku {fileName}: {ex.Message}",
                                "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            UpdateSongsCount();
            string message = songController.CreateResultMessage(addedCount, errorCount);
            MessageBox.Show(message, "Rezultat", MessageBoxButton.OK, MessageBoxImage.Information);
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

        protected SongData()
        {
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

        public Song() : base()
        {
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

        public override void Stop()
        {
            IsActive = false;
            Console.WriteLine($"Zatrzymuję: {Author} - {Name}");
        }

        public override string ToString()
        {
            return $"{Author} - {Name} ({Duration.Minutes:D2}:{Duration.Seconds:D2})";
        }
    }
}