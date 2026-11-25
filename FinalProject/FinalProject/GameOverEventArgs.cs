namespace FinalProject;

public class GameOver : EventArgs
{
    public bool IsVictory { get; }

    public int BaseHP { get; }
    public int Tick { get; }

    public GameOver(bool isVictory, int baseHP, int tick)
    {
        IsVictory = isVictory;
        BaseHP = baseHP;
        Tick = tick;
    }
}