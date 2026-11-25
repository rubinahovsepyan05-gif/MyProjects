namespace InheritancenPolymorphism;

class Program
{
    private static Logger _logger = Console.WriteLine;
    static void Main(string[] args)
    {
        Unit[] light =
        {
            new Warrior("Arthur", 50),
            new Healer("Mira", 30)
        };

        Unit[] dark =
        {
            new Mage("Zed", 28),
            new Warrior("Brok", 45)
        };

        Battle(light, dark);
    }

    static void Battle(Unit[] light, Unit[] dark)
    {
        int round = 1;
        while (Alive(light) && Alive(dark))
        {
            Console.WriteLine($"--- Round {round++} ---");
            for (int i = 0; i < light.Length && Alive(dark); i++)
            {
                if (light[i].IsAlive)
                    light[i].Act(light, dark, _logger);
            }
            if(!Alive(dark))
                break;
            for (int i = 0; i < dark.Length && Alive(light); i++)
            {
                if (dark[i].IsAlive)
                    dark[i].Act(dark, light, _logger);
            }
        }
        if (Alive(light))
            Console.WriteLine("Light wins!");
        else if (Alive(dark))
            Console.WriteLine("Dark wins!");
        else
            Console.WriteLine("Draw!");
    }

    public static bool Alive(Unit[] units)
    {
        int count = 0;
        foreach (var unit in units)
        {
            if (unit.IsAlive)
            {
                count++;
            }
        }

        if (count > 0)
            return true;
        return false;
    }
}