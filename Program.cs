using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelLoops
{
    class Program
    { 
        public static void Main()
        {
            //ParallelFor test
            Console.WriteLine("Parallel for test!");
            Parallel.ParallelFor(0,100,Console.WriteLine);
            Console.ReadLine();

            //ParallelForEach test
            Console.WriteLine("Parallel ForEach test!");
            List<int> list = new List<int>();
            for (var counter = 0; counter < 100; counter++)
            {
                list.Add(counter);
            }
            Parallel.ParallelForEach(list,Console.WriteLine);
            Console.ReadLine();

            //Parallel For Each with options test
            Console.WriteLine("Parallel ForEach with options Test!");
            Parallel.ParallelForEachWithOptions(list, new ParallelOptions() {MaxDegreeOfParallelism = 4}, Console.WriteLine);
            Console.ReadLine();
        }

       
    }
}
