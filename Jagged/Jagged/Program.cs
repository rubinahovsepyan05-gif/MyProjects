namespace Jagged;

class Program
{
    static void Main(string[] args)
    {
        //Task a
        int[][] results = Pascal(5);
        for (int i = 0; i < results.Length; i++)
        {
            for (int j = 0; j < results[i].Length; j++)
            {
                Console.Write(results[i][j] + " ");
            }
            Console.WriteLine();
        }
        
        //Task b
        int[][] mountain =
        {
            new[] { 2 },
            new[] { 3, 4 },
            new[] {6, 5, 7},
            new[] { 4, 1, 8, 3 }
        };
        int minpath = MinPathDown(mountain);
        Console.WriteLine("Min Path sum: " + minpath);

        int[] result = MinPath(mountain);
        for (int i = 0; i < result.Length; i++)
        {
            Console.Write(result[i] + " ");
        }
        Console.WriteLine();
        
        //Task c
        string[] students = { "Anna", "Bob", "Carl", "Dina" };
        int[][] grades =
        {
            new[] { 90, 85, 77 },
            new[] { 60 },
            new int[0],
            new[] { 100, 95, 98, 91 },
        };
        AverageGrade(grades, students);
        
        //additional
        int[][] sorted =
        {
            new[] { 1, 2, 4 },
            new[] { 3, 5, 9, 11 },
            new[] { 7, 8 },
            new[] { 6, 9, 10 },
        };
        int[] final=MergeSortedArrays(sorted);
        for(int i = 0; i < final.Length; i++)
        {
            Console.Write(final[i] + " ");
        }
        Console.WriteLine();
    }

    //Task a
    static int[][] Pascal(int rows)
    {
        int[][] pascal = new int[rows][];
        for (int i = 0; i < rows ; i++)
        {
            pascal[i] = new int[i + 1];
            pascal[i][0] = 1; 
            pascal[i][i] = 1;
            for (int j = 1; j < i ; j++)
            {
                pascal[i][j] = pascal[i - 1][j - 1] + pascal[i - 1][j];
            }
        }
        return pascal;
    }
    
    //Task b
    static int MinPathDown(int[][] m)
    {
        int[][] sums = Sums(m);
        return sums[0][0];
    }

    static int[] MinPath(int[][] m)
    {
        int length = m.Length;
        int[] result = new int[length];
        int[][] sums = Sums(m);

        int min = 0;
        result[0] = m[min][min];
        for (int i = 1; i < length; i++)
        {
            if (sums[i][min] < sums[i][min + 1])
            {
                result[i] = m[i][min];
            }
            else
            {
                result[i] = m[i][min + 1];
                min++;
            }
        }

        return result;
    }
    
    static int[][] Sums(int[][] m)
    {
        int length = m.Length;
        int[][] sums = new int[length][];
        for (int i = 0; i < length; i++)
        {
            sums[i] = new int[m[i].Length];
        }

        for (int j = 0; j < sums[length - 1].Length; j++)
        {
            sums[length - 1][j] = m[length - 1][j];
        }

        for (int i = length - 2 ; i >= 0; i--)
        {
            for (int j = 0; j < sums[i].Length; j++)
            {
                sums[i][j] = m[i][j] + Math.Min(sums[i + 1][j], sums[i + 1][j + 1]);
            }
        }
        return  sums;
    }
    
    //Task c
    static void AverageGrade(int[][] grades, string[] students)
    {
        if (students.Length != grades.Length) 
            throw new ArgumentException("The number of students does not match the number of grades");

        double[] average = new double[students.Length];
        int max;
        for (int i = 0; i < grades.Length; i++)
        {
            int sum = 0;
            Console.Write(students[i] + ": ");
            for (int j = 0; j < grades[i].Length; j++)
            {
                sum += grades[i][j];
            }

            if (grades[i].Length != 0)
            {
                average[i] = (double)sum / grades[i].Length;
                Console.WriteLine($"{average[i]:F2}");
            }
            else
            {
                average[i] = 0;
                Console.WriteLine("No grades");
            }
        }

        max = -1;
        for (int i = 1; i < average.Length; i++)
        {
            if (grades[i].Length > 0 &&
                (max == -1 || average[max] < average[i]))
            {
                max = i;
            }
        }
        if (max != -1)
            Console.WriteLine($"Best: {students[max]} ({average[max]:F2})");
    }

    //Additional
    static int[] MergeSortedArrays(int[][] jagged)
    {
        int[] indexes = new int[jagged.Length];
        
        int length = 0;
        for (int i = 0; i < jagged.Length; i++)
        {
            indexes[i] = 0;
            length += jagged[i].Length;
        }

        int[] result = new int[length];

        for (int i = 0; i < result.Length; i++)
        {
            int minRow = -1;
            for (int j = 0; j < jagged.Length ; j++)
            {
                if (indexes[j] >= jagged[j].Length)
                {
                    continue;
                }

                if (minRow == -1)
                {
                    minRow = j;
                }
                else
                {
                    if (jagged[j][indexes[j]] < jagged[minRow][indexes[minRow]])
                    {
                        minRow = j;
                    }
                }
            }
            result[i] = jagged[minRow][indexes[minRow]];
            indexes[minRow]++;
        }
        return result;
    }
}