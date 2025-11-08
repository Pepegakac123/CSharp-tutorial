using System;
using System.Threading;
using System.Threading.Tasks;

public sealed class Logger
{
    private static Logger instance;
    private static readonly object lokcObj = new object();

    private static int constructorCalls = 0;

    private Logger()
    {
        Interlocked.Increment(ref constructorCalls);
        Console.WriteLine($"\n--- Logger: Nowa instancja została utworzona ({constructorCalls}. raz/y) ---");
    }

    // Statyczna metoda dostępu
    public static Logger Instance()
    {
        if (instance == null)
        {
            lock (lokcObj)
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
            }
        }
        return instance;
    }

    public void LoggActivity(string log)
    {
        Console.WriteLine($"[Wątek {Thread.CurrentThread.ManagedThreadId} | HashCode: {this.GetHashCode()}]: {DateTime.Now:HH:mm:ss.fff} | {log}");
    }


    // public static void Main(string[] args)
    // {
    //     Console.WriteLine("--- Test wielowątkowosci dla Singletona ---");

    //     Action logAction1 = () =>
    //     {
    //         Logger logger = Logger.Instance();
    //         logger.LoggActivity("Aktywność 1: Wątek nr 1 próbuje logować.");
    //     };

    //     Action logAction2 = () =>
    //     {
    //         Logger logger = Logger.Instance();
    //         logger.LoggActivity("Aktywność 2: Wątek nr 2 próbuje logować.");
    //     };

    //     Thread process1 = new Thread(new ThreadStart(logAction1));
    //     Thread process2 = new Thread(new ThreadStart(logAction2));

    //     Console.WriteLine("\nUruchamiam dwa wątki jednocześnie...");

    //     process1.Start();
    //     process2.Start();

    //     process1.Join();
    //     process2.Join();


    //     Console.WriteLine("\n--- WERYFIKACJA WYNIKÓW ---");

    //     if (constructorCalls == 1)
    //     {
    //         Console.WriteLine($"Konstruktor został wywołany **{constructorCalls} raz**.");
    //         Console.WriteLine("Oba wątki użyły tej samej (jedynej) instancji Singletona.");
    //     }
    //     else
    //     {
    //         Console.WriteLine($"Konstruktor został wywołany **{constructorCalls} razy**.");
    //         Console.WriteLine("Wielowątkowy Singleton zawiódł!");
    //     }
    // }
}