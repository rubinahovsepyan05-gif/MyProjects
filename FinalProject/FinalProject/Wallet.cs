namespace FinalProject;

public class Wallet
{
    public int Gold { get; private set; } = 0;

    public void Earn(int amount)
    {
        Gold += amount;
    }
    public void Spend(int amount)
    {
        Gold -= amount;
    }

    public void HandleEnemyKilled(Game sender, EnemyKilled e)
    {
        Earn(e.Enemy.Reward);
        Console.WriteLine($"[t{e.Tick}] +{e.Enemy.Reward} gold (gold: {Gold})");
    }
}