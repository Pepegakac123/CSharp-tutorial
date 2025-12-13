// using System;
// using System.Collections.Generic;

// Console.WriteLine("\nTworzymy obiekt proxy");
// IImage image = new ImageProxy("wakacje_2024.jpg");
// Console.WriteLine("\n Zaincjalizowanie lekkiego obiektu proxy i teraz czekamy na wywołanie ciężkiej operajci displayka  ");
// Console.WriteLine("\nPierwsze wywołanie Display()");
// image.Display();

// Console.WriteLine("\n Drugie wywołanie Display()");
// image.Display();



// public interface IImage
// {
//     void Display();
// }
// public class RealImage : IImage
// {
//     private string _filename;

//     public RealImage(string filename)
//     {
//         _filename = filename;
//         LoadFromDisk();
//     }

//     private void LoadFromDisk()
//     {
//         Console.WriteLine($"[RealImage] Ładowanie z dysku pliku: {_filename}...");
//         Thread.Sleep(1000); // Symulacja opóźnienia ładowania
//     }

//     public void Display()
//     {
//         Console.WriteLine($"[RealImage] Wyświetlanie: {_filename}");
//     }
// }

// public class ImageProxy : IImage
// {
//     private RealImage? _realImage;
//     private string _filename;

//     public ImageProxy(string filename)
//     {
//         _filename = filename;
//     }

//     public void Display()
//     {
//         if (_realImage == null)
//         {
//             Console.WriteLine("[Proxy] Inicjalizacja obiektu RealImage");
//             _realImage = new RealImage(_filename);
//         }

//         _realImage.Display();
//     }
// }
