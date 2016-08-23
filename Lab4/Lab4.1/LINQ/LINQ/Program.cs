using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var display =
                    // should be:
                    // From interfacesVariable in Assembly.Load("mscorlib").GetType().Where(x=>x.IsInterface)
                    // Why did't you use: From .... in ...... syntex ???
                    typeof(Assembly).Assembly.GetExportedTypes().Where(x=>x.IsInterface)
                    .Where((x) => x.IsPublic)
                    .Select(x => new {x.Name, x.GetMethods().Length});//x + " " + x.GetMethods().Length.ToString());
            Console.WriteLine("Interfaces & Number Of Methodes in Assembly :");
            foreach (var item in display)
            {
                Console.WriteLine(item);
            }

            
            var processes = Process.GetProcesses();
            // Why did't you use: From .... in ...... syntex ???
            var displayProcess = processes.Where((x) => x.Threads.Count < 5 && x.AccessProccess())
                .Select((x) => new {  x.ProcessName, x.Id,  x.StartTime })
                .OrderBy((x) => x.Id)
                .ToArray();
            Console.WriteLine("\nProcesses :");
            foreach (var item in displayProcess)
            {
                Console.WriteLine(item);
            }

            // Why did't you use: From .... in ...... syntex ???
            var grouptProcesses = processes.Where((x) => x.Threads.Count < 5 && x.AccessProccess() )
                .GroupBy(x => x.BasePriority , x=>new { x.ProcessName, x.Id , x.StartTime});
                
            Console.WriteLine("\nProcesses :");
            foreach (var item in grouptProcesses)
            {
                Console.WriteLine("Key : "+ item.Key);

                foreach (var part in item)
                {
                    Console.WriteLine(part);
                }
            }

            // Forget check for Access Proccess
            var totalThreads = Process.GetProcesses().Sum((procces) => procces.Threads.Count);
            Console.WriteLine($"\nTotal Threads : {totalThreads}");


            Student student = new Student { Name = "Alex" , Age = 18 };
            Person person = new Person { Name = "Moshe" };

            Console.WriteLine("\nCopy Test Exstention:");
            Console.WriteLine($"Before Copy :\n{person}");
            student.CopyTo(person);
            Console.WriteLine($"\nAfter Copy :\n{person}");

        }


    }

    public static class Extensions
    {
        public static void CopyTo(this object source, object target)
        {
            var hendler = from src in source.GetType().GetProperties()
                from trg in target.GetType().GetProperties()
                where src.Name == trg.Name && src.PropertyType == trg.PropertyType && src.CanRead && trg.CanWrite
                select new {SourceProperty = src, TargetProperty = trg};

            foreach (var property in hendler)
            {
                property.TargetProperty.SetValue(target , property.SourceProperty.GetValue(source));
            }
        }

        public static bool AccessProccess(this Process process)
        {
            try
            {
                //if process has proceess.StartTime() it exist,otherwise exception 
                return process != null && process.Handle != IntPtr.Zero;
            }
            catch (Exception exn)
            {
                return false;
            }
        }
    }
}
