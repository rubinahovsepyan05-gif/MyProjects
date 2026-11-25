namespace FinalProject;

public delegate Enemy? TargetPicker(Enemy[] inRange);

public delegate void Logger(string line);

class Program
{
    static void Main(string[] args)
    {
        
        Game game = new Game();
        ConsoleLogger consoleLogger = new ConsoleLogger();
        Wallet wallet = new Wallet();
        Achievements achievements = new Achievements();
        TargetPicker targetPicker = First;
        /*targetPicker = Strongest;
        targetPicker = Weakest;*/
        

        game.EnemySpawned += consoleLogger.HandleEnemySpawned;
        game.EnemyKilled += consoleLogger.HandleEnemyKilled;
        game.EnemyKilled += wallet.HandleEnemyKilled;
        game.EnemyKilled += achievements.HandleEnemyKilled;
        game.BaseDamaged += consoleLogger.HandleBaseDamaged;
        game.WaveCleared += consoleLogger.HandleWaveCleared;
        game.GameOver += consoleLogger.HandleGameOver;
        game.Run(targetPicker);
        Console.WriteLine($"{wallet.Gold} gold");
    }

    public static Enemy? First(Enemy[] enemies)
    {
        if (enemies.Length == 0)
        {
            return null; 
        }
        int best = 0;
        for (int i = 1; i < enemies.Length; i++)
        {
            if (enemies[i].PathIndex > enemies[best].PathIndex)
            {

                best = i;
            }
        }
        return enemies[best];
    }

    public static Enemy? Strongest(Enemy[] enemies)
    {
        if (enemies.Length == 0)
        {
            return null; 
        }
        int strongest = 0;
        for (int i = 1; i < enemies.Length; i++)
        {
            if (enemies[i].HP > enemies[strongest].HP)
            {

                strongest = i;
            }
        }
        return enemies[strongest];
    }
    
    public static Enemy? Weakest(Enemy[] enemies)
    {
        if (enemies.Length == 0)
        {
            return null; 
        }
        int weakest = 0;
        for (int i = 1; i < enemies.Length; i++)
        {
            if (enemies[i].HP < enemies[weakest].HP)
            {

                weakest = i;
            }
        }
        return enemies[weakest];
    }
}
