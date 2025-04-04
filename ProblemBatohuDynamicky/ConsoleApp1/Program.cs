using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] weights = { 3, 1, 3, 4, 2 };
            int[] costs = { 2, 2, 4, 5, 3 };
            long capacity = 7;
            DynamicReturnValues(weights, costs, capacity);
            Console.ReadLine();
        }
        static void DynamicReturnValues(int[] we, int[] co, long ca)
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
            Console.WriteLine(table[we.Length,ca]);
            int x = 0;
            for (int i = 0; i < we.Length; i++)
            {
                if (table[we.Length - i, ca - x] > table[we.Length - i - 1, ca - x])
                {
                    Console.Write(we.Length - i + " ");
                    x = x + we[we.Length - i - 1];
                }
            }
        }
    }
}
