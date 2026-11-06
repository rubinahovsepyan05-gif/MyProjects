namespace Closure;

class Program
{
    static void Main(string[] args)
    {
        int bonus = 5;
        Bonus b = new Bonus(bonus);
        Level[] lvl = new Level[3];
        int num = 0;
        while (num < lvl.Length)
        {
            lvl[num] = new Level(b);
            lvl[num].level = num;
            num++;
        }

        int newBonus = 50;
        b._bonus = newBonus;
        
        for (int i = 0; i < lvl.Length; i++)
        {
            Console.WriteLine(lvl[i].Func(2));
        }
        Console.WriteLine(lvl[0].Func(0));
        
    }
}