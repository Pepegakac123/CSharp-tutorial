using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MusicPlayer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // GŁÓWNE ZMIENNE
        private ObservableCollection<Playlist> allPlaylists;  // Wszystkie playlisty 
        private Playlist currentPlaylist;                     // Aktualnie wybrana playlista
        private ObservableCollection<Song> displayedSongs;    // Utwory wyświetlane w SongsList
        private Song currentSong;                        // Aktualnie odtwarzany utwór
        private UserSettings userSettings;
        private MediaPlayer mediaPlayer;

        // ZMIENNE DLA UŻYTKOWNIKÓW
        private ObservableCollection<User> allUsers;           // Wszyscy użytkownicy
        private User currentUser;                              // Aktualny użytkownik
        private UserManager userManager;                       // Manager użytkowników

        // Pomocnicze klasy/zmienne
        private SettingsManager settingsManager;
        private PlaylistManager playlistManager;
        private SongManager songManager;
        private DispatcherTimer positionTimer;
        private TimeSpan? positionToRestore = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializePlayer();
        }

        /// <summary>
        /// Inicjalizacja odtwarzacza 
        /// </summary>
        private void InitializePlayer()
        {
            // Inicjalizacja pomocniczych klas
            playlistManager = new PlaylistManager();
            songManager = new SongManager();
            settingsManager = new SettingsManager();
            userManager = new UserManager();

            // Inicjalizacja kolekcji - struktura dla wielu playlist
            allPlaylists = new ObservableCollection<Playlist>();
            displayedSongs = new ObservableCollection<Song>();
            SongsList.ItemsSource = displayedSongs;

            // Inicjalizacja kolekcji użytkowników
            allUsers = new ObservableCollection<User>();
            UsersList.ItemsSource = allUsers;

            mediaPlayer = new MediaPlayer();

            //Timer
            positionTimer = new DispatcherTimer();
            positionTimer.Interval = TimeSpan.FromMilliseconds(500);

            // Event handlery - podstawowe
            SetupEventHandlers();

            // Załaduj użytkowników z pliku
            LoadUsersFromFile();

            EnsureDefaultUserExists();

            // Załaduj dane z pliku
            LoadPlaylistsFromFile();

            // Upewnij się że istnieje domyślna playlista
            EnsureDefaultPlaylistExists();

            // Ustaw interfejs
            SetupInitialInterface();

            UpdateSongsCount();
            // Załaduj Ustawienia z pliku
            LoadSettingsFromFile();
        }

        /// <summary>
        /// Konfiguruje event handlery
        /// </summary>
        private void SetupEventHandlers()
        {
            AddSongsButton.Click += AddSongsButton_Click;
            PlaylistsList.SelectionChanged += PlaylistsList_SelectionChanged;
            this.Closing += MainWindow_Closing;

            SongsList.MouseDoubleClick += SongsList_MouseDoubleClick;

            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            positionTimer.Tick += PositionTimer_Tick;
            ProgressSlider.ValueChanged += ProgressSlider_ValueChanged;

            PlayPauseButton.Click += PlayPauseButton_Click;
            PreviousButton.Click += PreviousButton_Click;
            NextButton.Click += NextButton_Click;
            VolumeSlider.ValueChanged += VolumeSlider_ValueChanged;

            CreatePlaylistButton.Click += CreatePlaylistButton_Click;
            DeletePlaylistButton.Click += DeletePlaylistButton_Click;
            CancelPlaylistButton.Click += CancelPlaylistButton_Click;
            OkPlaylistButton.Click += OkPlaylistButton_Click;

            // Event handlery dla użytkowników
            CreateUserButton.Click += CreateUserButton_Click;
            CancelUserButton.Click += CancelUserButton_Click;
            OkUserButton.Click += OkUserButton_Click;
            UsersList.SelectionChanged += UsersList_SelectionChanged;
        }

        /// <summary>
        /// Wczytuje użytkowników z pliku
        /// </summary>
        private void LoadUsersFromFile()
        {
            var loadedUsers = userManager.LoadUsers();

            allUsers.Clear();
            foreach (var user in loadedUsers)
            {
                allUsers.Add(user);
            }

            Console.WriteLine($"Wczytano {allUsers.Count} użytkowników");
        }

        /// <summary>
        /// Zapewnia istnienie domyślnego użytkownika
        /// </summary>
        private void EnsureDefaultUserExists()
        {
            if (allUsers.Count == 0)
            {
                var defaultUser = new User("Domyślny Użytkownik");
                defaultUser.CreateDefaultPlaylist();
                defaultUser.IsActive = true;
                allUsers.Add(defaultUser);
                Console.WriteLine("Utworzono domyślnego użytkownika");
            }

            if (currentUser == null)
            {
                currentUser = allUsers.FirstOrDefault();
                if (currentUser != null)
                {
                    currentUser.IsActive = true;
                    UsersList.SelectedItem = currentUser;
                }
            }
        }

        /// <summary>
        /// Zapewnia istnienie domyślnej playlisty "Wszystkie utwory"
        /// </summary>
        private void EnsureDefaultPlaylistExists()
        {
            if (currentUser == null) return;

            // Szukaj domyślnej playlisty w aktualnym użytkowniku
            var defaultPlaylist = currentUser.FindPlaylistByName("Wszystkie utwory");

            // Jeśli nie istnieje, utwórz ją
            if (defaultPlaylist == null)
            {
                defaultPlaylist = new Playlist("Wszystkie utwory");
                defaultPlaylist.IsSystemPlaylist = true;
                currentUser.AddPlaylist(defaultPlaylist);
                Console.WriteLine("Utworzono domyślną playlistę 'Wszystkie utwory'");
            }
            else
            {
                Console.WriteLine($"Domyślna playlista istnieje z {defaultPlaylist.SongCount} utworami");
            }

            // Ustaw jako aktualną
            currentPlaylist = defaultPlaylist;
        }

        /// <summary>
        /// Ustawia początkowy interfejs
        /// </summary>
        private void SetupInitialInterface()
        {
            if (currentUser != null)
            {
                // Ustaw źródło danych dla listy playlist
                allPlaylists.Clear();
                foreach (var playlist in currentUser.UserPlaylists)
                {
                    allPlaylists.Add(playlist);
                }
                PlaylistsList.ItemsSource = allPlaylists;

             
                var defaultPlaylist = currentUser.FindPlaylistByName("Wszystkie utwory");
                if (defaultPlaylist != null)
                {
                    PlaylistsList.SelectedItem = defaultPlaylist;
                    currentPlaylist = defaultPlaylist;
                }

                RefreshDisplayedSongs();
                CurrentUserLabel.Text = $"Użytkownik: {currentUser.Name}";
            }
        }

        /// <summary>
        /// Obsługuje zmianę wybranego użytkownika
        /// </summary>
        private void UsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersList.SelectedItem is User selectedUser)
            {
                SwitchToUser(selectedUser);
            }
        }

        /// <summary>
        /// Przełącza na innego użytkownika
        /// </summary>
        /// <param name="user">Użytkownik do przełączenia</param>
        private void SwitchToUser(User user)
        {
            if (user == null) return;

            
            if (currentUser != null)
            {
                SaveCurrentUserState();
                currentUser.IsActive = false;
            }

            
            currentUser = user;
            currentUser.IsActive = true;

            
            allPlaylists.Clear();
            foreach (var playlist in currentUser.UserPlaylists)
            {
                allPlaylists.Add(playlist);
            }

            
            var defaultPlaylist = currentUser.FindPlaylistByName("Wszystkie utwory");
            if (defaultPlaylist != null)
            {
                currentPlaylist = defaultPlaylist;
                PlaylistsList.SelectedItem = defaultPlaylist;
            }

            // Przywróć ustawienia użytkownika
            RestoreUserSettings(currentUser);

            // Odśwież interfejs
            RefreshDisplayedSongs();
            UpdateSongsCount();
            CurrentUserLabel.Text = $"Użytkownik: {currentUser.Name}";

            Console.WriteLine($"Przełączono na użytkownika: {currentUser.Name}");
        }

        /// <summary>
        /// Zapisuje stan aktualnego użytkownika
        /// </summary>
        private void SaveCurrentUserState()
        {
            if (currentUser == null) return;

            currentUser.Settings.Volume = (int)VolumeSlider.Value;
            currentUser.Settings.SelectedPlaylistName = currentPlaylist?.Name ?? string.Empty;
            currentUser.Settings.CurrentSongPath = currentSong?.Path ?? string.Empty;
            currentUser.Settings.CurrentPosition = mediaPlayer.Position;
        }

        /// <summary>
        /// Przywraca ustawienia użytkownika
        /// </summary>
        /// <param name="user">Użytkownik</param>
        private void RestoreUserSettings(User user)
        {
            if (user?.Settings == null) return;

            // Przywróć głośność
            VolumeSlider.Value = user.Settings.Volume;
            mediaPlayer.Volume = user.Settings.Volume / 100.0;

            // Przywróć wybraną playlistę
            if (!string.IsNullOrEmpty(user.Settings.SelectedPlaylistName))
            {
                var savedPlaylist = currentUser.FindPlaylistByName(user.Settings.SelectedPlaylistName);
                if (savedPlaylist != null)
                {
                    currentPlaylist = savedPlaylist;
                    PlaylistsList.SelectedItem = savedPlaylist;
                }
            }

            // Przywróć aktualny utwór
            if (!string.IsNullOrEmpty(user.Settings.CurrentSongPath) && File.Exists(user.Settings.CurrentSongPath))
            {
                var savedSong = currentPlaylist?.Songs.FirstOrDefault(s => s.Path == user.Settings.CurrentSongPath);
                if (savedSong != null)
                {
                    currentSong = savedSong;
                    SongsList.SelectedItem = savedSong;
                    positionToRestore = user.Settings.CurrentPosition;
                    mediaPlayer.Open(new Uri(savedSong.Path));
                    RefreshSongInfo(savedSong);
                }
            }
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
            // Ta metoda teraz jest używana tylko do kompatybilności wstecznej
            // Playlisty są teraz zarządzane przez użytkowników
            Console.WriteLine("Playlisty są teraz zarządzane przez użytkowników");
        }

        /// <summary>
        /// Wczytuje ustawienia użytkownika z pliku i przywraca stan aplikacji
        /// </summary>
        private void LoadSettingsFromFile()
        {
            userSettings = settingsManager.LoadSettings();

            // Przywróć aktualnego użytkownika
            if (!string.IsNullOrEmpty(userSettings.CurrentUserName))
            {
                var savedUser = allUsers.FirstOrDefault(u => u.Name == userSettings.CurrentUserName);
                if (savedUser != null)
                {
                    UsersList.SelectedItem = savedUser;
                    SwitchToUser(savedUser);
                    return;
                }
            }

            // Jeśli nie znaleziono zapisanego użytkownika, ustaw pierwszego
            if (allUsers.Count > 0)
            {
                UsersList.SelectedItem = allUsers[0];
                SwitchToUser(allUsers[0]);
            }
        }

        /// <summary>
        /// Zapisuje playlisty przy zamykaniu aplikacji
        /// </summary>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveCurrentUserState();
            userManager.SaveUsers(allUsers);
            SaveCurrentSettings();
        }

        /// <summary>
        /// Zapisuje aktualne ustawienia użytkownika do pliku JSON
        /// </summary>
        private void SaveCurrentSettings()
        {
            var settings = new UserSettings
            {
                Volume = (int)VolumeSlider.Value,
                SelectedPlaylistName = currentPlaylist?.Name ?? string.Empty,
                CurrentSongPath = currentSong?.Path ?? string.Empty,
                CurrentPosition = mediaPlayer.Position,
                CurrentUserName = currentUser?.Name ?? string.Empty
            };
            settingsManager.SaveSettings(settings);
        }

        /// <summary>
        /// Dodaje utwory do aktualnie wybranej playlisty
        /// </summary>
        private void AddSongsButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("Brak wybranego użytkownika!", "Błąd",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Globalna Playlist dla użytkownika
            var systemPlaylist = currentUser.FindPlaylistByName("Wszystkie utwory");

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
                        if (!currentPlaylist.IsSystemPlaylist && systemPlaylist != null)
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
        /// Obsługuje anulowanie tworzenia nowej playlisty
        /// </summary>
        private void CancelPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            CreatePlaylistDialog.Visibility = Visibility.Collapsed;
            ClearPlaylistForm();
        }

        /// <summary>
        /// Obsługuje potwierdzenie tworzenia nowej playlisty
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
            if (currentUser == null) return;

            var newPlaylist = new Playlist(name);
            currentUser.AddPlaylist(newPlaylist);
            allPlaylists.Add(newPlaylist);

            PlaylistsList.SelectedItem = newPlaylist;
            currentPlaylist = newPlaylist;
            RefreshDisplayedSongs();
            CreatePlaylistDialog.Visibility = Visibility.Collapsed;
            ClearPlaylistForm();
            MessageBox.Show($"Pomyślnie utworzono playlistę {newPlaylist.Name}");
        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku usuwania playlisty
        /// </summary>
        private void DeletePlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePlaylist(currentPlaylist);
        }

        /// <summary>
        /// Usuwa wskazaną playlistę
        /// </summary>
        /// <param name="playlist">Playlista do usunięcia</param>
        private void DeletePlaylist(Playlist playlist)
        {
            if (currentUser == null || playlist == null) return;

            var (wasDeleted, wasCurrentPlaylist, message) = playlistManager.DeletePlaylistSafely(
                playlist, currentPlaylist, allPlaylists);

            if (!wasDeleted)
            {
                MessageBox.Show(message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Usuń z użytkownika
            currentUser.RemovePlaylist(playlist);

            if (wasCurrentPlaylist)
            {
                var defaultPlaylist = currentUser.FindPlaylistByName("Wszystkie utwory");
                currentPlaylist = defaultPlaylist;
                PlaylistsList.SelectedItem = defaultPlaylist;
                RefreshDisplayedSongs();
            }
            MessageBox.Show(message, "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku tworzenia użytkownika
        /// </summary>
        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUserDialog.Visibility = Visibility.Visible;
            UserNameTextBox.Focus();
        }

        /// <summary>
        /// Obsługuje anulowanie tworzenia użytkownika
        /// </summary>
        private void CancelUserButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUserDialog.Visibility = Visibility.Collapsed;
            ClearUserForm();
        }

        /// <summary>
        /// Obsługuje potwierdzenie tworzenia użytkownika
        /// </summary>
        private void OkUserButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text?.Trim();
            var (isValid, errorMessage) = userManager.ValidateUserName(userName, allUsers);

            if (!isValid)
            {
                WriteErrorMsg(errorMessage, UserErrorMessageTextBlock);
                return;
            }

            CreateNewUser(userName);
        }

        /// <summary>
        /// Tworzy nowego użytkownika
        /// </summary>
        /// <param name="userName">Nazwa użytkownika</param>
        private void CreateNewUser(string userName)
        {
            User newUser = userManager.CreateNewUser(userName, allUsers);

            // Przełącz na nowego użytkownika
            UsersList.SelectedItem = newUser;
            SwitchToUser(newUser);

            CreateUserDialog.Visibility = Visibility.Collapsed;
            ClearUserForm();
            MessageBox.Show($"Pomyślnie utworzono użytkownika: {newUser.Name}");
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
        /// Czyści formularz tworzenia playlisty
        /// </summary>
        private void ClearPlaylistForm()
        {
            PlaylistNameTextBox.Text = "";
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            ErrorMessageTextBlock.Text = "";
        }

        /// <summary>
        /// Czyści formularz tworzenia użytkownika
        /// </summary>
        private void ClearUserForm()
        {
            UserNameTextBox.Text = "";
            UserErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            UserErrorMessageTextBlock.Text = "";
        }

        /// <summary>
        /// Obsługuje podwójne kliknięcie na utworze w liście
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
        /// </summary>
        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                var duration = mediaPlayer.NaturalDuration.TimeSpan;
                ProgressSlider.Maximum = duration.TotalSeconds;
                ProgressSlider.Minimum = 0;

                TotalTime.Text = duration.ToString(@"mm\:ss");

                if (positionToRestore.HasValue)
                {
                    mediaPlayer.Position = positionToRestore.Value;

                    Task.Delay(100).ContinueWith(_ =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            var actualPosition = mediaPlayer.Position;
                            ProgressSlider.Value = actualPosition.TotalSeconds;
                            CurrentTime.Text = actualPosition.ToString(@"mm\:ss");

                            if (currentSong != null)
                            {
                                RefreshSongInfo(currentSong);
                            }

                            Console.WriteLine($"UI zaktualizowane: {actualPosition.ToString(@"mm\:ss")} / {duration.ToString(@"mm\:ss")}");
                        });
                    });

                    positionToRestore = null;
                }
                else
                {
                    ProgressSlider.Value = 0;
                    CurrentTime.Text = "00:00";
                }
            }
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (currentSong == null || currentPlaylist == null) return;

            int currIndex = currentPlaylist.Songs.ToList().IndexOf(currentSong);
            if (currIndex != -1)
            {
                int nextIndex = (currIndex + 1) >= currentPlaylist.Songs.Count ? 0 : currIndex + 1;
                Song selectedSong = currentPlaylist.Songs[nextIndex];
                Console.WriteLine($"Obecna Indeks - {currIndex} Nastepna Piosenka - {nextIndex}");

                if (currentSong.IsActive)
                {
                    currentSong.Stop(mediaPlayer);
                    currentSong.IsActive = false;
                    positionTimer.Stop();
                }
                selectedSong.Play(mediaPlayer);
                selectedSong.IsActive = true;
                currentSong = selectedSong;
                SongsList.SelectedItem = selectedSong;
                PlayPauseButton.Content = "⏸";
                positionTimer.Start();
                RefreshSongInfo(selectedSong);
            }
        }

        /// <summary>
        /// Timer wywoływany co 500ms podczas odtwarzania
        /// </summary>
        private void PositionTimer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer != null && currentSong != null && currentSong.IsActive)
            {
                ProgressSlider.Value = mediaPlayer.Position.TotalSeconds;
                CurrentTime.Text = mediaPlayer.Position.ToString(@"mm\:ss");
            }
        }

        /// <summary>
        /// Obsługuje zmiany wartości slidera postępu przez użytkownika
        /// </summary>
        private void ProgressSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && mediaPlayer != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                mediaPlayer.Position = TimeSpan.FromSeconds(ProgressSlider.Value);
            }
        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku Play/Pause
        /// </summary>
        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSong == null)
            {
                if (currentPlaylist?.SongCount <= 0) return;
                currentSong = currentPlaylist?.Songs.FirstOrDefault();
                currentSong?.Play(mediaPlayer);
                if (currentSong != null)
                {
                    currentSong.IsActive = true;
                    PlayPauseButton.Content = "⏸";
                    positionTimer.Start();
                    RefreshSongInfo(currentSong);
                }
            }
            else if (currentSong.IsActive)
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
        /// Obsługuje kliknięcie przycisku następny utwór
        /// </summary>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSong == null || currentPlaylist == null) return;

            int currIndex = currentPlaylist.Songs.ToList().IndexOf(currentSong);
            if (currIndex != -1)
            {
                int nextIndex = (currIndex + 1) >= currentPlaylist.Songs.Count ? 0 : currIndex + 1;
                Song selectedSong = currentPlaylist.Songs[nextIndex];
                Console.WriteLine($"Obecna Indeks - {currIndex} Nastepna Piosenka - {nextIndex}");

                if (currentSong.IsActive)
                {
                    currentSong.Stop(mediaPlayer);
                    currentSong.IsActive = false;
                    positionTimer.Stop();
                }
                selectedSong.Play(mediaPlayer);
                selectedSong.IsActive = true;
                currentSong = selectedSong;
                SongsList.SelectedItem = selectedSong;
                PlayPauseButton.Content = "⏸";
                positionTimer.Start();
                RefreshSongInfo(selectedSong);
            }
        }

        /// <summary>
        /// Obsługuje kliknięcie przycisku poprzedni utwór
        /// </summary>
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSong == null || currentPlaylist == null) return;

            int currIndex = currentPlaylist.Songs.ToList().IndexOf(currentSong);
            if (currIndex != -1)
            {
                int prevIndex = (currIndex - 1) < 0 ? currentPlaylist.Songs.Count - 1 : currIndex - 1;
                Song selectedSong = currentPlaylist.Songs[prevIndex];
                Console.WriteLine($"Obecna Indeks - {currIndex} Nastepna Piosenka - {prevIndex}");

                if (currentSong.IsActive)
                {
                    currentSong.Stop(mediaPlayer);
                    currentSong.IsActive = false;
                    positionTimer.Stop();
                }
                selectedSong.Play(mediaPlayer);
                selectedSong.IsActive = true;
                currentSong = selectedSong;
                SongsList.SelectedItem = selectedSong;
                PlayPauseButton.Content = "⏸";
                positionTimer.Start();
                RefreshSongInfo(selectedSong);
            }
        }

        /// <summary>
        /// Obsługuje zmiany slidera głośności
        /// </summary>
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaPlayer != null)
            {
                mediaPlayer.Volume = VolumeSlider.Value / 100.0;
            }
        }

        /// <summary>
        /// Aktualizuje sekcję informacji o aktualnym utworze
        /// </summary>
        /// <param name="song">Utwór do wyświetlenia lub null dla braku utworu</param>
        private void RefreshSongInfo(Song song)
        {
            if (song != null)
            {
                CurrentSongTitle.Text = song.Name;
                CurrentArtist.Text = string.IsNullOrWhiteSpace(song.Author) ? "Autor nieznany" : song.Author;
                CurrentAlbum.Text = string.IsNullOrWhiteSpace(song.Album) ? "" : song.Album;
                CoverImageBrush.ImageSource = song.CoverImage;
            }
            else
            {
                CurrentSongTitle.Text = "Brak aktualnie odtwarzanego utworu.";
            }
        }
    }
}