namespace FinalProject;

public abstract class Tower
{
    public Position Position { get; }
    public TargetPicker Targeting { get; set; }
    
    protected Tower(Position position)
    {
        Position = position;
    }
    
    public abstract void Tick(Enemy[] enemies, Position[] path, Logger log);
}