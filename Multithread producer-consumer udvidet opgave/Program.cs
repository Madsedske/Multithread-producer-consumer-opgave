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


        static void KeepRunning()
        {

        }


        static void Consumer()
        {
            while (true)
            {
                Monitor.Enter(products);

                if (products.Count >= 1)
                {
                    Monitor.Wait(products);
                    products.Dequeue();
                    Console.WriteLine("Consumer har consumeret " + " - Queue count is " + products.Count);
                    Monitor.Exit(products);
                }
                else
                {
                    Console.WriteLine("Consumer kan ikke consumere " + " - Queue count is " + products.Count);
                }
            }
        }

        static void Producer()
        {
            while (true)
            {
                Monitor.Enter(products);

                if (products.Count < 3)
                {
                    products.Enqueue(1);
                    Monitor.PulseAll(products);
                    Console.WriteLine("Producer har produceret " + " - Queue count is " + products.Count);
                    Monitor.Exit(products);
                }
                else
                {
                    Console.WriteLine("Producer kan ikke producere " + " - Queue count is " + products.Count);
                }
            }
        }
    }
}
