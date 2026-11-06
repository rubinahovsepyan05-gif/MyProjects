namespace Event;

public class Library
{
    public int count = 0;
    public delegate void Visit(string name);
    private event Visit visit;
    private event Visit once;
    
    public void Subscribe(Visit del)
    {
        visit += del;
    }

    public void Unsubscribe(Visit del)
    {
        visit -= del;
    }

    public void SubscribeOnce(Visit del)
    {
        once += del;
    }

    public void Enter(string name)
    {
        visit?.Invoke(name);
        once?.Invoke(name);
        once = null;
    }
    
    public void Chime(string name)
    {
        Console.WriteLine($"Chime {name}");
    }
    
    public void Desk(string name)
    {
        count++;
        Console.WriteLine($"{count} {name}");
    }

    public void Slip(string name)
    {
        Console.WriteLine($"Slip {name}");
    }

    public void Card(string name)
    {
        Console.WriteLine($"Card {name}");
    }
    
    public void Hold(string name)
    {
        Console.WriteLine($"Hold {name}");
    }
}