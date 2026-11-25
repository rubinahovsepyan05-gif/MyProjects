namespace InheritancenPolymorphism;

public class Warrior : Unit
{
    public Warrior(string name,int maxHP) : base(name, maxHP)
    { }
    public override int TakeDamage(int amount)
    {
        amount = Math.Max(1, amount - 3);
        return base.TakeDamage(amount); 
    }

    public override void Act(Unit[] allies, Unit[] enemies, Logger logger)
    {
        int min = -1;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!enemies[i].IsAlive)
                continue;
            if (min == -1 || enemies[i].HP < enemies[min].HP)
                min = i;
        }

        if (min == -1)
            return;
        
        int damage = enemies[min].TakeDamage(12);
        string message =
            $"{Name} hits {enemies[min].Name} for {damage} " +
            $"({enemies[min].Name} {enemies[min].HP}/{enemies[min].MaxHP})";

        if (!enemies[min].IsAlive)
            message += $". {enemies[min].Name} is defeated!";

        logger(message);
    }
}