namespace InheritancenPolymorphism;

public class Healer : Unit
{
    public Healer(string name, int maxHP) : base(name, maxHP)
    { }

    public override void Act(Unit[] allies, Unit[] enemies, Logger logger)
    {
        int[] percentages = new int[allies.Length];
        for (int i = 0; i < allies.Length; i++)
        {
            percentages[i] = allies[i].HP * 100 / allies[i].MaxHP;
        }

        int min = -1;
        for (int i = 0; i < allies.Length; i++)
        {
            if (!allies[i].IsAlive)
                continue;
            if (min == -1 || percentages[i] < percentages[min])
                min = i;
        }
        if (min == -1)
            return;

        if (allies[min].HP < allies[min].MaxHP)
        {
            int heal = allies[min].Heal(10);
            logger($"{Name} heals {allies[min].Name} for {heal} ({allies[min].Name} {allies[min].HP}/{allies[min].MaxHP})");
        }
        else
        {
            min = -1;
            for (int i = 0; i < enemies.Length; i++)
            {
                if (!enemies[i].IsAlive)
                    continue;
                if (min == -1 || enemies[i].HP < enemies[min].HP)
                    min = i;
            }
            if (min == -1)
                return;
            
            int damage = enemies[min].TakeDamage(4);
            string message =
                $"{Name} hits {enemies[min].Name} for {damage} " +
                $"({enemies[min].Name} {enemies[min].HP}/{enemies[min].MaxHP})";

            if (!enemies[min].IsAlive)
                message += $". {enemies[min].Name} is defeated!";

            logger(message);
            
        }
    }
}