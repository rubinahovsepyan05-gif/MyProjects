namespace Encapsulation;

class Program
{
    static void Main(string[] args)
    {
        var anna = new BankAccount("Anna");
        var ben = new BankAccount("Ben");
        anna.Deposit(500);
        anna.Withdraw(120);
        anna.TransferTo(ben, 300);
        try
        {
            anna.Withdraw(200);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"InsufficientFundsException: {ex.Message}");
        }
        anna.PrintStatement();
        ben.PrintStatement();
    }
}