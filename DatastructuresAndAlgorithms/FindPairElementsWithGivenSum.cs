using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatastructuresAndAlgorithms
{
    public static class FindPairElementsWithGivenSum
    {
        public static void Invoke(int[] array = null, int expectedSum = 0)
        {
            if (array == null || array.Length > 0)
            {
                array = new int[] { 5, -3, 4, -200, 275, 9 };
                expectedSum = 75;
            }


            //SolutionA( array,  expectedSum);
            SolutionB(array, expectedSum);
        }

        private static void SolutionA(int[] array, int expectedSum)
        {
            // Step 1 :- Sort the array in ascending manner
            Array.Sort(array);
            int left = 0;
            int right = array.Length - 1;
            // Step 2 :- Take two pointers one which moves from
            // the left of the array and the second from right of the array
            while (left < right)
            {

                int sum = array[left] + array[right];
                // Step 3 :- If sum is proper then add from the left
                // substract from the right
                if (sum == expectedSum)
                {
                    Console.WriteLine(array[left] + "  " + array[right]);
                    left = left + 1;
                    right = right - 1;
                }
                else if (sum < expectedSum)
                {
                    // Step 4 :- If target total is greater than
                    // the sum then advance from the left
                    left = left + 1;
                }
                else if (sum > expectedSum)
                {
                    // Step 5 :- If target total is less than
                    // the sum then substract from the right
                    right = right - 1;
                }
            }
            Console.ReadLine();
        }

        private static void SolutionB(int[] arr, int n)
        {
            HashSet<int> arrayItemList = new HashSet<int>();
            foreach (var item in arr)
            {
                int remainingvalue = n - item;
                if (arrayItemList.Contains(remainingvalue))
                {
                    Console.WriteLine($"({ item},{remainingvalue})");
                }
                else
                {
                    arrayItemList.Add(item);
                }
            }
        }
    }
}