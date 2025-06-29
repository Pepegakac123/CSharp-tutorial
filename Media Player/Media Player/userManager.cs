using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace MusicPlayer
{

    public class UserManager
    {
        private readonly string _usersFilePath;
        private SongManager songManager = new SongManager();

        public UserManager()
        {
            // Ścieżka do pliku w folderze Dokumenty
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string musicPlayerFolder = Path.Combine(documentsFolder, "MusicPlayer");

            // Upewnij się, że folder istnieje
            if (!Directory.Exists(musicPlayerFolder))
            {
                Directory.CreateDirectory(musicPlayerFolder);
            }

            _usersFilePath = Path.Combine(musicPlayerFolder, "users.json");
        }

        public void SaveUsers(ObservableCollection<User> users)
        {
            try
            {
                var usersToSave = users.Select(user => new
                {
                    Name = user.Name,
                    IsActive = user.IsActive,
                    Settings = new
                    {
                        Volume = user.Settings.Volume,
                        SelectedPlaylistName = user.Settings.SelectedPlaylistName,
                        CurrentSongPath = user.Settings.CurrentSongPath,
                        CurrentPosition = user.Settings.CurrentPosition.ToString(),
                        LastSaved = user.Settings.LastSaved.ToString("yyyy-MM-dd HH:mm:ss")
                    },
                    Playlists = user.UserPlaylists.Select(playlist => new
                    {
                        Name = playlist.Name,
                        IsSystemPlaylist = playlist.IsSystemPlaylist,
                        CreatedDate = playlist.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                        Songs = playlist.Songs.Select(song => new
                        {
                            Name = song.Name,
                            Author = song.Author,
                            Duration = song.Duration.ToString(),
                            Path = song.Path,
                            Album = song.Album
                        }).ToList()
                    }).ToList()
                }).ToList();

                string jsonString = JsonConvert.SerializeObject(usersToSave, Formatting.Indented);

                // Zapisz do pliku
                File.WriteAllText(_usersFilePath, jsonString);

                Console.WriteLine($"Zapisano {users.Count} użytkowników do: {_usersFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania użytkowników: {ex.Message}",
                               "Błąd zapisu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public ObservableCollection<User> LoadUsers()
        {
            var users = new ObservableCollection<User>();

            try
            {
                // Sprawdź czy plik istnieje
                if (!File.Exists(_usersFilePath))
                {
                    Console.WriteLine("Plik users.json nie istnieje - tworzę domyślnego użytkownika.");

                    // Utwórz domyślnego użytkownika
                    var defaultUser = new User("Domyślny Użytkownik");
                    defaultUser.CreateDefaultPlaylist();
                    defaultUser.IsActive = true;
                    users.Add(defaultUser);

                    return users;
                }

                // Wczytaj JSON
                string jsonString = File.ReadAllText(_usersFilePath);
                var loadedUsers = JsonConvert.DeserializeObject<List<dynamic>>(jsonString);

                int loadedUsersCount = 0;
                int errorCount = 0;

                foreach (var userData in loadedUsers)
                {
                    try
                    {
                        // Wczytaj dane użytkownika
                        string name = userData.Name?.ToString() ?? "Nienazwany użytkownik";
                        bool isActive = userData.IsActive?.ToString() == "True";

                        User user = new User(name)
                        {
                            IsActive = isActive
                        };

                        // Wczytaj ustawienia użytkownika
                        if (userData.Settings != null)
                        {
                            try
                            {
                                user.Settings.Volume = userData.Settings.Volume ?? 50;
                                user.Settings.SelectedPlaylistName = userData.Settings.SelectedPlaylistName?.ToString() ?? "";
                                user.Settings.CurrentSongPath = userData.Settings.CurrentSongPath?.ToString() ?? "";

                                if (TimeSpan.TryParse(userData.Settings.CurrentPosition?.ToString(), out TimeSpan position))
                                {
                                    user.Settings.CurrentPosition = position;
                                }

                                if (DateTime.TryParse(userData.Settings.LastSaved?.ToString(), out DateTime lastSaved))
                                {
                                    user.Settings.LastSaved = lastSaved;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Błąd podczas wczytywania ustawień użytkownika {name}: {ex.Message}");
                            }
                        }

                        if (userData.Playlists != null)
                        {
                            int playlistsLoaded = 0;
                            int playlistsError = 0;

                            foreach (var playlistData in userData.Playlists)
                            {
                                try
                                {
                                    string playlistName = playlistData.Name?.ToString() ?? "";
                                    bool isSystemPlaylist = playlistData.IsSystemPlaylist?.ToString() == "True";

                                    DateTime createdDate = DateTime.Now;
                                    if (playlistData.CreatedDate != null)
                                    {
                                        DateTime.TryParse(playlistData.CreatedDate.ToString(), out createdDate);
                                    }

                                    Playlist playlist = new Playlist(playlistName)
                                    {
                                        CreatedAt = createdDate,
                                        IsSystemPlaylist = isSystemPlaylist
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
                                                string songPath = songData.Path?.ToString() ?? "";

                                                // Sprawdź czy plik nadal istnieje
                                                if (File.Exists(songPath))
                                                {
                                                    Song song = songManager.CreateSongFromFile(songPath);
                                                    playlist.AddSong(song);
                                                    songsLoaded++;
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Plik nie istnieje: {songPath}");
                                                    songsError++;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"Błąd podczas wczytywania utworu: {ex.Message}");
                                                songsError++;
                                            }
                                        }

                                        Console.WriteLine($"Playlista '{playlistName}': wczytano {songsLoaded} utworów, błędów: {songsError}");
                                    }

                                    user.AddPlaylist(playlist);
                                    playlistsLoaded++;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Błąd podczas wczytywania playlisty: {ex.Message}");
                                    playlistsError++;
                                }
                            }

                            Console.WriteLine($"Użytkownik '{name}': wczytano {playlistsLoaded} playlist, błędów: {playlistsError}");
                        }

                        // Upewnij się, że użytkownik ma domyślną playlistę
                        if (user.PlaylistCount == 0)
                        {
                            user.CreateDefaultPlaylist();
                        }

                        users.Add(user);
                        loadedUsersCount++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Błąd podczas wczytywania użytkownika: {ex.Message}");
                        errorCount++;
                    }
                }

                if (loadedUsersCount > 0 || errorCount > 0)
                {
                    string message = $"Wczytano {loadedUsersCount} użytkowników.";
                    if (errorCount > 0)
                    {
                        message += $"\nBłędów użytkowników: {errorCount}";
                    }

                    MessageBox.Show(message, "Wczytywanie zakończone",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                }

                if (users.Count == 0)
                {
                    var defaultUser = new User("Domyślny Użytkownik");
                    defaultUser.CreateDefaultPlaylist();
                    defaultUser.IsActive = true;
                    users.Add(defaultUser);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania użytkowników: {ex.Message}",
                               "Błąd wczytywania", MessageBoxButton.OK, MessageBoxImage.Error);

                // W przypadku błędu, utwórz domyślnego użytkownika
                var defaultUser = new User("Domyślny Użytkownik");
                defaultUser.CreateDefaultPlaylist();
                defaultUser.IsActive = true;
                users.Add(defaultUser);
            }

            return users;
        }

        public (bool isValid, string errorMessage) ValidateUserName(string name, ObservableCollection<User> allUsers)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return (false, "Nazwa użytkownika nie może być pusta");
            }

            if (name.Length > 25)
            {
                return (false, "Nazwa użytkownika nie może przekraczać 25 znaków");
            }

            if (UserNameExists(allUsers, name))
            {
                return (false, $"Użytkownik o nazwie '{name}' już istnieje");
            }

            return (true, string.Empty);
        }


        public User CreateNewUser(string name, ObservableCollection<User> allUsers)
        {
            User newUser = new User(name);
            newUser.CreateDefaultPlaylist();
            allUsers.Add(newUser);

            Console.WriteLine($"Utworzono nowego użytkownika: {name}");
            return newUser;
        }

        public User FindUserByName(ObservableCollection<User> users, string name)
        {
            return users.FirstOrDefault(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

 
        public bool UserNameExists(ObservableCollection<User> users, string name)
        {
            return users.Any(u => u.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}