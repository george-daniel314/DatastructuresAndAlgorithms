using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatastructuresAndAlgorithms
{
    public static class ReverseStringUsingStack
    {
        public static void Invoke(string s = null)
        {
            if (string.IsNullOrEmpty(s))
            {
                s = "ABC";
            }

            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                stack.Push(s[i]);
            }
            var count = stack.Count;

            string reversedString = "";
            for (int i = 0; i < count; i++)
            {
                reversedString += stack.Pop();
            }

            Console.WriteLine(reversedString);
        }
    }
}