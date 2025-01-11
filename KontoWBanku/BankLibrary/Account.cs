using System;

namespace Bank
{
    public interface IAccount
    {
        // nazwa klienta, bez spacji przed i po
        // readonly - modyfikowalna wyłącznie w konstruktorze
        string Name { get; }

        // bilans, aktualny stan środków, podawany w zaokrągleniu do 2 miejsc po przecinku
        decimal Balance { get; }

        // czy konto jest zablokowane
        bool IsBlocked { get; }
        void Block();            // zablokowanie konta
        void Unblock();          // odblokowanie konta

        // wpłata, dla kwoty ujemnej - zignorowana (false)
        // jeśli konto zablokowane - zignorowana (false)
        // true jeśli wykonano i nastąpiła zmiana salda
        bool Deposit(decimal amount);

        // wypłata, dla kwoty ujemnej - zignorowana (false)
        // jeśli konto zablokowane - zignorowana (false)
        // jeśli jest niewystarczająca ilość środków - zignorowana (false)
        // true jeśli wykonano i nastąpiła zmiana salda   
        bool Withdrawal(decimal amount);
    }

    public interface IAccountWithLimit : IAccount
    {
        // przyznany limit debetowy
        // mozliwość zmiany, jeśli konto nie jest zablokowane
        decimal OneTimeDebetLimit { get; set; }

        // dostępne środki, z uwzględnieniem limitu
        decimal AvaibleFounds { get; }
    }
    public class Account : IAccount
    {
        protected const int PRECISION = 4;

        public string Name { get; }
        public decimal Balance { get; private set; }


        public bool IsBlocked { get; private set; } = false;
        public void Block() => IsBlocked = true;
        public void Unblock() => IsBlocked = false;

        public Account(string name, decimal initialBalance = 0)
        {
            if (name == null || initialBalance < 0)
                throw new ArgumentOutOfRangeException();
            Name = name.Trim();
            if (Name.Length < 3)
                throw new ArgumentException();
            Balance = Math.Round(initialBalance, PRECISION);
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0 || IsBlocked) return false;

            Balance = Math.Round(Balance += amount, PRECISION);
            return true;
        }

        public bool Withdrawal(decimal amount)
        {
            if (amount <= 0 || IsBlocked || amount > Balance) return false;

            Balance = Math.Round(Balance -= amount, PRECISION);
            return true;
        }

        public override string ToString() =>
            IsBlocked ? $"Account name: {Name}, balance: {Balance:F2}, blocked"
                        : $"Account name: {Name}, balance: {Balance:F2}";
    }

    public class AccountPlus : Bank.Account, Bank.IAccountWithLimit
    {
        private decimal _oneTimeDebetLimit;
        private decimal _usedLimit;
        private bool _isManuallyBlocked;

        public decimal OneTimeDebetLimit
        {
            get => _oneTimeDebetLimit;
            set
            {
                if (IsBlocked || value < 0) return;
                _oneTimeDebetLimit = value;
            }
        }

        public decimal AvaibleFounds => Balance + (OneTimeDebetLimit - _usedLimit);

        public AccountPlus(string name, decimal initialBalance = 0, decimal initialLimit = 100)
            : base(name, initialBalance)
        {
            _oneTimeDebetLimit = initialLimit < 0 ? 0 : initialLimit;
        }

        public new bool Withdrawal(decimal amount)
        {
            if (amount <= 0 || IsBlocked || AvaibleFounds < amount)
                return false;

            // Standardowa wypłata
            if (amount <= Balance)
                return base.Withdrawal(amount);

            // Wypłata z użyciem limitu
            decimal fromBalance = Balance;
            if (base.Withdrawal(fromBalance))  // Najpierw wypłacamy całe saldo
            {
                _usedLimit = amount - fromBalance;  // Reszta z limitu
                Block();
                return true;
            }

            return false;
        }

        public new bool Deposit(decimal amount)
        {
            if (amount <= 0) return false;

            if (_usedLimit > 0)
            {
                // Najpierw wpłacamy całość na konto
                if (!base.Deposit(amount))
                    return false;

                // Następnie rozliczamy limit
                decimal toLimit = Math.Min(amount, _usedLimit);
                _usedLimit -= toLimit;

                // Odblokowujemy konto jeśli spłaciliśmy cały limit
                if (_usedLimit == 0 && !_isManuallyBlocked)
                {
                    base.Unblock();
                }

                return true;
            }

            return base.Deposit(amount);
        }

        public new void Block()
        {
            _isManuallyBlocked = true;
            base.Block();
        }

        public new void Unblock()
        {
            if (_usedLimit > 0) return;
            _isManuallyBlocked = false;
            base.Unblock();
        }

        public override string ToString() =>
            IsBlocked
                ? $"Account name: {Name}, balance: {Balance:F2}, blocked, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}"
                : $"Account name: {Name}, balance: {Balance:F2}, avaible founds: {AvaibleFounds:F2}, limit: {OneTimeDebetLimit:F2}";
    }
}