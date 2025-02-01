namespace FiguryLib;

public interface IMierzalna1D
{
    double Dlugosc { get; }
}

public interface IMierzalna2D : IMierzalna1D
{
    double Pole { get; }
    double Obwod { get; }
}

abstract public class Figura
{
    public ConsoleColor DefaultColor { get; protected set; } = ConsoleColor.Black;
    // wypisuje na konsolę figurę
    public virtual void Rysuj()
    {
        Console.ResetColor();
        Console.ForegroundColor = this.DefaultColor;
        Console.WriteLine(this);
        Console.ResetColor();
    }
}

// immutable
public class Punkt : Figura, IEquatable<Punkt>
{
    public readonly int X, Y;
    public Punkt(int x = 0, int y = 0) { X = x; Y = y; }
    public override string ToString() => $"P({X}, {Y})";
    public bool Equals(Punkt other) =>
        other != null && X == other.X && Y == other.Y;
}

public class Odcinek : Figura, IMierzalna1D, IEquatable<Odcinek>
{
    public Punkt P1 { get; private set; }
    public Punkt P2 { get; private set; }

    public Odcinek() : this(new Punkt(), new Punkt())
    { }

    public Odcinek(Punkt p1, Punkt p2)
    {
        if (p1 is null || p2 is null)
            throw new ArgumentException("Punkt nie może być null");
        P1 = p1; P2 = p2;
        DefaultColor = ConsoleColor.Blue;
    }

    public double Dlugosc =>
        Math.Round(Math.Sqrt((P2.X - P1.X) * (P2.X - P1.X) + (P2.Y - P1.Y) * (P2.Y - P1.Y)), 2);

    public override string ToString() => $"L({P1}, {P2})";

    public override void Rysuj()
    {
        Console.ResetColor();
        Console.ForegroundColor = this.DefaultColor;
        Console.WriteLine(this + $" dlugosc={Dlugosc:F2}");
        Console.ResetColor();
    }

    public bool Equals(Odcinek other) =>
        other != null && P1 == other.P1 && P2 == other.P2;
}
public class Okrag : Figura, IMierzalna1D
{
    private Punkt srodek;
    private int promien;

    public Punkt Srodek
    {
        get => srodek;
        set
        {
            if (value == null)
                throw new ArgumentException("Punkt nie może być null");
            srodek = value;
        }
    }

    public int Promien
    {
        get => promien;
        set => promien = value < 0 ? 0 : value;
    }

    public double Dlugosc => Math.Round(2 * Math.PI * Promien, 2);

    public Okrag()
    {
        Srodek = new Punkt(0, 0);
        Promien = 1;
        DefaultColor = ConsoleColor.Cyan;
    }

    public Okrag(Punkt srodek, int promien)
    {
        Srodek = srodek;
        Promien = promien;
        DefaultColor = ConsoleColor.Cyan;
    }

    public override string ToString() => $"O({Srodek}, {Promien})";

    public override void Rysuj()
    {
        Console.ResetColor();
        Console.ForegroundColor = DefaultColor;
        Console.WriteLine($"{this} dlugosc={Dlugosc:F2}");
        Console.ResetColor();
    }
}
public class Kolo : Okrag, IMierzalna2D
{
    public double Pole => Math.Round(Math.PI * Promien * Promien, 2);
    public double Obwod => Dlugosc;

    public Kolo() : base()
    {
        Promien = 2;
        DefaultColor = ConsoleColor.Red;
    }

    public Kolo(Punkt srodek, int promien) : base(srodek, promien)
    {
        DefaultColor = ConsoleColor.Red;
    }

    public override string ToString() => $"K({Srodek}, {Promien})";

    public override void Rysuj()
    {
        Console.ResetColor();
        Console.ForegroundColor = DefaultColor;
        Console.WriteLine($"{this} obwod={Obwod:F2}, pole={Pole:F2}");
        Console.ResetColor();
    }
}

public class Ekran
{
    private List<Figura> figury = new List<Figura>();
    public void Dodaj(Figura f) => figury.Add(f);
    public void Usun(Figura f) => figury.Remove(f);
    public void Rysuj() => figury.ForEach(f => f.Rysuj());

    public double SumarycznaDlugosc() => Math.Round(
        figury.Where(f => f is IMierzalna1D)
              .Sum(f => ((IMierzalna1D)f).Dlugosc),
        2);

    public double SumarycznePole() => Math.Round(
        figury.Where(f => f is IMierzalna2D)
              .Sum(f => ((IMierzalna2D)f).Pole),
        2);
}

