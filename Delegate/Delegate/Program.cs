namespace Delegate;

class Program
{
    //Task a
    static int[] Filter(int[] items, IntCheck check)
    {
        int count = 0;
        int[] result;
        
        for (int i = 0; i < items.Length; i++)
        {
            if (check(items[i]))
            {
                count++;
            }
        }
        result = new int[count];
        count = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (check(items[i]))
            {
                result[count++] = items[i];
            }
        }
        return result;
    }

    static int[] Map(int[] items, IntMap map)
    {
        int[] result = new int[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            result[i] = map(items[i]);
        }
        return result;
    }

    static int Reduce(int[] items, int seed, IntStep step)
    {
        int acc = seed;
        for (int i = 0; i < items.Length; i++)
        {
            acc = step(acc, items[i]);
        }
        return acc;
    }

    static string[] FilterText(string[] items, TextCheck check)
    {
        int count = 0;
        string[] result;
        for (int i = 0; i < items.Length; i++)
        {
            if (check(items[i]))
            {
                count++;
            }
        }
        result = new string[count];
        count = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (check(items[i]))
            {
                result[count++] = items[i];
            }
        }
        return result;  
    }

    static string ReduceText(string[] items, string seed, TextStep step)
    {
        string acc = seed;
        for (int i = 0; i < items.Length; i++)
        {
            acc = step(acc, items[i]);
        }
        return acc;
    }
    
    //Task b
    static TextMap Compose(params TextMap[] steps)
    {
        return s =>
        {
            foreach (TextMap step in steps)
                s = step(s);
            return s;
        };
    }

    static IntMap ComposeInt(params IntMap[] steps)
    {
        return i =>
        {
            foreach (IntMap step in steps)
                i = step(i);
            return i;
        };
    }
    
    //Task c
    static int Retry(IntJob job, int attempts, FailHandler onFail)
    {
        if (attempts <= 0)
            throw new ArgumentOutOfRangeException(nameof(attempts));
        for (int attempt = 1; attempt <= attempts; attempt++)
        {
            try
            {
                return job();
            }
            catch (Exception e)
            {
                onFail(attempt, e);
                
                if (attempt == attempts)    
                    throw;
            }
        }

        throw new InvalidOperationException("Attempt " + attempts + " failed");
    }
    
    static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        string[] words = { "apple", "kivi", "banana", "fig" };
        
        //Task a 
        int[] evens=Filter(numbers, n => n % 2 == 0);
        for (int i = 0; i < evens.Length; i++)
        {
            Console.Write(evens[i] + " ");
        }
        Console.WriteLine();
        Console.WriteLine();
        
        int[] squares = Map(evens, n => n * n);
        for (int i = 0; i < squares.Length; i++)
        {
            Console.Write(squares[i] + " ");
        }
        Console.WriteLine();
        Console.WriteLine();
        
        int sum = Reduce(squares, 0, (acc, n) => acc + n);
        Console.WriteLine("Sum of squares: " + sum);
        Console.WriteLine();
        
        string[] longWords = FilterText(words, w => w.Length > 3); 
        for (int i = 0; i < longWords.Length; i++)
        {
            Console.Write(longWords[i] + " ");
        }
        Console.WriteLine();
        Console.WriteLine();
        
        string initials = ReduceText(words, "", (acc, w) => acc + w[0]);
        Console.WriteLine("First letters: " + initials);
        Console.WriteLine();
        
        int biggest = Reduce(numbers, int.MinValue,
            (acc, n) => n > acc ? n : acc);
        Console.WriteLine(biggest); 
        Console.WriteLine();

        string longest = ReduceText(words, "",
            (acc, w) => w.Length > acc.Length ? w : acc);
        Console.WriteLine(longest); 
        Console.WriteLine();
        
        //Task b
        TextMap slug = Compose(
            s => s.Replace(' ', '-'),
            s => s.Trim(),
            s => s.ToLower());
        Console.WriteLine(slug(" Hello Big World "));
        Console.WriteLine();
        IntMap math = ComposeInt(
            x => x + 1,
            x => x * 10,
            x => x - 3);
        Console.WriteLine(math(4));
        Console.WriteLine();
        
        //Task c
        int calls = 0;

        int Flaky()
        {
            calls++;
            if (calls < 3)
                throw new TimeoutException("Timeout");

            return 42;
        }

        int result = Retry(
            Flaky,
            5,
            (n, ex) => Console.WriteLine(
                $"attempt {n} failed: {ex.Message}"));

        Console.WriteLine($"result: {result}");
        Console.WriteLine();
        
        calls = 0;

        try
        {
            int result1 = Retry(Flaky, 2,
                (n, ex) => Console.WriteLine(
                    $"attempt {n} failed: {ex.Message}"));

            Console.WriteLine($"result: {result1}");
        }
        catch (TimeoutException ex)
        {
            Console.WriteLine($"Final failure: {ex.Message}");
        }
        Console.WriteLine();
        
        try
        {
            Retry(Flaky, 0,
                (n, ex) => Console.WriteLine(ex.Message));
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("The number of attempts must be positive");
        }
        Console.WriteLine();
        
        //Task d
        Notify toLog = msg => Console.WriteLine($"[log] {msg}");
        Notify toEmail = msg => Console.WriteLine($"[email] {msg}");
        Notify toSms = msg => Console.WriteLine($"[sms] {msg}");
        
        Notify notify = toLog;
        notify += toEmail;
        notify += toSms;
        notify("Order #17 shipped");
        Console.WriteLine();
        
        notify -= msg => Console.WriteLine($"[email] {msg}"); //Հղվում է այլ հասցեի
        notify("Order #18 shipped");
    }
    
    //Task a,b
    public delegate bool IntCheck(int n);
    public delegate int IntMap(int n);
    public delegate int IntStep(int acc, int n);
    
    public delegate bool TextCheck(string s);
    public delegate string TextMap(string s);
    public delegate string TextStep(string acc, string s);
    
    //Task c
    public delegate int IntJob();
    public delegate void FailHandler(int attempt, Exception ex);
    
    //Task d
    public delegate void Notify(string msg);
}