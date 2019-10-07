using System;
using AssemblyDependedProject;

namespace AssemblyMainProject
{
    class AssemblyMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var book = new Book();

            // TODO(Quincy): Add dependent assembly binding redirect
        }
    }
}
