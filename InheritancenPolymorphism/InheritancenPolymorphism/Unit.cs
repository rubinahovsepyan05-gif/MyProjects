namespace InheritancenPolymorphism;

public delegate void Logger(string line);

public abstract class Unit
{
    public string Name { get; }
    public int MaxHP { get; }
    public int HP { get; protected set; }
    public bool IsAlive => HP > 0;
    
    protected Unit(string name, int maxHP)
    {
        Name = name;
        MaxHP = maxHP;
        HP = maxHP;
    }

    public virtual int TakeDamage(int amount)
    {
        if (!IsAlive)
            throw new InvalidOperationException("Unit is dead");
        if (HP - amount < 0)
        {
            amount = HP;
            HP = 0;
        }
        else
        {
            HP -= amount;
        }
        return amount;
    }

    public int Heal(int amount)
    {
        if (!IsAlive)
            throw new InvalidOperationException("Unit is dead");
        if (HP + amount > MaxHP)
        {
            amount = MaxHP - HP;
            HP = MaxHP;
        }
        else
        {
            HP += amount;
        }
        return amount;
    }

    public abstract void Act(Unit[] allies, Unit[] enemies, Logger logger);
}