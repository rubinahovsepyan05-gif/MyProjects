namespace FinalProject;

public class Troll : Enemy
{
    public override int Speed { get; }
    public override int Reward { get; }
    public override int BaseDamage { get; }

    public Troll(int number):base($"Troll#{number}", 80)
    {
        Speed = 1;
        Reward = 20;
        BaseDamage = 3;
    }

    public override int TakeDamage(int amount)
    {
        int newAmount = amount - 2;
        return base.TakeDamage(newAmount);
    }

    public override void OnTick()
    {
        if (HP + 3 > 80)
        {
            HP = 80;
        }
        else
        {
            HP += 3;
        }
    }
}