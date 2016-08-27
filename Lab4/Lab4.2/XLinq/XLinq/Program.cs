using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            // 2
            var listOfClasses = typeof(Assembly).Assembly.GetExportedTypes()
                .Where(x => x.IsClass) // & x.IsClass
                .Select((clas) => 
                new XElement("Type", 
                new XAttribute("FullName", clas.FullName),
                    new XElement("Propirties",
                        //should be clas.GetProperties(BindingFlags.Public | BindingFlags.Instance) 
                        clas.GetProperties().Select(p =>
                    new XElement("Property", 
                    new XAttribute("Name", p.Name),
                    new XAttribute("Type", p.PropertyType.FullName ?? "T")))),
                    new XElement("Methodes" , 
                        clas.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Where(method=> !method.IsSpecialName) // ???
                        .Select(method=>
                    new XElement("Method" , 
                    new XAttribute("Name" , method.Name),
                    new XAttribute("ReturnType" , method.ReturnType.FullName ?? "T"), // why not just method.ReturnType ??
                        new XElement("Parameter" ,
                            method.GetParameters().Select(parameter=>
                        new XElement("Parameter",
                        new XAttribute("Name", parameter.Name),
                        new XAttribute("Type" , parameter.ParameterType))))))))).ToArray();
            
            var xml = new XElement("Types" , listOfClasses);
            Console.WriteLine(xml);

            // 3.a
            var listOfTypesNoProperties = from type in listOfClasses
                let element = type.Element("Propirties")
                where element != null && !element.Descendants().Any()
                let nameOfType = type.Attribute("FullName").ToString()
                orderby nameOfType
                select nameOfType;
               
            Console.WriteLine($"All types without properties {listOfTypesNoProperties.Count()} : ");
            foreach (var type in listOfTypesNoProperties)
            {
                Console.WriteLine(type);
            }

            // 3.c

            var sumOfMethods = listOfClasses.Descendants("Method").Count();
            Console.WriteLine($"\nTotal of methods : {sumOfMethods}\n");

            var parameters = from element in listOfClasses.Descendants("Parameter")
                group element by (string)element.Attribute("Type")
                into grp orderby grp.Count() descending
                select new {Name = grp.Key , Count = grp.Count()};

            Console.WriteLine($"Most Common : {parameters.First().Name} {parameters.First().Count} times");

            // 3.d

            var orderdByMethods =from type in listOfClasses
                let methods = type.Descendants("Method").Count()
                orderby methods descending
                select new
                {
                    Name = (string)type.Attribute("FullName"),
                    Methods = methods,
                    Properties = type.Descendants("Property").Count()
                };

            Console.WriteLine("\nProperties And Methods :");
            foreach (var method in orderdByMethods)
            {
                Console.WriteLine(method);
            }

            //3.e

            var typesByMethods = from type in listOfClasses
                orderby (string) type.Attribute("FullName")
                group new
                {
                    Name = (string) type.Attribute("FullName"),
                    Methods = type.Descendants("Method").Count(),
                    Properties = type.Descendants("Property").Count()

                } by type.Descendants("Method").Count()
                into grp
                orderby grp.Key descending
                select grp;

            Console.WriteLine("\nTypes By Methods:");
            foreach (var grp in typesByMethods)
            {
                Console.WriteLine($"Methods: {grp}");
                foreach (var i in grp)
                    Console.WriteLine($"{i.Name} - {i.Properties} Properties");
            }

        }
    }
}
