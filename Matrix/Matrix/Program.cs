namespace Matrix;

class Program
{
    const int MaxPerSlot = 3;
    static void Main(string[] args)
    {
        
        string[] gates = { "A1", "A2", "B1", "B2" };
        string[] hours = { "06:00", "07:00", "08:00", "09:00", "10:00" };

        int[,] flights =
        {
            { 2, 3, 1, 0, 2 },
            { 1, 3, 3, 2, 0 },
            { 0, 2, 3, 1, 1 },
            { 3, 1, 0, 0, 2 },
        };
        
        //Task a
        Console.WriteLine("GateTotal " + GateTotal(flights, 1));
        Console.WriteLine("HourTotal " + HourTotal(flights,3));
        
        //Task b
        PrintBoard(flights, gates, hours);
        
        //Task c
        Console.WriteLine("Busiest Gate: " + BusiestGate(flights));
        Console.WriteLine("Busiest Hour: " + RushHour(flights));
        Console.WriteLine("Quietest Gate: " + QuietestHour(flights));
        
        //Task d
        string[] fullSlots = FullSlots(flights, gates, hours);
        for (int i = 0; i < fullSlots.Length; i++)
        {
            Console.Write(fullSlots[i] + " | ");
        }
        Console.WriteLine();
        string[] idleSlots = IdleSlots(flights, gates, hours);
        for (int i = 0; i < idleSlots.Length; i++)
        {
            Console.Write(idleSlots[i] + " | ");
        }
        Console.WriteLine();
        
        //Task e
        string assign=AssignFlight(flights, gates, 1);
        Console.WriteLine(assign);
        string assign1=AssignFlight(flights, gates, 1);
        Console.WriteLine(assign1);
        string assign2=AssignFlight(flights, gates, 1);
        Console.WriteLine(assign2);
        string assign3=AssignFlight(flights, gates, 1);
        Console.WriteLine(assign3);
        Console.WriteLine("HourTotal " + HourTotal(flights,1));
        
        int[,] result=Transpose(flights);
        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                Console.Write(result[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    //Task a
    static int GateTotal(int[,] flights, int gate)
    {
        int sum = 0;
        for (int i = 0; i < flights.GetLength(1); i++)
        {
            sum += flights[gate, i];
        }
        return sum;
    }

    static int HourTotal(int[,] flights, int hour)
    {
        int sum = 0;
        for (int i = 0; i < flights.GetLength(0); i++)
        {
            sum+=flights[i, hour];
        }
        return sum;
    }
    
    //Task b
    static void PrintBoard(int[,] flights, string[] gates, string[] hours)
    {
        if (gates.Length != flights.GetLength(0) ||
            hours.Length != flights.GetLength(1))
        {
            throw new ArgumentException("Array sizes do not match");
        }

        Console.Write($"{"",-6}");

        for (int j = 0; j < hours.Length; j++)
        {
            Console.Write($"{hours[j],7}");
        }
        Console.WriteLine(" | Total");

        int total = 0;

        for (int i = 0; i < flights.GetLength(0); i++)
        {
            Console.Write($"{gates[i],-6}");

            for (int j = 0; j < flights.GetLength(1); j++)
            {
                Console.Write($"{flights[i, j],7}");
            }

            int gateTotal = GateTotal(flights, i);
            Console.WriteLine($" | {gateTotal,5}");
            total += gateTotal;
        }

        Console.WriteLine(new string('-', 6 + hours.Length * 7 + 8));

        Console.Write($"{"Total",-6}");

        for (int j = 0; j < hours.Length; j++)
        {
            Console.Write($"{HourTotal(flights, j),7}");
        }

        Console.WriteLine($" | {total,5}");
    }
    
    //Task c
    static int BusiestGate(int[,] flights)
    {
        int best = 0;
        for (int i = 0; i < flights.GetLength(0); i++)
        {
            if (GateTotal(flights, best) < GateTotal(flights, i))
            {
                best = i;
            }
        }
        return best;
    }

    static int RushHour(int[,] flights)
    {
        int rushed = 0;
        for (int i = 0; i < flights.GetLength(1); i++)
        {
            if (HourTotal(flights, rushed) < HourTotal(flights, i))
            {
                rushed = i;
            }
        }
        return rushed;
    }

    static int QuietestHour(int[,] flights)
    {
        int quietest = 0;
        for (int i = 0; i < flights.GetLength(1); i++)
        {
            if (HourTotal(flights, quietest) > HourTotal(flights, i))
            {
                quietest = i;
            }
        }
        return quietest;
    }
    
    //Task d
    static string[] FullSlots(int[,] flights, string[] gates, string[] hours)
    {
        int count = 0;
        for (int i = 0; i < flights.GetLength(0); i++)
        {
            for (int j = 0; j < flights.GetLength(1); j++)
            {
                if (flights[i, j] == MaxPerSlot)
                {
                    count++;
                }
            }
        }
        string[] result = new string[count];
        count = 0;
        for (int i = 0; i < flights.GetLength(0); i++)
        {
            for (int j = 0; j < flights.GetLength(1); j++)
            {
                if (flights[i, j] == MaxPerSlot)
                {
                    result[count++] = gates[i] + " at " + hours[j];
                }
            }
        }
        return result;
    }
    
    static string[] IdleSlots(int[,] flights, string[] gates, string[] hours)
    {
        int count = 0;
        for (int i = 0; i < flights.GetLength(0); i++)
        {
            for (int j = 0; j < flights.GetLength(1); j++)
            {
                if (flights[i, j] == 0)
                {
                    count++;
                }
            }
        }
        string[] result = new string[count]; 
        count = 0;
        for (int i = 0; i < flights.GetLength(0); i++)
        {
            for (int j = 0; j < flights.GetLength(1); j++)
            {
                if (flights[i, j] == 0)
                {
                    result[count++] = gates[i] + " at " + hours[j];;
                }
            }
        }
        return result;
    }
    
    //Task e
    static string? AssignFlight(int[,] flights, string[] gates, int hour)
    {
        if (hour < 0 || hour >= flights.GetLength(1))
            throw new ArgumentOutOfRangeException(nameof(hour));
        
        int quietest = QuietestHour(flights);

        if (flights[quietest, hour] == 3)
        {
            for (int i = 0; i < flights.GetLength(0); i++)
            {
                if (flights[i, hour] != 3)
                {
                    flights[i, hour]++;
                    return gates[i];
                }
            }
        }
        else
        {
            flights[quietest, hour]++;
            return gates[quietest];
        }
        return null;
    }
    
    //Additional
    static int[,] Transpose(int[,] m)
    {
        int rows = m.GetLength(0);
        int cols = m.GetLength(1);
        int[,] result = new int[cols, rows];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[j, i] = m[i, j];
            }
        }
        return result;
    }
}