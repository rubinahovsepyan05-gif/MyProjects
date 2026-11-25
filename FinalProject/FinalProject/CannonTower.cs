namespace FinalProject;

public class CannonTower : Tower
{
    private int tickCounter = 0;
    public CannonTower(Position position) : base(position)
    {
        
    }
    public override void Tick(Enemy[] enemies, Position[] path, Logger log)
    {
        tickCounter++;

        if (tickCounter % 2 == 0)
            return;

        int count = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy != null && enemy.IsAlive &&
                Position.DistanceTo(path[enemy.PathIndex]) <= 2)
            {
                count++;
            }
        }

        Enemy[] inRange = new Enemy[count];
        int index = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy != null && enemy.IsAlive &&
                Position.DistanceTo(path[enemy.PathIndex]) <= 2)
            {
                inRange[index] = enemy;
                index++;
            }
        }

        Enemy? target = Targeting?.Invoke(inRange);

        if (target == null)
            return;

        Position targetPosition = path[target.PathIndex];
        int damage = target.TakeDamage(10);

        log($"Cannon{Position} hits {target.Name} " +
            $"for {damage} (HP: {target.HP})");

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy == null || !enemy.IsAlive || enemy == target)
                continue;

            int splashDistance =
                targetPosition.DistanceTo(path[enemy.PathIndex]);

            if (splashDistance == 1)
            {
                int splashDamage = enemy.TakeDamage(4);

                log($"Cannon{Position} splash {enemy.Name} " +
                    $"for {splashDamage} (HP: {enemy.HP})");
            }
        }
    }
    
}