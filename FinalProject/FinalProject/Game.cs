namespace FinalProject;

public delegate void EnemySpawnedHandler(Game sender, EnemySpawned e);

public delegate void EnemyKilledHandler(Game sender, EnemyKilled e);

public delegate void BaseDamageHandler(Game sender, BaseDamaged e);

public delegate void WaveClearedHandler(Game sender, WaveCleared e);

public delegate void GameOverHandler(Game sender, GameOver e);

public class Game
{
    public event EnemySpawnedHandler? EnemySpawned;
    public event EnemyKilledHandler? EnemyKilled;
    public event BaseDamageHandler? BaseDamaged;
    public event WaveClearedHandler? WaveCleared;
    public event GameOverHandler? GameOver;
    
    private void OnEnemySpawned(EnemySpawned e)
    {
        EnemySpawned?.Invoke(this, e);
    }

    private void OnEnemyKilled(EnemyKilled e)
    {
        EnemyKilled?.Invoke(this, e);
    }

    public void OnBaseDamaged(BaseDamaged e)
    {
        BaseDamaged?.Invoke(this, e);
    }

    public void OnWaveCleared(WaveCleared e)
    {
        WaveCleared?.Invoke(this, e);
    }

    public void OnGameOver(GameOver e)
    {
        GameOver.Invoke(this, e);
    }
    public void Run(TargetPicker targetPicker)
    {
        Logger logger = Console.WriteLine;
        char[,] map =
        {
            { 'S', '#', '#', '.', '.', '.' },
            { '.', '.', '#', '.', 'A', '.' },
            { '.', 'C', '#', '#', '#', '.' },
            { '.', '.', '.', '.', '#', 'B' },
        };

        string[][] waves =
        {
            new[] { "orc", "goblin", "orc" },
            new[] { "goblin", "goblin", "goblin", "goblin", "goblin" },
            new[] { "troll", "orc" },
        };
        
         
        int baseHP = 10;
        int tickCounter = 0;
        bool isVictory;
        for (int wave = 0; wave < waves.Length; wave++)
        {
            int length = waves[wave].Length;
            
            Enemy[] enemies = new Enemy[length];
            Tower[] towers = FindTowers(map);

            for (int i = 0; i < towers.Length; i++)
            {
                towers[i].Targeting = targetPicker;
            }
            
            int count = 0;
            Position[] path = FindPath(map);
            
            bool[] alives = new bool[length];
            
            while (true)
            {
                tickCounter++;
                if (count < length)
                {
                    enemies[count] = CreateEnemy(waves[wave][count],count + 1);
                    EnemySpawned enemySpawned=new EnemySpawned(enemies[count],posS(map),tickCounter);
                    OnEnemySpawned(enemySpawned);
                    count++;
                }
                
                for (int tower = 0; tower < towers.Length; tower++)
                {
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i] !=  null && enemies[i].IsAlive)
                        {
                            alives[i] = true;
                        }
                        else
                        {
                            alives[i] = false;
                        }
                    }
                    towers[tower].Tick(enemies,path, logger);
                    for (int i = 0; i < enemies.Length; i++)
                    {
                        if (enemies[i] != null && !enemies[i].IsAlive && alives[i] == true) 
                        {
                            EnemyKilled enemyKilled=new EnemyKilled(enemies[i],towers[tower],tickCounter);
                            OnEnemyKilled(enemyKilled);
                        }
                    }
                }
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i] != null && enemies[i].IsAlive)
                    {
                        enemies[i].OnTick();
                        enemies[i].PathIndex += enemies[i].Speed;
                        if (enemies[i].PathIndex >= path.Length - 1)
                        {
                            baseHP -= enemies[i].BaseDamage;
                            BaseDamaged baseDamaged = new BaseDamaged(enemies[i], baseHP, tickCounter);
                            OnBaseDamaged(baseDamaged);
                            enemies[i] = null;
                        }
                    }
                }
                if (baseHP <= 0)
                    break;
                int dead = 0;
                for (int i = 0; i < enemies.Length; i++)
                {
                    if(enemies[i] == null || !enemies[i].IsAlive)
                        dead++;
                }

                if (count == length && dead == enemies.Length)
                {
                    WaveCleared waveCleared = new WaveCleared(wave + 1, tickCounter);
                    OnWaveCleared(waveCleared);
                    break;
                }
            }
            if (baseHP <= 0)
                break;
        }

        if (baseHP <= 0)
        {
            isVictory = false;
        }
        else
        {
            isVictory = true;
        }

        GameOver gameOver = new GameOver(isVictory, baseHP, tickCounter);
        OnGameOver(gameOver);
    }

    static Position posS(char[,] map)
    {
        Position pos = new Position(0, 0);
        int rows= map.GetLength(0);
        int cols = map.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == 'S')
                {
                    pos = new Position(i, j);
                }
            }
        }
        return pos;
    }
    static Position[] FindPath(char[,] map)
    {
        int count = 0;
        int z = 0;
        
        int rows= map.GetLength(0);
        int cols = map.GetLength(1);
        
        Position pos = new Position(0, 0);
        Position[] positions;
        bool[,] visited = new bool[rows, cols];
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == '#')
                {
                    count++;
                }
            }
        }

        pos = posS(map);

        positions = new Position[count + 2];
        positions[z++] = new Position(pos.Row, pos.Col);
        
        for (int i = 0; i < count; i++)
        {
            if (pos.Row - 1 >= 0)
            {
                if (map[pos.Row - 1, pos.Col] == '#' && !visited[pos.Row - 1, pos.Col])
                {
                    visited[pos.Row - 1, pos.Col] = true;
                    positions[z] = new Position(pos.Row - 1, pos.Col);
                    pos = new Position(pos.Row - 1, pos.Col);
                    z++;
                    continue;
                }
            }
            if (pos.Row + 1 < rows)
            {
                if (map[pos.Row + 1, pos.Col] == '#' &&  !visited[pos.Row + 1, pos.Col])
                {
                    visited[pos.Row + 1, pos.Col] = true;
                    positions[z] = new Position(pos.Row + 1, pos.Col);
                    pos = new Position(pos.Row + 1, pos.Col);
                    z++;
                    continue;
                }
            }
            
            if (pos.Col + 1 < cols)
            {
                if (map[pos.Row , pos.Col + 1] == '#' && !visited[pos.Row , pos.Col + 1])
                {
                    visited[pos.Row , pos.Col + 1] = true;
                    positions[z] = new Position(pos.Row, pos.Col + 1);
                    pos = new Position(pos.Row, pos.Col + 1);
                    z++;
                    continue;
                }
            }
            if (pos.Col - 1 >= 0)
            {
                if (map[pos.Row , pos.Col - 1] == '#'  && !visited[pos.Row , pos.Col - 1])
                {
                    visited[pos.Row , pos.Col - 1] = true;
                    positions[z] = new Position(pos.Row, pos.Col - 1);
                    pos = new Position(pos.Row, pos.Col - 1);
                    z++;
                    continue;
                }
            }
        }
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == 'B')
                {
                    Position last = positions[z - 1];

                    int distance = Math.Abs(last.Row - i)
                                   + Math.Abs(last.Col - j);

                    if (distance != 1)
                        throw new InvalidOperationException("B is not next to the path.");

                    positions[z] = new Position(i, j);
                    break;
                }
            }
        }
        
        return positions;
    }

    static Tower[] FindTowers(char[,] map)
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        int count = 0;
        Tower[] towers;
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == 'A' || map[i, j] == 'C')
                    count++;
            }
        }
        
        towers = new Tower[count];
        count = 0;
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] == 'A')
                {
                    towers[count] = new ArrowTower(new Position(i, j));
                    count++;
                }

                if (map[i, j] == 'C')
                {
                    towers[count] = new CannonTower(new Position(i, j));
                    count++;
                }
            }
        }
        return towers;
    }

    static Enemy CreateEnemy(string name, int number)
    {
        if (name.ToLower() == "orc")
        {
            return new Orcs(number);
        }
        else if (name.ToLower() == "troll")
        {
            return new Troll(number);
        }
        else if (name.ToLower() == "goblin")
        {
            return new Goblin(number);
        }
        else
        {
            throw new ArgumentException();
        }
    }
}