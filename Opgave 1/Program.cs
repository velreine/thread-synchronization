using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Opgave_1
{
   
    class Program
    {
        private static int counter = 0;
        private static object LockObj = new object();


        static void Main(string[] args)
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (Monitor.TryEnter(LockObj, 1000))
                        {
                            counter--;
                            Console.WriteLine(counter);
                        }

            
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    finally
                    {
                        Monitor.Exit(LockObj);
                    }

                    Thread.Sleep(1000);
                }


            })
            {
                Name = "counter_decrement_by1_thread"
            };

            Thread t2 = new Thread(() =>
            {

                while (true)
                {
                    try
                    {
                        if (Monitor.TryEnter(LockObj, 1000))
                        {
                            counter += 2;
                            Console.WriteLine(counter);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        Monitor.Exit(LockObj);
                    }

                    Thread.Sleep(1000);
                }
            })
            {
                Name = "counter_increment_by2_thread"
            };

            t.Start();
            t2.Start();

            //t.Join();
            //t2.Join();


            // Prevent Console Application from Exiting without pressing enter.
            Console.ReadLine();


        }
    }
}
