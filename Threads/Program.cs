using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Reader> readers = new List<Reader>();

            for (int i = 1; i < 4; i++)
            {
                Reader reader = new Reader(i);// crearea cititorilor 1,2,3
                readers.Add(reader);
            }

            foreach (var t in  readers.Select(x=>x.MyThread).ToList())
            {
                t.Join();
            };

            Reader reader4 = new Reader(4); // cititor 4
            reader4.MyThread.Join();

            for (int i = 5; i < 8; i++)
            {
                Reader reader = new Reader(i); // crearea cititorilor 5,6,7
                readers.Add(reader);
            }

            Console.ReadLine();
        }
    }

    class Reader
    {
        //creare semaforu
        static Semaphore sem = new Semaphore(3, 3);
        public Thread MyThread { get; }

        public Reader(int i)
        {
            MyThread = new Thread(Read);
            MyThread.Name = $"Cititorul {i.ToString()}";
            MyThread.Start();
        }

        public void Read()
        {
            sem.WaitOne();

            Console.WriteLine($"{Thread.CurrentThread.Name} intra in biblioteca");

            Console.WriteLine($"{Thread.CurrentThread.Name} citeste");
            Thread.Sleep(1000);

            Console.WriteLine($"{Thread.CurrentThread.Name} paraseste biblioteca");

            sem.Release();

            Thread.Sleep(1000);
        }
    }
}
