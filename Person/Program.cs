using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Person
{
    public class Tests
    {
        public static int i = 0;
        public class First : IDisposable
        {
            public virtual void Dispose()
            {
                i += 1; Console.WriteLine("First's Dispose is called.");
            }
            ~First()
            {
                Console.WriteLine("First's Destuctor is called.");
            }
           
        }

       public class Second : First
        {
            public override void Dispose()
            {
                i += 10; Console.WriteLine("Second's Dispose is called.");
                base.Dispose();
            }
            ~Second()
            {
                Console.WriteLine("Second's Destuctor is called.");
            }
        }

       public class Third : Second
        {
            public override void Dispose()
            {
                i += 100; Console.WriteLine("Third's Dispose is called.");
                base.Dispose();
            }
            ~Third()
            {
                Console.WriteLine("Third's Destuctor is called.");
            }
        }
        public static void Test()
        {
            using (Third t = new Third())
            {
                Console.WriteLine("Now everything will be ok, after leaving this block");
                Console.WriteLine("t object will be dispose");
            }
            Thread.Sleep(1000);
            System.GC.Collect();
            GC.WaitForPendingFinalizers();
           // Assert.AreEqual(111, i);
        }
    }
    internal class Program
    {
        string name;

        void getName()
        {
            Console.WriteLine("Name: " + name);
        }
        public Program()
        {
            Console.WriteLine("Constructor called.");
        }

        // destructor
        ~Program()
        {
            Console.WriteLine("Destructor called.");
        }

        public static void Main(string[] args)
        {

            using (Person.Tests.Third  t = new Person.Tests.Third())
            {
                Console.WriteLine("Now everything will be ok, after leaving this block");
                Console.WriteLine("t object will be dispose");
            }
            Thread.Sleep(1000);
            System.GC.Collect();
            Console.ReadLine();
        }
    }
}
