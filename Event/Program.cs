namespace Event;

class Program
{
    static void Main(string[] args)
    {
        Library lib = new Library();
        lib.Subscribe(lib.Chime);
        lib.Subscribe(lib.Desk);
        lib.SubscribeOnce(lib.Slip);
        lib.SubscribeOnce(lib.Card);
        Console.WriteLine();
        lib.Enter("Ana");
        Console.WriteLine();
        lib.Enter("Leo");
        Console.WriteLine();
        lib.Unsubscribe(lib.Desk);
        lib.SubscribeOnce(lib.Hold);
        lib.Enter("Mia");
    }
}