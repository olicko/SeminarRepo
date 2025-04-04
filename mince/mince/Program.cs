using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mince
{
    internal class Program
    {
        static void Main()
        {
            int[] coins = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int target = int.Parse(Console.ReadLine());

            List<List<int>> results = new List<List<int>>();
            List<int> current = new List<int>();

            Backtrack(coins, target, 0, current, results);

            foreach (var i in results)
            {
                Console.WriteLine(string.Join(" ", i));
            }
            Console.ReadLine();
        }

        static void Backtrack(int[] coins, int remaining, int index, List<int> current, List<List<int>> results)
        {
            if (remaining == 0)
            {
                results.Add(new List<int>(current));
                return;
            }

            for (int i = index; i < coins.Length; i++)
            {
                int coin = coins[i];
                if (coin <= remaining)
                {
                    current.Add(coin);
                    Backtrack(coins, remaining - coin, i, current, results);
                    current.RemoveAt(current.Count - 1);
                }
            }
        }
    }
}
