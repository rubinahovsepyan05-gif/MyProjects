namespace InheritancenPolymorphism;

public sealed class Berserker : Warrior
{
    public Berserker(string name, int maxHP) : base(name, maxHP)
    { }
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

        int amount = 12 + (MaxHP - HP) / 5;
        int damage = enemies[min].TakeDamage(amount);
        string message =
            $"{Name} hits {enemies[min].Name} for {damage} " +
            $"({enemies[min].Name} {enemies[min].HP}/{enemies[min].MaxHP})";

        if (!enemies[min].IsAlive)
            message += $". {enemies[min].Name} is defeated!";

        logger(message);
    }
}