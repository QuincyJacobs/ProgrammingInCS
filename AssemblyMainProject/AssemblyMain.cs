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
            // TODO(Quincy): Add WinMD assembly (Windows Runtime Component project) - Can't do this, because you need Windows 10 to create that type of project.
        }
    }
}
