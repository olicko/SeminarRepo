using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine();
            return;
        }

        int[] array = Array.ConvertAll(input.Split(), int.Parse);
        int n = array.Length;

        if (n == 0)
        {
            Console.WriteLine();
            return;
        }

        int[] dp = new int[n];
        int[] prev = new int[n];
        Array.Fill(dp, 1);
        Array.Fill(prev, -1);

        int maxLen = 1;
        int maxIndex = 0;

        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (array[j] < array[i] && dp[j] + 1 > dp[i])
                {
                    dp[i] = dp[j] + 1;
                    prev[i] = j;
                }
            }

            if (dp[i] > maxLen)
            {
                maxLen = dp[i];
                maxIndex = i;
            }
        }

        List<int> lis = new List<int>();
        int current = maxIndex;
        while (current != -1)
        {
            lis.Add(array[current]);
            current = prev[current];
        }

        lis.Reverse();

        Console.WriteLine(string.Join(" ", lis));
    }
}
