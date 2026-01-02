Console.WriteLine("--- Test 1: Valid Payment ---");
PaymentService paymentService = new PaymentService();
paymentService.ProcessPayment(new SwiftPayment("PL12345678901234567890123456", 100));

Console.WriteLine("\n--- Test 2: Invalid Amount ---");
paymentService.ProcessPayment(new SwiftPayment("PL12345678901234567890123456", -50));

Console.WriteLine("\n--- Test 3: Invalid Account Length ---");
paymentService.ProcessPayment(new SwiftPayment("PL123", 100));

Console.WriteLine("\n--- Test 4: Invalid Account Prefix ---");
paymentService.ProcessPayment(new SwiftPayment("XX12345678901234567890123456", 100));

Console.WriteLine("\n--- Test 5: Mobile Payment (Adapter) ---");
MobilePayment mobilePayment = new MobilePayment("500-600-700", 250);
IBankPayment adapter = new MobileToBankPaymentAdapter(mobilePayment);
paymentService.ProcessPayment(adapter);

Console.WriteLine("\n--- Test 6: Mobile Payment (Invalid Phone) ---");
MobilePayment mobilePaymentShort = new MobilePayment("123", 50);
paymentService.ProcessPayment(new MobileToBankPaymentAdapter(mobilePaymentShort));

public interface IBankPayment
{
    int Amount();
    string BankAccount();
}

public class PaymentService
{
    public void ProcessPayment(IBankPayment payment)
    {
        if (payment.Amount() <= 0)
        {
            Console.WriteLine("Invalid amount");
            return;
        }
        if (!ValidateBankAccount(payment.BankAccount()))
        {
            Console.WriteLine("Invalid bank account");
            return;
        }
        Console.WriteLine($"Processing payment of {payment.Amount()} to {payment.BankAccount()}");
    }

    private bool ValidateBankAccount(string AccountName)
    {
        if (AccountName.Length != 28)
        {
            Console.WriteLine("Invalid account name (expected 28 chars, got " + AccountName.Length + ")");
            return false;
        }
        if (!AccountName.StartsWith("PL"))
        {
            Console.WriteLine("Account name should start with PL");
            return false;
        }
        for (int i = 2; i < AccountName.Length; i++)
        {
            if (!char.IsDigit(AccountName[i]))
            {
                Console.WriteLine("Account name should contain only digits after PL");
                return false;
            }
        }
        return true;
    }
}

public class SwiftPayment : IBankPayment
{
    private string _accountName;
    private int _amount;

    public SwiftPayment(string accountName, int amount)
    {
        _accountName = accountName;
        _amount = amount;
    }

    public int Amount() => _amount;
    public string BankAccount() => _accountName;
}

public class MobilePayment
{
    public string PhoneNumber { get; }
    public int Amount { get; }

    public MobilePayment(string phoneNumber, int amount)
    {
        PhoneNumber = phoneNumber;
        Amount = amount;
    }
}

public class MobileToBankPaymentAdapter : IBankPayment

{
    private readonly MobilePayment _mobilePayment;
    private static readonly Random _random = new Random();
    public MobileToBankPaymentAdapter(MobilePayment mobilePayment)
    {
        _mobilePayment = mobilePayment;
    }
    public int Amount() => _mobilePayment.Amount;
    public string BankAccount()

    {

        // Logika transformacji:
        // 1. Wyciągamy same cyfry.
        // 2. Dodajemy prefiks "88" (oznaczenie mobilne platnosci).
        // 3. Dopełniamy losowymi cyframi do 26 znaków po "PL".
        // 4. Dodajemy na początku "PL".

        string digitsOnly = new string(_mobilePayment.PhoneNumber.Where(char.IsDigit).ToArray());
        string accountDigits = "88" + digitsOnly;
        while (accountDigits.Length < 26)
        {
            accountDigits += GetRandomDigit();
        }
        if (accountDigits.Length > 26) accountDigits = accountDigits.Substring(0, 26);

        return "PL" + accountDigits;

    }



    private char GetRandomDigit()

    {

        return (char)('0' + _random.Next(0, 10));

    }

}
