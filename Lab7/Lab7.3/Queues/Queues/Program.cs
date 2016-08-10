using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new LimitedQueue<int>(10);

            Timer popUpMessage = new Timer((_) => {
                Console.WriteLine("Items in Queue: {0}", queue.Count);
            }, null, 100, 100);

            Random r = new Random();

            for (int i = 0; i < 100; i++)
                ThreadPool.QueueUserWorkItem((x) => {
                    for (int j = 0; j < 100; j++)
                    {
                        if (r.Next(50)>28)
                            queue.Enque(2);
                        else
                        {
                            try
                            {
                                queue.Deque();
                            }
                            catch
                            {
                            }
                        }
                        Thread.Sleep(r.Next(10));
                    }
                });
            Thread.Sleep(120000);
            popUpMessage.Dispose();
        }
    }
}
