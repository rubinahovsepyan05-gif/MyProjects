namespace FinalProject;

public class ConsoleLogger
{
    public void HandleEnemySpawned(Game sender, EnemySpawned e)
    {
        Console.WriteLine($"[t{e.Tick}] {e.Enemy.Name} spawned at  {e.Position}");
    }

    public void HandleEnemyKilled(Game sender, EnemyKilled e)
    {
        Console.WriteLine($"[t{e.Tick}] {e.Enemy.Name} killed by  {e.Killer.ToString()}");
    }

    public void HandleBaseDamaged(Game sender, BaseDamaged e)
    {
        Console.WriteLine($"[t{e.Tick}] {e.Enemy.Name} reached the base! Base  {e.BaseHP}");
    }

    public void HandleWaveCleared(Game sender, WaveCleared e)
    {
        Console.WriteLine($"[t{e.Tick}] Wave {e.WaveNumber} cleared");
    }

    public void HandleGameOver(Game sender, GameOver e)
    {
        Console.WriteLine($"=== Game Over: {(e.IsVictory ? "victory" : "defeat")}. {e.BaseHP}/10" +
                          $" ===");
    }
}