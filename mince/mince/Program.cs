using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mince
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
        static void ReturnValues()
        {
            string[] coinStr = Console.ReadLine().Split(' ');
            int[] coin = new int[coinStr.Length];
            for (int i = 0; i < coinStr.Length; i++)
            {
                coin[i] = Int32.Parse(coinStr[i]);
            }
            
        }
    }
}
