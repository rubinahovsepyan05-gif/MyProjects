namespace FinalProject;

public abstract class Enemy
{
    public string Name { get; }
    public int HP { get; protected set; }
    public int PathIndex { get; set; }
    public bool IsAlive => HP > 0;
    public abstract int Speed { get; }
    public abstract int Reward { get; }
    public abstract int BaseDamage { get; }
    
    protected Enemy(string name, int hp)
    {
        Name = name;
        HP = hp;
    }
    
    public virtual int TakeDamage(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (!IsAlive)
            return 0;

        int actualDamage = Math.Min(amount, HP);
        HP -= actualDamage;
        return actualDamage;
    }

    public virtual void OnTick()
    { }
}