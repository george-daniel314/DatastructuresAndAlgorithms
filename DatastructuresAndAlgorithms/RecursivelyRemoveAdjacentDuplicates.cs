using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatastructuresAndAlgorithms
{
    /// <summary>
    /// 1. Initialize a stack containing the string. (Stack A)
    /// 2. Pop top most element of Stack A and push into Stack B thereby initializing Stack B
    /// 3. Pop from Stack B and pop from Stack A and check if they are equal.
    /// 4. If they aren't push both the elements in Stack B. (The popped element from Stack A is pushed later to preserve order)
    /// 5. Do step 3 till Stack A is empty.
    /// Stack B should contain the chracters with adjacent duplicates removed.
    /// </summary>
    public static class RecursivelyRemoveAdjacentDuplicates
    {
        public static void Invoke(string s = null)//ABBACCCA
        {
            if (string.IsNullOrEmpty(s))
            {
                s = "AABBACCCCA";

            }
            SolutionA(s);
            SolutionB(s);
        }

        private static void SolutionA(string s)
        {
            string[] patterns = new string[] { "AA", "BB", "CC" };
            RepeatLoop:
            foreach (var pattern in patterns)
            {
                if (s.Contains(pattern))
                {
                    s = s.Replace(pattern, "");
                    goto RepeatLoop;
                }
            }
            Console.WriteLine(s);
        }

        private static void SolutionB(string s)
        {
            Stack<char> stackA = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                stackA.Push(s[i]);
            }
            Stack<char> stackB = new Stack<char>(stackA.Count);

            stackB.Push(stackA.Pop()); //first A

            while (stackA.Count != 0)
            {
                if (stackB.Count == 0)
                    stackB.Push(stackA.Pop());
                char a = stackA.Pop();
                char b = stackB.Pop();

                if (a != b)
                {
                    stackB.Push(b);
                    stackB.Push(a);
                }
            }
            Console.WriteLine(string.Join("", stackB.ToArray()));
        }
    }
}