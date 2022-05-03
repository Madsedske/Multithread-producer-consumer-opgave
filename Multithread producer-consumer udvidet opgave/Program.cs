using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithread_producer_consumer_udvidet_opgave
{
    internal class Program
    {
        static object _lock = new object();
        static Random r = new Random();
        static Queue<int> products = new Queue<int>();

        static void Main(string[] args)
        {
            Thread producer = new Thread(Producer);
            Thread consumer = new Thread(Consumer);

            producer.Start();
            consumer.Start();

            producer.Join();
            consumer.Join();

        }

        static void Consumer()
        {
            while (true)
            {
                lock (products)
                {
                    while (products.Count == 0)
                    {
                        Monitor.Wait(products);
                    }
                    products.Dequeue();
                    Console.WriteLine("Consumer har consumeret " + " - Queue count is " + products.Count);

                }
            }
        }

        static void Producer()
        {
            while (true)
            {
                lock (products)
                {
                    if (products.Count < 3)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            products.Enqueue(1);
                            Console.WriteLine("Producer har produceret " + " - Queue count is " + products.Count);
                        }
                        

                    }
                    else if (products.Count == 3)
                        Console.WriteLine("Producer venter..");
                    Monitor.PulseAll(products);

                }
            }
        }
    }
}
