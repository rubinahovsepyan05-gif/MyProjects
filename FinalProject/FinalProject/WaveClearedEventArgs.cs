namespace FinalProject;

public class WaveCleared : EventArgs
{
    public int WaveNumber { get; }
    public int Tick { get; }
    public WaveCleared(int waveNumber, int tick)
    {
        WaveNumber = waveNumber;
        Tick = tick;
    }
}