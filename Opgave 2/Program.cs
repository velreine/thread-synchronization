using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Opgave_2
{
    class Program
    {
        private static int totalCharsWritten = 0;
        private static object LockObj = new object();

        static void Main(string[] args)
        {
            const string starString = "************************************************************";
            const string hashString = "############################################################";



            Thread t = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (Monitor.TryEnter(LockObj, 1000))
                        {
                            totalCharsWritten += starString.Length;
                            Console.Write($"{starString}  {totalCharsWritten}\r\n");
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
                
                
            });

            Thread t2 = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (Monitor.TryEnter(LockObj, 1000))
                        {
                            totalCharsWritten += hashString.Length;
                            Console.WriteLine($"{hashString} {totalCharsWritten}");
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
                
            });

            t.Start();
            t2.Start();
        
            Console.ReadKey();




        }
    }
}
