using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            PreprocessorCompilerDirectives.DefineTest();
            PreprocessorCompilerDirectives.TestOnlyFunction();
            //PreprocessorCompilerDirectives.ObsoleteError();
            PreprocessorCompilerDirectives.ObsoleteWarning();
            PreprocessorCompilerDirectives.NotObsolete();
            PreprocessorCompilerDirectives.WarningGenerator();
            PreprocessorCompilerDirectives.ErrorGenerator();

            try
            {
                PreprocessorCompilerDirectives.ExceptionWithCustomLine();
            }
            catch(ApplicationException ax)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n" + ax.StackTrace + "\n");
                Console.ForegroundColor = ConsoleColor.White;
            }

            PreprocessorCompilerDirectives.HideFromDebugger();
            PreprocessorCompilerDirectives.DebuggerStepThrough();

            // TODO(Quincy): Build configurations
            // TODO(Quincy): Program database (pdb) 

            Console.WriteLine("Goodbye cruel World!");
            Console.ReadKey();
        }
    }
}
