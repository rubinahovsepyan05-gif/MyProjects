namespace FinalProject;

public class Achievements
{
    private int count = 0;

    public void HandleEnemyKilled(Game sender, EnemyKilled e)
    {
        if (e.Enemy is Goblin)
        {
            count++;
        }

        if (count == 3)
        {
            Console.WriteLine("[achievement] Goblin Slayer");
            sender.EnemyKilled -= HandleEnemyKilled;
        }
    }
}