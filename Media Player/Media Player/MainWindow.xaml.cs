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
using Newtonsoft.Json.Bson;

namespace MusicPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // GŁÓWNE ZMIENNE - struktura gotowa na rozbudowę
        private ObservableCollection<Playlist> allPlaylists;  // Wszystkie playlisty (gotowe na więcej)
        private Playlist currentPlaylist;                     // Aktualnie wybrana playlista
        private ObservableCollection<Song> displayedSongs;    // Utwory wyświetlane w SongsList

        // Pomocnicze klasy
        private PlaylistManager songManager;
        private SongManager songController;

        public MainWindow()
        {
            InitializeComponent();
            InitializePlayer();
        }

        /// <summary>
        /// Inicjalizacja odtwarzacza - gotowa na rozbudowę
        /// </summary>
        private void InitializePlayer()
        {
            // Inicjalizacja pomocniczych klas
            songManager = new PlaylistManager();
            songController = new SongManager();

            // Inicjalizacja kolekcji - struktura dla wielu playlist
            allPlaylists = new ObservableCollection<Playlist>();
            displayedSongs = new ObservableCollection<Song>();
            SongsList.ItemsSource = displayedSongs;

            // Event handlery - podstawowe (gotowe na dodanie więcej)
            SetupEventHandlers();

            // Załaduj dane z pliku
            LoadPlaylistsFromFile();

            // Upewnij się że istnieje domyślna playlista
            EnsureDefaultPlaylistExists();

            // Ustaw interfejs
            SetupInitialInterface();

            UpdateSongsCount();
        }

        /// <summary>
        /// Konfiguruje event handlery - gotowe na dodanie więcej
        /// </summary>
        private void SetupEventHandlers()
        {
            AddSongsButton.Click += AddSongsButton_Click;
            PlaylistsList.SelectionChanged += PlaylistsList_SelectionChanged;
            this.Closing += MainWindow_Closing;
            CreatePlaylistButton.Click += CreatePlaylistButton_Click;
            DeletePlaylistButton.Click += DeletePlaylistButton_Click;
            CancelPlaylistButton.Click += CancelPlaylistButton_Click;
            OkPlaylistButton.Click += OkPlaylistButton_Click;
        }

        /// <summary>
        /// Zapewnia istnienie domyślnej playlisty "Wszystkie utwory"
        /// </summary>
        private void EnsureDefaultPlaylistExists()
        {
            // Szukaj domyślnej playlisty
            var defaultPlaylist = FindPlaylistByName("Wszystkie utwory");

            // Jeśli nie istnieje, utwórz ją
            if (defaultPlaylist == null)
            {
                defaultPlaylist = new Playlist("Wszystkie utwory");
                allPlaylists.Add(defaultPlaylist);
                Console.WriteLine("Utworzono domyślną playlistę 'Wszystkie utwory'");
            }
            else
            {
                Console.WriteLine($"Domyślna playlista istnieje z {defaultPlaylist.SongCount} utworami");
            }

            // Ustaw jako aktualną
            defaultPlaylist.IsSystemPlaylist = true;
            currentPlaylist = defaultPlaylist;
        }

        /// <summary>
        /// Ustawia początkowy interfejs
        /// </summary>
        private void SetupInitialInterface()
        {
            // Ustaw źródło danych dla listy playlist
            PlaylistsList.ItemsSource = allPlaylists;

            // Znajdź i wybierz domyślną playlistę
            var defaultPlaylist = FindPlaylistByName("Wszystkie utwory");
            if (defaultPlaylist != null)
            {
                PlaylistsList.SelectedItem = defaultPlaylist;
            }

            // Pokaż utwory z aktualnej playlisty
            RefreshDisplayedSongs();
        }

        /// <summary>
        /// GOTOWE NA ROZBUDOWĘ: Obsługuje zmianę wybranej playlisty
        /// </summary>
        private void PlaylistsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlaylistsList.SelectedItem is Playlist selectedPlaylist)
            {
                currentPlaylist = selectedPlaylist;
                RefreshDisplayedSongs();

                Console.WriteLine($"Zmieniono playlistę na: {selectedPlaylist.Name} ({selectedPlaylist.SongCount} utworów)");
            }
        }

        /// <summary>
        /// Odświeża listę wyświetlanych utworów na podstawie aktualnej playlisty
        /// </summary>
        private void RefreshDisplayedSongs()
        {
            displayedSongs.Clear();

            if (currentPlaylist != null)
            {
                foreach (var song in currentPlaylist.Songs)
                {
                    displayedSongs.Add(song);
                }
            }

            UpdateSongsCount();
        }

        /// <summary>
        /// Wczytuje playlisty z pliku
        /// </summary>
        private void LoadPlaylistsFromFile()
        {
            var loadedPlaylists = songManager.LoadPlaylists();

            allPlaylists.Clear();
            foreach (var playlist in loadedPlaylists)
            {
                allPlaylists.Add(playlist);
            }

            Console.WriteLine($"Wczytano {allPlaylists.Count} playlist");
        }

        /// <summary>
        /// Zapisuje playlisty przy zamykaniu aplikacji
        /// </summary>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            songManager.SavePlaylists(allPlaylists);
        }

        /// <summary>
        /// Dodaje utwory do aktualnie wybranej playlisty
        /// </summary>
        private void AddSongsButton_Click(object sender, RoutedEventArgs e)
        {
            //Globalna Plylists
            var systemPlaylist = FindPlaylistByName("Wszystkie utwory");


            // Sprawdź czy mamy wybraną playlistę
            if (currentPlaylist == null)
            {
                MessageBox.Show("Brak wybranej playlisty!", "Błąd",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog()
            {
                Title = "Wybierz pliki MP3",
                Filter = "PLIKI MP3 (*.mp3)|*.mp3",
                Multiselect = true,
            };

            if (dialog.ShowDialog() != true) return;

            int addedCount = 0;
            int duplicatesCount = 0;
            int errorCount = 0;

            foreach (string filePath in dialog.FileNames)
            {
                try
                {
                    // Sprawdź duplikaty w aktualnej playliście
                    if (currentPlaylist.ContainsSong(filePath))
                    {
                        duplicatesCount++;
                        continue;
                    }

                    // Utwórz obiekt Song z pliku
                    Song newSong = songController.CreateSongFromFile(filePath);

                    // Dodaj do aktualnej playlisty
                    if (currentPlaylist.AddSong(newSong))
                    {
                        if (!currentPlaylist.IsSystemPlaylist)
                        {
                            systemPlaylist.AddSong(newSong); // Dodaj do systemowej playlisty "Wszystkie utwory"
                        }
                        addedCount++;
                    }
                }
                catch (Exception ex)
                {
                    errorCount++;
                    string fileName = songController.GetFileNameOnly(filePath);
                    Console.WriteLine($"Błąd podczas dodawania pliku {fileName}: {ex.Message}");
                }
            }

            // Aktualizuj liczbę utworów w aktualnej playliście
            UpdateSongsCount();
            // Odśwież widok
            RefreshDisplayedSongs();

            // Pokaż rezultat
            ShowAddSongsResult(addedCount, duplicatesCount, errorCount);
        }

        /// <summary>
        /// Aktualizuje licznik utworów
        /// </summary>
        private void UpdateSongsCount()
        {
            if (currentPlaylist != null)
            {
                SongsCountLabel.Text = $"Playlista: {currentPlaylist.Name} - Liczba utworów: {currentPlaylist.SongCount}";
            }
            else
            {
                SongsCountLabel.Text = "Brak wybranej playlisty - Liczba utworów: 0";
            }
        }

        // ====== METODY POMOCNICZE - GOTOWE NA ROZBUDOWĘ ======

        /// <summary>
        /// Znajduje playlistę po nazwie - przydatne przy rozbudowie
        /// </summary>
        /// <param name="name">Nazwa playlisty</param>
        /// <returns>Znaleziona playlista lub null</returns>
        private Playlist FindPlaylistByName(string name)
        {
            return allPlaylists.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Sprawdza czy nazwa playlisty już istnieje - przydatne przy dodawaniu nowych
        /// </summary>
        /// <param name="name">Nazwa do sprawdzenia</param>
        /// <returns>True jeśli nazwa już istnieje</returns>
        private bool PlaylistNameExists(string name)
        {
            return songManager.PlaylistNameExists(allPlaylists, name);
        }

        /// <summary>
        /// Pokazuje rezultat dodawania utworów - metoda pomocnicza
        /// </summary>
        private void ShowAddSongsResult(int addedCount, int duplicatesCount, int errorCount)
        {
            string message = $"Dodano {addedCount} nowych utworów do playlisty '{currentPlaylist.Name}'.";

            if (duplicatesCount > 0)
            {
                message += $"\nPominięto {duplicatesCount} duplikatów.";
            }

            if (errorCount > 0)
            {
                message += $"\nBłędów: {errorCount}";
            }

            MessageBox.Show(message, "Rezultat", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Dodaje nową playlistę
        /// </summary>
        private void CreatePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            CreatePlaylistDialog.Visibility = Visibility.Visible;

        }

        private void CancelPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            CreatePlaylistDialog.Visibility = Visibility.Collapsed;
            ClearForm();
        }

        private void OkPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            string value = PlaylistNameTextBox.Text;
            bool textNotValid = false; 
            TextBlock errorTextBlock = ErrorMessageTextBlock;
            if (string.IsNullOrWhiteSpace(value))
            {
                WriteErrorMsg("Wartość nie może być pusta", errorTextBlock);
                textNotValid = true;
            }
            string processedValue = value.Trim();

            if (processedValue.Length <= 0 || processedValue.Length > 20)
            {
                WriteErrorMsg("Wartość nie może być pusta lub nie może przekraczać 20 znaków", errorTextBlock);
                textNotValid = true;
            }
            if (PlaylistNameExists(processedValue))
            {
                WriteErrorMsg($"Playlista o nazwie {processedValue} już istnieje",errorTextBlock);
                textNotValid = true;
            }
            if (textNotValid)
            {
                return;
            }
            else
            {
                CreateNewPlaylist(processedValue);
            }

        }

        private void CreateNewPlaylist(string name)
        {
            Playlist newPlaylist = new Playlist(name);
            allPlaylists.Add(newPlaylist);
            PlaylistsList.SelectedItem = newPlaylist;
            currentPlaylist = newPlaylist;
            RefreshDisplayedSongs();
            CreatePlaylistDialog.Visibility = Visibility.Collapsed;
            ClearForm();
            MessageBox.Show($"Pomyślne utworzono playliste {newPlaylist.Name} ");
        }
            

        private void WriteErrorMsg(string msg, TextBlock textBlock)
        {
            textBlock.Visibility = Visibility.Visible;
            textBlock.Text = msg;
        }

        private void ClearForm()
        {
            PlaylistNameTextBox.Text = "";
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            ErrorMessageTextBlock.Text = "";
        }

        private void DeletePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("DeletePlaylistButton_Click wywołane");
            DeletePlaylist(currentPlaylist);
        }
        private void DeletePlaylist(Playlist playlist)
        {
            if (playlist == null) return;

            Console.WriteLine($"{playlist.IsSystemPlaylist}");
            if (playlist.IsSystemPlaylist)
            {
                MessageBox.Show("Nie można usunąć systemowej playlisty!", "Błąd",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool wasCurrentPlaylist = (currentPlaylist == playlist); 

            allPlaylists.Remove(playlist);

            if (wasCurrentPlaylist)
            {
                var defaultPlaylist = FindPlaylistByName("Wszystkie utwory");
                currentPlaylist = defaultPlaylist;
                PlaylistsList.SelectedItem = defaultPlaylist;
                RefreshDisplayedSongs();
            }

            MessageBox.Show($"Usunięto playlistę: {playlist.Name}");
        }

        // ====== METODY DO PRZYSZŁEJ ROZBUDOWY ======

        // TODO: Metody które dodamy w przyszłości:

        // private void CreateNewPlaylist(string name) { }
        // private void DeletePlaylist(Playlist playlist) { }
        // private void RenamePlaylist(Playlist playlist, string newName) { }
        // private void DuplicatePlaylist(Playlist playlist) { }
        // private void ExportPlaylist(Playlist playlist) { }
        // private void ImportPlaylist() { }
    }

    // Klasy Song i SongData pozostają bez zmian
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