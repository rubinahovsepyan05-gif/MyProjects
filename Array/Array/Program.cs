namespace Array;

class Program
{
    static void Main(string[] args)
    {
        //Task a
        int[] changes = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };
        
        int sum1 = BestStreak(changes, out int s, out int e);
        Console.WriteLine($"Start index: {s}\nEnd index: {e}\nMax sum: {sum1}");
        
        int sum2 = BestStreak(new[] { -3, -1, -2 }, out s, out e);
        Console.WriteLine($"Start index: {s}\nEnd index: {e}\nMax sum: {sum2}");

        //Task b
        int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
        RotateRight(ref arr, 3);
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();
        
        arr = new int[] { 1, 2, 3, 4, 5, 6, 7 };
        RotateRight(ref arr, 10);
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();
        
        //Task c
        int[] levelScores = { 73, 74, 75, 71, 69, 72, 76, 73 };
        int[] result = DaysUntillBetter(levelScores);
        for (int i = 0; i < result.Length; i++)
        {
            Console.Write(result[i] + " ");
        }
        Console.WriteLine(); 
        
        //Additional
        arr = new int[] { 1, 1, 2, 3, 3, 3, 4 };
        int n = RemoveDuplicate(arr);
        Console.WriteLine("Array's length without duplicates: " + n);
        for (int i = 0; i < n; i++)
        {
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();
    }

    //Task a
    static int BestStreak(int[] arr, out int start, out int end)
    {
        if (arr.Length == 0)
        {
            throw new ArgumentException("Array is empty");
        }

        int current = 0;
        int best = int.MinValue, currentstart = 0;
        start = 0;
        end = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            if (current < 0)
            {
                current = arr[i];
                currentstart = i;
            }
            else
            {
                current += arr[i];
            }

            if (current > best)
            {
                best = current;
                start = currentstart;
                end = i;
            }
        }
        return best;
    }

    //Task b
    static void RotateRight(ref int[] arr, int k)
    {
        if (k > arr.Length)
        {
            k %= arr.Length;
        }

        Reverse(arr, 0, arr.Length - 1);
        Reverse(arr, 0, k - 1);
        Reverse(arr, k, arr.Length - 1);
    }

    static void Reverse(int[] arr, int from, int to)
    {
        for (int i = from; i < to; i++)
        {
            int temp = arr[i];
            arr[i] = arr[to];
            arr[to] = temp;
            to--;
        }
    }
    
    //Task c
    static int[] DaysUntillBetter(int[] scores)
    {
        int[] result = new int[scores.Length];
        if (scores.Length == 0)
            return result;
        int[] stack = new int[scores.Length];
        int top = 0;
        stack[top] = 0;
        top++;
        for (int i = 1; i < scores.Length; i++)
        {
            while (top > 0 && scores[stack[top - 1]] < scores[i])
            {
                int day = stack[top - 1];
                result[day] = i - day;
                top--;

            }

            stack[top] = i;
            top++;
        }
        return result;
    }
    
    //Additional task
    static int RemoveDuplicate(int[] sorted)
    {
        int length = sorted.Length;
        for (int i = 0; i < length - 1; i++)
        {
            if (sorted[i] == sorted[i + 1])
            {
                for (int j = i + 1; j < length - 1; j++)
                {
                    sorted[j] = sorted[j + 1];
                }
                length--;
                i--;
            }
        }
        return length;
    }
}