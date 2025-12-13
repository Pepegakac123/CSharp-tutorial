// using System;
// using System.Collections.Generic;


// Console.WriteLine("Klient pyta o dane");
// IRemoteService proxy = new RemoteServiceProxy();

// // brak w cache, łączy z siecią
// Console.WriteLine(proxy.GetData("Raport_2024"));

// Console.WriteLine("\nKlient pyta o te same dane 'Raport_2024' ponownie");
// // dane są w cache, brak połączenia z siecią
// Console.WriteLine(proxy.GetData("Raport_2024"));

// Console.WriteLine("\nKlient pyta o NOWE dane 'Statystyki_Q1'");
// // brak w cache, łączy z siecią
// Console.WriteLine(proxy.GetData("Statystyki_Q1"));


// public interface IRemoteService
// {
//     string GetData(string key);
// }

// public class RealRemoteService : IRemoteService
// {
//     public string GetData(string key)
//     {
//         Console.WriteLine($"[SIEĆ] Łączenie z zewnętrznym serwerem po: '{key}'...");
//         System.Threading.Thread.Sleep(500);
//         return $"Wynik dla {key}";
//     }
// }

// public class RemoteServiceProxy : IRemoteService
// {
//     private RealRemoteService _realService = new RealRemoteService();
//     private Dictionary<string, string> _cache = new Dictionary<string, string>();

//     public string GetData(string key)
//     {
//         if (_cache.ContainsKey(key))
//         {
//             Console.WriteLine($"[CACHE] Zwracam dane z pamięci dla: '{key}'");
//             return _cache[key];
//         }
//         string data = _realService.GetData(key);
//         _cache[key] = data;

//         return data;
//     }
// }
