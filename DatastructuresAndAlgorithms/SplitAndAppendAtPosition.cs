using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatastructuresAndAlgorithms
{
    public static class SplitAndAppendAtPosition
    {
        public static void Invoke(string s = null, int? interval = null)
        {
            //if (string.IsNullOrEmpty(s))
            //{
            //    s = "abcdefghij";
            //    interval = 3;
            //}
            //Console.WriteLine("SplitAndAppendAtPosition: String = " + s + ". Interval =" + interval);

            //var builder = new StringBuilder();
            //int count = 0;
            //foreach (var c in s)
            //{
            //    builder.Append(c);
            //    if ((++count % interval) == 0)
            //    {
            //        builder.Append('-');
            //    }
            //}
            //Console.WriteLine("Before: {0}", s);
            //s = builder.ToString();
            //Console.WriteLine("After: {0}", s);




            string alpha = "abcdefghijklmnopqrstuvwxyz";
            string newAlpha = "";
            for (int i = 5; i < alpha.Length; i += 6)
            {
                newAlpha = alpha.Insert(i, "-");
            }

            Console.WriteLine("Before: {0}", alpha);
            Console.WriteLine("Before: {0}", newAlpha);
        }
    }
}