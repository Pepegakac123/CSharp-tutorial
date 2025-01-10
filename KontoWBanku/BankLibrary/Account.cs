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

    public class Account : IAccount
    {
        private readonly string _name;
        private decimal _balance = 0;
        private bool _isBlocked = false;

        public string Name
        {
            get { return _name; }
        }

        public decimal Balance
        {
            get { return _balance; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _balance = Math.Round(value, 4);
            }
        }

        public bool IsBlocked
        {
            get { return _isBlocked; }
            private set { _isBlocked = value; }
        }

        public Account(string name, decimal balance = 0)
        {
            if (name == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            var processedName = name.Trim();
            if (processedName.Length < 3)
            {
                throw new ArgumentException();
            }
            _name = processedName;
            Balance = balance;
        }

        public void Block()
        {
            IsBlocked = true;
        }

        public void Unblock()
        {
            IsBlocked = false;
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0 || IsBlocked) return false;
            Balance += amount;
            return true;
        }

        public bool Withdrawal(decimal amount)
        {
            if (amount <= 0 || IsBlocked || Balance < amount) return false;
            Balance -= amount;
            return true;
        }

        public override string ToString()
        {
            var balanceStr = Balance.ToString("F2");
            if (IsBlocked)
            {
                return string.Format("Account name: {0}, balance: {1}, blocked", Name, balanceStr);
            }
            return string.Format("Account name: {0}, balance: {1}", Name, balanceStr);
        }
    }
}