namespace FinalProject;

public readonly struct Position
{
    public int Row { get; }
    public int Col { get; }

    public Position(int row, int col)
    {
        Row = row;
        Col = col;
    }
    
    
    public int DistanceTo(Position other)
    {
        int distance = Math.Max(Math.Abs(Row - other.Row), Math.Abs(Col - other.Col));
        return distance;
    }

    public override string ToString() => $"({Row}, {Col})";
}