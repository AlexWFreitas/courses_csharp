using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            var catalog = new Catalog();  // Instance of Catalog

            ((ISaveable)catalog).Save();  // Explicit Interface Implementation

            catalog.Save();               // Normal Interface Implementation

            Console.ReadLine();           // Waiting for Results
        }
    }
}
