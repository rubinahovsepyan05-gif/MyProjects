namespace FinalProject;

public class ArrowTower : Tower
{
    public ArrowTower(Position position) : base(position)
    {
        
    }
    public override void Tick(Enemy[] enemies, Position[] path, Logger log)
    {
        int count = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy != null && enemy.IsAlive &&
                Position.DistanceTo(path[enemy.PathIndex]) <= 1)
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
                Position.DistanceTo(path[enemy.PathIndex]) <= 1)
            {
                inRange[index] = enemy;
                index++;
            }
        }

        Enemy? target = Targeting?.Invoke(inRange);

        if (target == null)
            return;

        int damage = target.TakeDamage(6);

        log($"Arrow{Position} hits {target.Name} " +
            $"for {damage} (HP: {target.HP})");
    }
    
    public override string ToString() => $"Arrow{Position}";
}