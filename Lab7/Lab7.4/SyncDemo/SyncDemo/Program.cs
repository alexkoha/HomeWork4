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
            // This isn't good. Your mutex is unnamed and thus you'll have a diffferent mutex for each process.
            //Other processes will crash while trying to access the file simultaneously
            //To fix it: using (Mutex MutexSyncFileMutex = new Mutex(false, "AlexKohatzki"))
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
