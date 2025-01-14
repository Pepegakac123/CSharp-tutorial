using System;
using System.Collections.Generic;
using System.Linq;

namespace AgreagatorLotow
{
    public class AgregatorLotow
    {

        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            Dictionary<string, UserData> users = new Dictionary<string, UserData>();
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(" ");
                string userName = input[1];
                string ipAddress = input[0];
                int duration = int.Parse(input[2]);
                //Console.WriteLine($"{userName}: {duration} [{ipAddress}]");
                if (!users.ContainsKey(userName))
                {
                    users[userName] = new UserData(duration, ipAddress);
                }
                else
                {
                    users[userName].Duration += duration;
                    users[userName].IpAddresses.Add(ipAddress);
                }

            }
             
            foreach (var user in users.OrderBy(x => x.Key))
            {
                // Sortowanie IP dla każdego użytkownika
                var sortedIps = user.Value.IpAddresses.OrderBy(ip => ip);

                Console.WriteLine($"{user.Key}: {user.Value.Duration} [{string.Join(", ", sortedIps)}]");
            }
        }
    }

    public class UserData
    {
        public int Duration { get; set; }
        public HashSet<string> IpAddresses { get; set; }

        public UserData(int duration, string ipAddress)
        {
            // Inicjalizacja HashSet w konstruktorze!
            IpAddresses = new HashSet<string>();

            Duration = duration;  
            if (ipAddress == null)
            {
                ipAddress = "192.168.0.1";
            }
            IpAddresses.Add(ipAddress);
        }
    }

}

