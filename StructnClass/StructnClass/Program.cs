namespace StructnClass;

class Program
{
    static void Main(string[] args)
    {
        var a = new Money(10.25m, "USD");
        var b = new Money(2.25m, "USD");

        Console.WriteLine(a + b);   
        Console.WriteLine(a - b);   
        Console.WriteLine(a * 3m);  

        Console.WriteLine(a == new Money(10.25m, "USD")); 
        Console.WriteLine(a != b);                    
        
        Console.WriteLine(a > b); 
        Console.WriteLine(a < b); 

        Console.WriteLine(a.Equals(new Money(10.25m, "USD"))); 
        Console.WriteLine(a.Equals(b));                       

        var sameAsA = new Money(10.25m, "USD");

        Console.WriteLine(a.GetHashCode() == sameAsA.GetHashCode()); 
        
        try
        {
            var result = a + new Money(1m, "EUR");
            Console.WriteLine(result);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Currencies are different");
        }
        
        Money[] parts = new Money(100m, "USD").Allocate(3);

        foreach (Money part in parts)
        {
            Console.WriteLine(part);
        }

        try
        {
            new Money(100m, "USD").Allocate(0);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Parts must be greater than zero");
        }
    }
}
