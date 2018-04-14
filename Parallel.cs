using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLoops
{
    public static class Parallel
    {
        /// <summary>
        /// Does for loop in a parallel way.
        /// </summary>
        /// <param name="fromInclusive"> Inclusive start index. </param>
        /// <param name="toExclusive"> Exclusive end index. </param>
        /// <param name="body"> Fuction that is invoked once per iteration. </param>
        public static void ParallelFor(int fromInclusive, int toExclusive, Action<int> body)
        {
           
            List<Task> taskList = new List<Task>();

            if (body == null)
            {
                throw new ArgumentNullException("Body must be non-null to be invoked.");
            }

            TaskFactory taskFactory = new TaskFactory();
            for (var counter = fromInclusive; counter < toExclusive; counter++)
            {
                var counterCopy = counter;
                var task = taskFactory.StartNew(() => body(counterCopy));
                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());
        }

        /// <summary>
        /// Does foreach loop in a parallel way.
        /// </summary>
        /// <typeparam name="TSource"> Type of source. </typeparam>
        /// <param name="source"> Source to be iterated. </param>
        /// <param name="body"> Function which will be invoked once per iteration. </param>
        public static void ParallelForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Source is null");
            }

            if (body == null)
            {
                throw new ArgumentNullException("Function must be non-null to be invoked.");
            }

            List<Task> taskList = new List<Task>();
            TaskFactory taskFactory = new TaskFactory();
            foreach (var currentElement in source)
            {
                var task = taskFactory.StartNew(() => body(currentElement));
                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());

        }

        /// <summary>
        /// Does foreach loop with parallel options in a parallel way.
        /// </summary>
        /// <typeparam name="TSource"> Type of source. </typeparam>
        /// <param name="source"> Source to be iterated. </param>
        /// <param name="parallelOptions"> Parallel options. </param>
        /// <param name="body"> Function which will be invoked once for iteration.</param>
        public static void ParallelForEachWithOptions<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions,
            Action<TSource> body)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Source is null");
            }

            if (body == null)
            {
                throw new ArgumentNullException("Function must be non-null to be invoked");
            }

            TaskFactory taskFactory = new TaskFactory(parallelOptions.CancellationToken, TaskCreationOptions.None,
                TaskContinuationOptions.None, parallelOptions.TaskScheduler);
            List<Task> taskList = new List<Task>();
            foreach (var currentElement in source)
            {
                var task = taskFactory.StartNew(() => body(currentElement));
                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());

        }
    }
}
