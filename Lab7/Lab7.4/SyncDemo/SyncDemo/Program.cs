using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new Mutex.
            using (Mutex MutexSyncFileMutex = new Mutex())
            {
                Console.WriteLine("Lets start reccord.\nPress Enter To start...");
                Console.Read();
                Console.WriteLine("Working....");

                for (int i = 0; i < 10000; i++)
                {
                    // block current thread 
                    MutexSyncFileMutex.WaitOne();
                    try
                    {
                        using (StreamWriter fileWriter = new StreamWriter(@"c:\temp\data.txt", true))
                        {
                            fileWriter.WriteLine($"Process id {Process.GetCurrentProcess().Id}");
                        }
                    }
                    finally
                    {
                        // Release the Mutex
                        MutexSyncFileMutex.ReleaseMutex();
                    }

                }

                Console.WriteLine("Done !");

            }
        }
    }
}
