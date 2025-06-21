using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using TagLib;

namespace MusicPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // GŁÓWNE ZMIENNE - struktura gotowa na rozbudowę
        private ObservableCollection<Playlist> allPlaylists;  // Wszystkie playlisty 
        private Playlist currentPlaylist;                     // Aktualnie wybrana playlista
        private ObservableCollection<Song> displayedSongs;    // Utwory wyświetlane w SongsList
        private Song currentSong;                        // Aktualnie odtwarzany utwór (jeśli istnieje)
        private MediaPlayer mediaPlayer;
        // Pomocnicze klasy/zmienne
        private PlaylistManager playlistManager;
        private SongManager songManager;
        private DispatcherTimer positionTimer;
        private bool isUserSeekingPosition = false;
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
            playlistManager = new PlaylistManager();
            songManager = new SongManager();

            // Inicjalizacja kolekcji - struktura dla wielu playlist
            allPlaylists = new ObservableCollection<Playlist>();
            displayedSongs = new ObservableCollection<Song>();
            SongsList.ItemsSource = displayedSongs;

            //Inicjalizacja MediaPlayer
            mediaPlayer = new MediaPlayer();

            //Timer
            positionTimer = new DispatcherTimer();
            positionTimer.Interval = TimeSpan.FromMilliseconds(500); // Co pół sekundy
            

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

            SongsList.MouseDoubleClick += SongsList_MouseDoubleClick;


            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            positionTimer.Tick += PositionTimer_Tick;
            ProgressSlider.ValueChanged += ProgressSlider_ValueChanged;
            ProgressSlider.PreviewMouseDown += (s, e) => isUserSeekingPosition = true;
            ProgressSlider.PreviewMouseUp += (s, e) => isUserSeekingPosition = false;

            PlayPauseButton.Click += PlayPauseButton_Click;
            PreviousButton.Click += PreviousButton_Click;
            NextButton.Click += NextButton_Click;
            VolumeSlider.ValueChanged += VolumeSlider_ValueChanged;

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
        /// Obsługuje zmianę wybranej playlisty
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
            var loadedPlaylists = playlistManager.LoadPlaylists();

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
            playlistManager.SavePlaylists(allPlaylists);
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
                    Song newSong = songManager.CreateSongFromFile(filePath);

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
                    string fileName = songManager.GetFileNameOnly(filePath);
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
            return playlistManager.PlaylistNameExists(allPlaylists, name);
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

        /// <summary>
        /// Obsługuje anulowanie tworzenia nowej playlisty - ukrywa dialog i czyści formularz
        /// </summary>
        private void CancelPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            CreatePlaylistDialog.Visibility = Visibility.Collapsed;
            ClearForm();
        }

        /// <summary>
        /// Obsługuje potwierdzenie tworzenia nowej playlisty - waliduje nazwę i tworzy playlistę
        /// </summary>
        private void OkPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            string value = PlaylistNameTextBox.Text?.Trim();
            var (isValid, errorMessage) = playlistManager.ValidatePlaylistName(value, allPlaylists);
            if (!isValid)
            {
                WriteErrorMsg(errorMessage, ErrorMessageTextBlock);
                return;
            }
            CreateNewPlaylist(value);
        }

        /// <summary>
        /// Tworzy nową playlistę i aktualizuje interfejs użytkownika
        /// </summary>
        /// <param name="name">Nazwa nowej playlisty</param>
        private void CreateNewPlaylist(string name)
        {
            Playlist newPlaylist = playlistManager.CreateNewPlaylist(name, allPlaylists);
            PlaylistsList.SelectedItem = newPlaylist;
            currentPlaylist = newPlaylist;
            RefreshDisplayedSongs();
            CreatePlaylistDialog.Visibility = Visibility.Collapsed;
            ClearForm();
            MessageBox.Show($"Pomyślnie utworzono playlistę {newPlaylist.Name}");
        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku usuwania playlisty - usuwa aktualnie wybraną playlistę
        /// </summary>
        private void DeletePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePlaylist(currentPlaylist);
        }

        /// <summary>
        /// Usuwa wskazaną playlistę z odpowiednimi sprawdzeniami i aktualizuje interfejs
        /// </summary>
        /// <param name="playlist">Playlista do usunięcia</param>
        private void DeletePlaylist(Playlist playlist)
        {
            var (wasDeleted, wasCurrentPlaylist, message) = playlistManager.DeletePlaylistSafely(
                playlist, currentPlaylist, allPlaylists);
            if (!wasDeleted)
            {
                MessageBox.Show(message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (wasCurrentPlaylist)
            {
                var defaultPlaylist = FindPlaylistByName("Wszystkie utwory");
                currentPlaylist = defaultPlaylist;
                PlaylistsList.SelectedItem = defaultPlaylist;
                RefreshDisplayedSongs();
            }
            MessageBox.Show(message, "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Wyświetla komunikat błędu w podanym elemencie TextBlock
        /// </summary>
        /// <param name="msg">Treść komunikatu błędu</param>
        /// <param name="textBlock">Element TextBlock do wyświetlenia komunikatu</param>
        private void WriteErrorMsg(string msg, TextBlock textBlock)
        {
            textBlock.Visibility = Visibility.Visible;
            textBlock.Text = msg;
        }

        /// <summary>
        /// Czyści formularz tworzenia playlisty - usuwa tekst i ukrywa komunikaty błędów
        /// </summary>
        private void ClearForm()
        {
            PlaylistNameTextBox.Text = "";
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            ErrorMessageTextBlock.Text = "";
        }

        /// <summary>
        /// Obsługuje podwójne kliknięcie na utworze w liście - odtwarza/pauzuje wybrany utwór
        /// Implementuje logikę przełączania: podwójne kliknięcie na tę samą piosenkę = pause/play,
        /// podwójne kliknięcie na inną piosenkę = zmiana utworu
        /// </summary>
        private void SongsList_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Song selectedSong = SongsList.SelectedItem as Song;
            if (selectedSong == null) return;

            if (currentSong == selectedSong && selectedSong.IsActive)
            {
                selectedSong.Stop(mediaPlayer);
                selectedSong.IsActive = false;
                positionTimer.Stop();
            }
            else
            {
                if (currentSong != null && currentSong.IsActive)
                {
                    currentSong.Stop(mediaPlayer);
                    currentSong.IsActive = false;
                    PlayPauseButton.Content = "▶";
                    positionTimer.Stop();
                }
                selectedSong.Play(mediaPlayer);
                selectedSong.IsActive = true;
                currentSong = selectedSong;
                PlayPauseButton.Content = "⏸";
                positionTimer.Start();


            }

            RefreshSongInfo(selectedSong);
        }

        /// <summary>
        /// Event handler wywoływany gdy MediaPlayer pomyślnie załaduje plik audio
        /// Konfiguruje slider postępu z rzeczywistą długością utworu i resetuje pozycję na początek
        /// </summary>
        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
              if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                var duration = mediaPlayer.NaturalDuration.TimeSpan;
                ProgressSlider.Maximum = duration.TotalSeconds;
                ProgressSlider.Minimum = 0;
                ProgressSlider.Value = 0;

                TotalTime.Text = duration.ToString(@"mm\:ss");
            }
        }
        /// <summary>
        /// Timer wywoływany co 500ms podczas odtwarzania - aktualizuje slider postępu i czas bieżący
        /// Działa tylko gdy muzyka gra i użytkownik nie manipuluje sliderem
        /// </summary>
        private void PositionTimer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer != null && currentSong != null && currentSong.IsActive && !isUserSeekingPosition)
            {
                ProgressSlider.Value = mediaPlayer.Position.TotalSeconds;
                CurrentTime.Text = mediaPlayer.Position.ToString(@"mm\:ss");
            }
        }
        /// <summary>
        /// Obsługuje zmiany wartości slidera postępu przez użytkownika (kliknięcie/przeciąganie)
        /// Przewija MediaPlayer na nową pozycję i tymczasowo blokuje timer aby uniknąć konfliktów
        /// </summary>
        private void ProgressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && mediaPlayer != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                mediaPlayer.Position = TimeSpan.FromSeconds(ProgressSlider.Value);

                
                isUserSeekingPosition = true;
                Task.Delay(1000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() => isUserSeekingPosition = false);
                });
            }
        }
        /// <summary>
        /// Obsługuje kliknięcie przycisku Play/Pause - przełącza między odtwarzaniem a pauzą aktualnego utworu
        /// Aktualizuje ikonę przycisku i kontroluje timer postępu
        /// </summary>
        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSong == null)
            {
                return;
            }
            if(currentSong.IsActive)
            {
                currentSong.Stop(mediaPlayer);
                currentSong.IsActive = false;
                PlayPauseButton.Content = "▶";
                positionTimer.Stop();

            }
            else
            {
                currentSong.Play(mediaPlayer);
                currentSong.IsActive = true;
                PlayPauseButton.Content = "⏸";
                positionTimer.Start();

            }
        }
        /// <summary>
        /// Obsługuje kliknięcie przycisku następny utwór - przechodzi do kolejnego utworu w playliście
        /// TODO: Implementacja funkcjonalności przechodzenia do następnego utworu
        /// </summary>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Obsługuje kliknięcie przycisku poprzedni utwór - przechodzi do poprzedniego utworu w playliście
        /// TODO: Implementacja funkcjonalności przechodzenia do poprzedniego utworu
        /// </summary>
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// Obsługuje zmiany slidera głośności - aktualizuje głośność MediaPlayera w czasie rzeczywistym
        /// Konwertuje wartość z zakresu 0-100 na 0.0-1.0 wymagany przez MediaPlayer
        /// </summary>
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaPlayer != null)
            {
                mediaPlayer.Volume = VolumeSlider.Value / 100.0;
            }
        }
        /// <summary>
        /// Aktualizuje sekcję informacji o aktualnym utworze (tytuł, wykonawca)
        /// Wyświetla dane o podanym utworze lub komunikat o braku aktualnego utworu
        /// </summary>
        /// <param name="song">Utwór do wyświetlenia lub null dla braku utworu</param>
        private void RefreshSongInfo(Song song)
        {
            if (song != null)
            {
                
                CurrentSongTitle.Text = song.Name;
                CurrentArtist.Text = string.IsNullOrWhiteSpace(song.Author) ? "Autor nieznany" : song.Author;
            }
            else
            {
                CurrentSongTitle.Text = "Brak aktualnie odtwarzanego utworu.";
            }
        }
    }

    
}