namespace InheritancenPolymorphism;

public class Mage : Unit
{
    public Mage(string name, int maxHP) : base(name, maxHP)
    { }

    public override int TakeDamage(int amount)
    {
        amount = amount * 3 / 2;
        return base.TakeDamage(amount);
    }

    public override void Act(Unit[] allies, Unit[] enemies, Logger logger)
    {
        string result = "";
        string defeated = "";

        foreach (var enemy in enemies)
        {
            if (!enemy.IsAlive)
                continue;

            int lost = enemy.TakeDamage(6);
            if (result != "")
                result += ", ";
            result += $"{enemy.Name} -{lost} ({enemy.HP}/{enemy.MaxHP})";

            if (!enemy.IsAlive)
                defeated += $" {enemy.Name} is defeated!";
        }

        if (result == "")
            return;

        string message = $"{Name} casts Fireball: {result}";
        if (defeated != "")
            message += "." + defeated;

        logger(message);
    }
}