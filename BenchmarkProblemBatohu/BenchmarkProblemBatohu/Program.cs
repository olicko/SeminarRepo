using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections;

namespace BenchmarkKnapsack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<MyBenchmark>(); // spustíme benchmarky (vše, co je označeno [Benchmark])
        }
    }

    [MemoryDiagnoser]
    public class MyBenchmark
    {
        int[] weights;
        int[] costs;
        long capacity;

        public MyBenchmark()
        {
            Random rand = new Random();
            int size = rand.Next(5,15);
            weights = new int[size];
            costs = new int[size];
            capacity = rand.Next(20, 50);

            for (int i = 0; i < size; i++)
            {
                weights[i] = rand.Next(1, 20);
                costs[i] = rand.Next(10, 100);
            }
        }

        [Benchmark]
        public void Knapsack_Backtracking()
        {
            BacktrackingReturnValues(weights, costs, capacity);

            (int, List<int>) BacktrackingReturnValues(int[] we, int[] va, long ca)
    {
                int maxValue = 0;
                List<int> bestItems = new List<int>();

                Stack<(int index, int weight, int value, List<int> items)> stack = new();
                stack.Push((0, 0, 0, new List<int>()));

                while (stack.Count > 0)
                {
                    var (index, currWe, currVa, currItems) = stack.Pop();

                    if (currWe > ca || index > we.Length)
                        continue;

                    if (currVa > maxValue)
                    {
                        maxValue = currVa;
                        bestItems = new List<int>(currItems);
                    }

                    if (index == we.Length)
                        continue;

                    stack.Push((index + 1, currWe, currVa, new List<int>(currItems)));

                    int nextWe = currWe + we[index];
                    if (nextWe <= ca)
                    {
                        List<int> nextItems = new List<int>(currItems) { index + 1 };
                        int nextVa = currVa + va[index];
                        stack.Push((index + 1, nextWe, nextVa, nextItems));
                    }
                }

                return (maxValue, bestItems);
            }
        }

        [Benchmark]
        public void Knapsack_DynamicProgramming()
        {
            DynamicReturnValues(weights, costs, capacity);

            void DynamicReturnValues(int[] we, int[] co, long ca)
            {
                int[,] table = new int[we.Length + 1, ca + 1];
                for (int i = 0; i < we.Length; i++)
                {
                    for (int j = 0; j < ca + 1; j++)
                    {
                        if (we[i] <= j && table[i, j - we[i]] + co[i] >= table[i, j])
                        {
                            table[i + 1, j] = table[i, j - we[i]] + co[i];
                        }
                        else
                        {
                            table[i + 1, j] = table[i, j];
                        }
                    }
                }
                int x = 0;
                for (int i = 0; i < we.Length; i++)
                {
                    if (table[we.Length - i, ca - x] > table[we.Length - i - 1, ca - x])
                    {
                        x = x + we[we.Length - i - 1];
                    }
                }
            }
        }
    }
}
