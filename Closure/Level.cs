namespace Closure;

public class Level
{
    public int level;
    private Bonus bonus;

    public Level(Bonus bonus)
    {
        this.bonus = bonus;
    }
    public int Func(int n)
    {
        return n + level + bonus._bonus;
    }
}