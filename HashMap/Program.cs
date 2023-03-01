using System;
using System.IO;
namespace HashMap
{
    internal class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            HashMap<int,int> map = new HashMap<int,int>(100);
            int[] test = { 1, 4, 7, 7, 5, 2 };/*
            for(int i = 0; i < test.Length; i++)
            {
                if (map.includes(6 - test[i]))
                {
                    Console.WriteLine($"Solution is: {map.GetValue(6-test[i])},{i}" );
                }
                else
                {
                    if (!map.includes(6 - test[i]))
                    {
                        map.AddKey(test[i], i);
                    }
                }
            }
            Console.WriteLine("Done");*/
            Random rand = new Random();
            for(int i = 0; i < 1E5; i++)
            {
                if (i % 1500 == 0) Console.WriteLine("ALPHA: " + map.GetFactor());
                map.AddKey(i, rand.Next());
            }
        }
    }
}
