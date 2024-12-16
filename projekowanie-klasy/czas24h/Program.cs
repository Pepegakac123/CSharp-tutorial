public class Czas24h
{
    private int liczbaSekund;

    public int Sekunda
    {
        get => liczbaSekund - Godzina * 60 * 60 - Minuta * 60;
        set
        {
            if (value < 0 || value > 59)
                throw new ArgumentException("error");
            liczbaSekund = Godzina * 3600 + Minuta * 60 + value;
        }
    }

    public int Minuta
    {
        get => (liczbaSekund / 60) % 60;
        set
        {
            if (value < 0 || value > 59)
                throw new ArgumentException("error");
            liczbaSekund = Godzina * 3600 + value * 60 + Sekunda;
        }
    }

    public int Godzina
    {
        get => liczbaSekund / 3600;
        set
        {
            if (value < 0 || value > 23)
                throw new ArgumentException("error");
            liczbaSekund = value * 3600 + Minuta * 60 + Sekunda;
        }
    }

    public Czas24h(int godzina, int minuta, int sekunda)
    {
        if (sekunda < 0 || sekunda > 59 || minuta < 0 || minuta > 59 || godzina < 0 || godzina > 23)
            throw new ArgumentException("error");
        // uzupełnij kod zgłaszając wyjątek ArgumentException
        // w sytuacji niepoprawnych danych

        liczbaSekund = sekunda + 60 * minuta + 3600 * godzina;
    }

    public override string ToString() => $"{Godzina}:{Minuta:D2}:{Sekunda:D2}";
}
public class Program
{
    public static void Main(string[] args)
    {
        // Test 5
        try
        {
            var t = new Czas24h(24, 15, 37);
            t.Minuta = 20;
            t.Godzina = 23;
            t.Godzina = 1;
            Console.WriteLine(t);

        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
