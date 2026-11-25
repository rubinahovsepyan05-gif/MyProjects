namespace Encapsulation;

public class BankAccount
{
    private static int counter;
    private readonly decimal overdraftLimit;
    private bool isFrozen;
    public string Owner { get; }
    public string Id { get; }
    public decimal Balance { get; private set; }

    private Transaction[] history = new Transaction[4];
    private int historyCount;

    public BankAccount(string owner, decimal overdraftLimit = 0)
    {
        if (string.IsNullOrWhiteSpace(owner))
            throw new ArgumentException();
        Owner = owner;
        this.overdraftLimit = overdraftLimit;
        counter++;
        Id = $"ACC-{counter:D4}";
    }

    private void AddTransaction(Transaction transaction)
    {
        if (historyCount == history.Length)
        {
            Transaction[] newHistory = new Transaction[history.Length * 2];
            for (int i = 0; i < historyCount; i++)
            {
                newHistory[i] = history[i];
            }
            history = newHistory;
        }

        history[historyCount] = transaction;
        historyCount++;
    }

    public Transaction[] GetHistory()
    {
        Transaction[] result = new Transaction[historyCount];
        for (int i = 0; i < historyCount; i++)
        {
            result[i] = history[i];
        }
        return result;
    }

    public void PrintStatement()
    {
        Console.WriteLine($"{Id} {Owner} balance {Balance:F2}");
        for (int i = 0; i < historyCount; i++)
        {
            Console.WriteLine($"{history[i].Type} {history[i].Amount:F2} -> {history[i].BalanceAfter:F2}");
        }
    }

    public void Freeze()
    {
        isFrozen = true;
    }

    public void Unfreeze()
    {
        isFrozen = false;
    }
    public void Deposit(decimal amount)
    {
        if (isFrozen)
            throw new InvalidOperationException("Account is frozen.");
        if (amount <= 0)
            throw new ArgumentException();
        Balance += amount;
        AddTransaction(new Transaction(DateTime.Now, TransactionType.Deposit, amount,Balance));
    }

    public void Withdraw(decimal amount)
    {
        if (isFrozen)
            throw new InvalidOperationException("Account is frozen.");
        if(amount<=0)
            throw new ArgumentException();
        if (amount > Balance + overdraftLimit)
            throw new InsufficientFundsException(
                $"Not enough money on {Id}: balance {Balance:F2}, requested {amount:F2}"
            );
        Balance -= amount;
        AddTransaction(new Transaction(DateTime.Now, TransactionType.Withdraw, amount,Balance));
    }

    public void TransferTo(BankAccount other, decimal amount)
    {
        if (isFrozen)
            throw new InvalidOperationException("Account is frozen.");
        if (other.isFrozen)
            throw new InvalidOperationException("The receiving account is frozen.");
        if (other == null)
            throw new ArgumentNullException();
        if (other == this)
            throw new ArgumentException();
        if (other.isFrozen)
            throw new InvalidOperationException("The receiving account is frozen.");
        if (amount <= 0)
            throw new ArgumentException();
        if (amount > Balance + overdraftLimit)
            throw new InsufficientFundsException(
                $"Not enough money on {Id}: balance {Balance:F2}, requested {amount:F2}"
            );
        other.Balance += amount;
        Balance -= amount;
        AddTransaction(new Transaction(DateTime.Now, TransactionType.TransferOut, amount,Balance));
        other.AddTransaction(new Transaction(DateTime.Now, TransactionType.TransferIn, amount,other.Balance));
    }
}