namespace SOLID_Fundamentals
{
    public abstract class Account
    {
        public decimal Balance { get; protected set; }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            Balance += amount;
        }

        public virtual decimal CalculateInterest()
        {
            return Balance * 0.01m;
        }
    }

    public abstract class BasicAccoubt : Account, IAccount
    {
        public abstract void Withdraw(decimal amount);
    }

    public class SavingAccount : BasicAccoubt
    {
        public decimal MinimumBalance { get; } = 100m;

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (Balance - amount < MinimumBalance)
            {
                throw new InvalidOperationException("Cannot go below minimum balance");
            }
            Balance -= amount;
        }

    }

    public class CheckingAccount : BasicAccoubt
    {
        public decimal OverdraftLimit { get; } = 500m;

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
                if (Balance - amount < -OverdraftLimit)
            {
                throw new InvalidOperationException("Overdraft limit exceeded");
            }
            Balance -= amount;
        }
    }

    public abstract class TimedAccount : Account, IAccount 
    {
        private DateTime _maturityDate;
        public DateTime MaturityDate
        {
            get { return _maturityDate; }
            protected set { _maturityDate = value; }
        }
        public bool IsMature
        {
            get
            {
                return DateTime.Now >= MaturityDate;
            }
        }

        public abstract void Withdraw(decimal amount);

    }
    public class FixedDepositAccount : TimedAccount
    {
        public FixedDepositAccount(decimal deposit, DateTime maturityDate)
        {
            if (deposit <= 0) throw new ArgumentException("Initial deposit must be positive.");
            Balance = deposit;
            MaturityDate = maturityDate.Date;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive.");
            if (!IsMature)
            {
                throw new InvalidOperationException("Cannot withdraw before maturity date");
            }

            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            Balance -= amount;
        }

        public override decimal CalculateInterest()
        {
            return Balance * 0.05m;
        }
    }

    public interface IAccount
    {
        void Withdraw(decimal amount);
        void Deposit(decimal amount);
    }

    public class Bank
    {
        public void ProcessWithdrawal(IAccount account, decimal amount)
        {
            try
            {
                account.Withdraw(amount);
                Console.WriteLine($"Successfully withdrew {amount}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Withdrawal failed: {ex.Message}");
            }
        }

        public void Transfer(IAccount from, IAccount to, decimal amount)
        {
            try
            {
                from.Withdraw(amount);
                to.Deposit(amount);
                Console.WriteLine($"Transferred {amount:C} successfully");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Transfer failed: {ex.Message}");
            }
        }
    }
}
