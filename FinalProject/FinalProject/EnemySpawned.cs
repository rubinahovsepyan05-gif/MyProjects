namespace FinalProject;

public class EnemySpawned : EventArgs
{
    public Enemy Enemy { get; }
    public Position Position { get; }
    public int Tick { get; }

    public EnemySpawned(Enemy enemy, Position position, int tick)
    {
        Enemy = enemy;
        Position = position;
        Tick = tick;
    }
}