namespace FinalProject;

public class EnemyKilled : EventArgs
{
    public Enemy Enemy { get; }
    public Tower Killer { get; }
    public int Tick { get; }

    public EnemyKilled(Enemy enemy, Tower killer, int tick)
    {
        Enemy = enemy;
        Killer = killer;
        Tick = tick;
    }
}