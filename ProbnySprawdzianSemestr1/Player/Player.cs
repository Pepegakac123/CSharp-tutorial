using System.Linq;
public class Player
{
    private string _name;
    private string _password;
    private List<int> _scores = new List<int>();

    // Konstruktor z wymaganymi parametrami
    public Player(string name, string password)
    {
        // Najpierw sprawdzamy nazwę
        if (name == null) throw new ArgumentException("Wrong name!");
        string processedName = name.Replace(" ", "");
        if (processedName.Length < 3 || !char.IsLetter(processedName[0]))
            throw new ArgumentException("Wrong name!");
        foreach (char c in processedName)
        {
            if (!char.IsLetter(c) && !char.IsDigit(c))
                throw new ArgumentException("Wrong name!");
        }
        _name = processedName.ToLower();

        // Następnie sprawdzamy hasło
        if (!IsPasswordValid(password))
            throw new ArgumentException("Wrong password!");
        _password = password;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (value == null) throw new ArgumentException("Wrong name!");
            string processedValue = value.Replace(" ", "");
            if (processedValue.Length < 3)
                throw new ArgumentException("Wrong name!");
            if (!char.IsLetter(processedValue[0]))
                throw new ArgumentException("Wrong name!");
            char[] chars = processedValue.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!char.IsLetter(chars[i]) && !char.IsDigit(chars[i]))
                    throw new ArgumentException("Wrong name!");
                chars[i] = char.ToLower(chars[i]);
            }
            _name = new string(chars);
        }
    }

    public int BestScore
    {
        get => _scores.Count > 0 ? _scores.Max() : 0;
    }

    public int LastScore
    {
        get => _scores.Count > 0 ? _scores[_scores.Count - 1] : 0;
    }

    public double AvgScore
    {
        get => _scores.Count > 0 ? Math.Round(_scores.Average(), 1) : 0.0;
    }

    public void AddScore(int currentScore)
    {
        if (currentScore < 0 || currentScore > 100)
            throw new ArgumentOutOfRangeException("Wrong value!", "Wrong value!");
        _scores.Add(currentScore);
    }

    public bool TryAddScore(int currentScore)
    {
        if (currentScore < 0 || currentScore > 100)
            return false;
        _scores.Add(currentScore);
        return true;
    }

    public bool ChangePassword(string oldPassword, string newPassword)
    {
        if (!VerifyPassword(oldPassword) || !IsPasswordValid(newPassword))
            return false;
        _password = newPassword;
        return true;
    }

    public bool VerifyPassword(string password)
    {
        return _password == password;
    }

    private bool IsPasswordValid(string password)
    {
        if (password == null) return false;
        if (password.Length < 8 || password.Length > 16) return false;
        if (password != password.Trim()) return false;

        bool hasLower = false;
        bool hasUpper = false;
        bool hasDigit = false;
        bool hasPunctuation = false;

        foreach (char c in password)
        {
            if (c > 127) return false;
            if (char.IsControl(c)) return false;
            if (char.IsLower(c)) hasLower = true;
            if (char.IsUpper(c)) hasUpper = true;
            if (char.IsDigit(c)) hasDigit = true;
            if (char.IsPunctuation(c)) hasPunctuation = true;
        }

        return hasLower && hasUpper && hasDigit && hasPunctuation;
    }

    public override string ToString()
    {
        return $"Name: {Name}, Score: last={LastScore}, best={BestScore}, avg={AvgScore}";
    }
}