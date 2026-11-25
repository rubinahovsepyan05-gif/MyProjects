namespace FinalProject;

public class BaseDamaged : EventArgs
{
    public Enemy Enemy { get; }
    public int BaseHP { get; }
    public int Tick { get; }

    public BaseDamaged(Enemy enemy, int baseHP, int tick)
    {
        Enemy = enemy;
        BaseHP = baseHP;
        Tick = tick;
    }
}