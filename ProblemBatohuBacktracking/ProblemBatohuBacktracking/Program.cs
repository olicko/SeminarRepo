class Program
{
    static void Main()
    {
        int[] weights = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] values = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int capacity = int.Parse(Console.ReadLine());

        (int maxValue, List<int> bestItems) = BacktrackingReturnValues(weights, values, capacity);

        Console.WriteLine(maxValue);
        Console.WriteLine(string.Join(" ", bestItems));
    }

    static (int, List<int>) BacktrackingReturnValues(int[] we, int[] va, int ca)
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