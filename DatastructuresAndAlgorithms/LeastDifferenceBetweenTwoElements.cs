using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatastructuresAndAlgorithms
{
    /// <summary>
    /// Find minimum difference between any two elements
    /// Given an unsorted array, find the minimum difference between any pair in given array.
    /// 
    /// Examples:
    /// Input: { 1, 5, 3, 19, 18, 25};
    /// Output: 1
    /// Minimum difference is between 18 and 19
    /// 
    /// Input: { 30, 5, 20, 9};
    /// Output: 4
    /// Minimum difference is between 5 and 9
    /// 
    /// Input: { 1, 19, -4, 31, 38, 25, 100};
    ///  Output: 5
    /// Minimum difference is between 1 and - 4
    /// </summary>
    public static class LeastDifferenceBetweenTwoElements
    {
        public static void Invoke(int[] array = null)
        {
            if (array == null || array.Count() == 0)
                array = new int[] { 1, 19, -4, 31, 38, 25, 100 };
            //O(n2) n square because of 2 loops.
            SolutionB(array);
        }

        /// <summary>
        /// O(n2) n square because of 2 loops.
        /// </summary>
        private static void SolutionA(int[] array)
        {

            int minDiff = int.MaxValue;

            for (int i = 0; i < array.Length - 1; i++)//need to check with 2nd last item
            {
                for (int j = (i + 1); j < array.Length; j++)// need to check with all items
                {
                    if (Math.Abs((array[i] - array[j])) < minDiff)
                        minDiff = Math.Abs((array[i] - array[j]));
                }
            }
            Console.WriteLine(minDiff);
        }

        /// <summary>
        /// Method 2 (Efficient: O(n Log n)
        /// The idea is to use sorting.Below are steps.
        /// 1) Sort array in ascending order.This step takes O(n Log n) time.
        /// 2) Initialize difference as infinite.This step takes O(1) time.
        /// 3) Compare all adjacent pairs in sorted array and keep track of minimum difference.This step takes O(n) time.
        /// </summary>
        private static void SolutionB(int[] array)
        {
            Array.Sort(array);
            int minDiff = int.MaxValue;
            for (int i = 0; i < array.Length - 1; i++)
            {
                if ((array[i + 1] - array[i]) < minDiff)
                    minDiff = Math.Abs((array[i] - array[+1]));
            }
            Console.WriteLine(minDiff);
        }
    }
}