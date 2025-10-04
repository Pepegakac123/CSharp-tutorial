using System;

namespace Zadanie1
{


    public class Account
    {
        public string Owner { get; set; }
        protected decimal balance;

        public Account(string owner, decimal initialBalance)
        {
            Owner = owner;
            balance = initialBalance;
        }

        public virtual void Deposit(decimal amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount}. New balance: {balance}");
        }

        public virtual bool Withdraw(decimal amount)
        {
            if (amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew {amount}. Remaining balance: {balance}");
                return true;
            }
            Console.WriteLine("Not enough funds.");
            return false;
        }

        public virtual void DisplayBalance()
        {
            Console.WriteLine($"The balance for {Owner} is {balance}\n");
        }
    }
    public class SavingsAccount : Account
    {
        private decimal interestRate;

        public SavingsAccount(string owner, decimal initialBalance, decimal interestRate)
            : base(owner, initialBalance)
        {
            this.interestRate = interestRate;
        }

        public void ApplyInterest()
        {
            var interest = balance * interestRate / 100;
            balance += interest;
            Console.WriteLine($"Interest applied: {interest}. New balance: {balance}");
        }

        public override void DisplayBalance()
        {
            base.DisplayBalance();
            Console.WriteLine($"Interest rate: {interestRate}%");
        }
    }
    public class CheckingAccount : Account
    {
        private decimal transactionFee;

        public CheckingAccount(string owner, decimal initialBalance, decimal transactionFee)
            : base(owner, initialBalance)
        {
            this.transactionFee = transactionFee;
        }

        public override void Deposit(decimal amount)
        {
            balance += amount - transactionFee;
            Console.WriteLine($"Deposited {amount}, transaction fee {transactionFee}. New balance: {balance}");
        }

        public override bool Withdraw(decimal amount)
        {
            if (amount + transactionFee <= balance)
            {
                balance -= (amount + transactionFee);
                Console.WriteLine($"Withdrew {amount}, transaction fee {transactionFee}. Remaining balance: {balance}");
                return true;
            }
            Console.WriteLine("Not enough funds to cover the transaction and fees.");
            return false;
        }
    }


    class Zadanie1
    {
        static void Main(string[] args)
        {
            SavingsAccount savings = new SavingsAccount("John Doe", 1000, 5);
            savings.Deposit(200);
            savings.ApplyInterest();
            savings.DisplayBalance();

            CheckingAccount checking = new CheckingAccount("Jane Doe", 500, 2);
            checking.Deposit(100);
            checking.Withdraw(50);
            checking.Withdraw(600);
        }
    }

}
