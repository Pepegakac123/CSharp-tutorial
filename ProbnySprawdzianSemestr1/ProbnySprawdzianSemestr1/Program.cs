using System.Collections.Generic;

namespace ProbnySprawdzianSemestr1
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
        public static void Analyze(string logs)
        {
            if (string.IsNullOrWhiteSpace(logs))
            {
                Console.WriteLine("empty");
                return;
            }
            SortedDictionary<string, Log> users = new SortedDictionary<string, Log>();
            HashSet<string> allDates = new HashSet<string>();
            string[] lines = logs.Split('\n');

            foreach (string line in lines)
            {
                string[] userLogs = line.Split(" ");
                string dateStr = userLogs[0];
                string username = userLogs[2];
                string ipAddress = userLogs[3];

                allDates.Add(dateStr);
                if (users.ContainsKey(username))
                {
                    users[username].Dates.Add(dateStr);
                    users[username].IpAddress = ipAddress;
                }
                else
                {
                    users.Add(username, new Log(dateStr, ipAddress));
                }
            }

            List<string> result = new List<string>();


            foreach (var user in users)
            {
                if (user.Value.Dates.Count == allDates.Count)
                {
                    bool hasAllDates = true;
                    foreach (var date in allDates)
                    {
                        if (!user.Value.Dates.Contains(date))
                        {
                            hasAllDates = false;
                            break;
                        }
                    }
                    if (hasAllDates)
                    {
                        result.Add(user.Key);
                    }
                }
            }
            if (result.Count == 0)
            {
                Console.WriteLine("empty");
                return;      
            }
            Console.WriteLine(string.Join(", ", result));


        }

        public class Log
        {
            public HashSet<string> Dates { get; set; }
            public string IpAddress { get; set; }

            public Log(string date,  string ipAddress)
            {
                Dates = new HashSet<string>();
                Dates.Add(date.ToString());
                IpAddress = ipAddress;
            }
        }
    }
}
