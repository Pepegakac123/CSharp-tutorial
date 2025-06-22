using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MusicPlayer
{
    ///<summary>
    /// Klasa bazowa dla danych piosenki
    ///</summary>
    public abstract class SongData
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public TimeSpan Duration { get; set; }
        public string Album { get; set; }
        public BitmapImage CoverImage { get; set; }
        public bool IsActive { get; set; }

        protected SongData()
        {
            Name = string.Empty;
            Author = string.Empty;
            Duration = TimeSpan.Zero;
            Album = string.Empty;
            CoverImage = null;
            IsActive = false;
        }

        public abstract void Play(MediaPlayer player);
        public abstract void Stop(MediaPlayer player);
    }

    public class Song : SongData
    {
        public string Path { get; set; }

        public Song() : base()
        {
            Path = string.Empty;
        }

        public Song(string name, string author, TimeSpan duration, string path, string album, BitmapImage coverImage) : base()
        {
            Name = name;
            Author = author;
            Duration = duration;
            Path = path;
            Album = album; 
            CoverImage = coverImage;
        }

        public override void Play(MediaPlayer player)
        {
            IsActive = true;
            if (player.Source == null || player.Source.LocalPath != this.Path)
            {
                player.Open(new Uri(this.Path));
            }
            player.Play();
            Console.WriteLine($"Odtwarzam: {Author} - {Name}");
        }

        public override void Stop(MediaPlayer player)
        {
            IsActive = false;
            player.Pause();
            Console.WriteLine($"Zatrzymuję: {Author} - {Name}");
        }



        public override string ToString()
        {
            return $"{Author} - {Name} ({Duration.Minutes:D2}:{Duration.Seconds:D2})";
        }
    }
}
