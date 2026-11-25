namespace FinalProject;

public class Orcs : Enemy
{
    public override int Speed { get; }
    public override int Reward { get; }
    public override int BaseDamage { get; }

    public Orcs(int number) : base($"Orc#{number}", 30)
    {
        Speed = 1;
        Reward = 5;
        BaseDamage = 1;
    }
}