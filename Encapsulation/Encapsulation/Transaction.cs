namespace Encapsulation;
public enum TransactionType
{
    Deposit,
    Withdraw,
    TransferIn,
    TransferOut
}
public class Transaction
{
    public DateTime At { get; }
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public decimal BalanceAfter { get; }

    public Transaction(DateTime at, TransactionType type, decimal amount, decimal balanceAfter)
    {
        At = at;
        Type = type;
        Amount = amount;
        BalanceAfter = balanceAfter;
    }
}