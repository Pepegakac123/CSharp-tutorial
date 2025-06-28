using Newtonsoft.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicPlayer
{
    public class SettingsManager
    {
        private readonly string _settingsFilePath;
        /// <summary>
        /// Konstruktor - ustawia ścieżkę do pliku z ustawieniami
        /// </summary>
        public SettingsManager()
        {
            // Ścieżka do pliku w folderze Dokumenty
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string musicPlayerFolder = Path.Combine(documentsFolder, "MusicPlayer");

            // Upewnij się, że folder istnieje
            if (!Directory.Exists(musicPlayerFolder))
            {
                Directory.CreateDirectory(musicPlayerFolder);
            }

            _settingsFilePath = Path.Combine(musicPlayerFolder, "settings.json");
        }
        public UserSettings LoadSettings()
        {
            try
            {
                if (!File.Exists(_settingsFilePath))
                    return new UserSettings();

                string jsonString = File.ReadAllText(_settingsFilePath);
                UserSettings settings = JsonConvert.DeserializeObject<UserSettings>(jsonString);

                return settings ?? new UserSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas wczytywania ustawień: {ex.Message}");
                return new UserSettings(); // Zwróć domyślne w przypadku błędu
            }


        }

        public void SaveSettings(UserSettings settings)
        {
            settings.LastSaved = DateTime.Now;
            try
            {
                string jsonString = JsonConvert.SerializeObject(settings, Formatting.Indented);

                // Zapisz do pliku
                File.WriteAllText(_settingsFilePath, jsonString);

                Console.WriteLine($"Zapisano ustawienia usera do: {_settingsFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania ustawień: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }


}
