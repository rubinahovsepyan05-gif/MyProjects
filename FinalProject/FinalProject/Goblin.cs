namespace FinalProject;

public class Goblin : Enemy
{
    public override int Speed { get; }
    public override int Reward { get; }
    public override int BaseDamage { get; }

    public Goblin(int number):base($"Goblin#{number}", 15)
    {
        Speed = 2;
        Reward = 3;
        BaseDamage = 1;
    }

}