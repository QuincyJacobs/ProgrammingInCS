#define PROGRAM
#if TEST
#define TESTAGAIN
#undef PROGRAM
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    /// <summary>
    /// The <see cref="PreprocessorCompilerDirectives"/> class showcases some functionalities of preprocessor compiler directives.
    /// </summary>
    public static class PreprocessorCompilerDirectives
    {
        #region Conditional compilation symbol functions

        /// <summary>
        /// Conditional compilation symbols example
        /// </summary>
        public static void DefineTest()
        {
            Console.WriteLine("\n");
#if DEBUG
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("DEBUG is defined");
            Console.ForegroundColor = ConsoleColor.White;
#else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("DEBUG is not defined");
            Console.ForegroundColor = ConsoleColor.White;
#endif
#if TEST
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("TEST is defined");
            Console.ForegroundColor = ConsoleColor.White;
#else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("TEST is not defined");
            Console.ForegroundColor = ConsoleColor.White;
#endif
#if RELEASE
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("RELEASE is defined");
            Console.ForegroundColor = ConsoleColor.White;
#else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("RELEASE is not defined");
            Console.ForegroundColor = ConsoleColor.White;
#endif
#if PRODUCTION
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("PRODUCTION is defined");
            Console.ForegroundColor = ConsoleColor.White;
#else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("PRODUCTION is not defined");
            Console.ForegroundColor = ConsoleColor.White;
#endif
#if DIAGNOSTICS
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("DIAGNOSTICS is defined");
            Console.ForegroundColor = ConsoleColor.White;
#else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("DIAGNOSTICS is not defined");
            Console.ForegroundColor = ConsoleColor.White;
#endif
#if TRACE
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("TRACE is defined");
            Console.ForegroundColor = ConsoleColor.White;
#else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("TRACE is not defined");
            Console.ForegroundColor = ConsoleColor.White;
#endif
#if PROGRAM
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("PROGRAM is defined");
            Console.ForegroundColor = ConsoleColor.White;
#else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("PROGRAM is not defined");
            Console.ForegroundColor = ConsoleColor.White;
#endif
#if TESTAGAIN
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("TESTAGAIN is defined");
            Console.ForegroundColor = ConsoleColor.White;
#else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("TESTAGAIN is not defined");
            Console.ForegroundColor = ConsoleColor.White;
#endif
            Console.WriteLine("\n");
        }

        #endregion

        #region Conditional attribute functions

        /// <summary>
        /// Conditional attribute example
        /// </summary>
        [Conditional("TEST")]
        public static void TestOnlyFunction()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hello from the TestOnlyFunction\n\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion

        #region Obsolete functions

        /// <summary>
        /// Example without obsolete
        /// </summary>
        public static void NotObsolete()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Awesome");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Obsolete example with warning
        /// </summary>
        [Obsolete("This method is obsolete since version 0.9.0.0. Call NotObsolete instead", false)]
        public static void ObsoleteWarning()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Barely awesome");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Obsolete example with error
        /// </summary>
        [Obsolete("This method is obsolete since version 0.9.0.0. Call ObsoleteWarning instead", true)]
        public static void ObsoleteError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Not awesome");
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion

        #region Warning and Error directive functions

        /// <summary>
        /// Generates warning in any build mode
        /// </summary>
        public static void WarningGenerator()
        {
#warning funky warning
#warning heartwarming warning
#warning excellent warning
#warning depressing warning
#warning unfixable warning
#warning java warning
#warning Rob "Robby Boy" Miles warning

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nYou've got some issues buddy.\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Generates an error when Production and Trace conditionals are both set.
        /// </summary>
        public static void ErrorGenerator()
        {
#if PRODUCTION && TRACE
#error funky error
#error heartwarming error
#error excellent error
#error depressing error
#error unfixable error
#error java error
#error angry Rob "Robby Boy" Miles error

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nYou've got some real issues buddy.\n");
            Console.ForegroundColor = ConsoleColor.White;
#endif
        }

        #endregion

        #region Pragma directive functions

        /// <summary>
        /// This method will not generate any warning because we disable all warning using a pragma
        /// </summary>
#pragma warning disable
        public static void NoWarnings()
        {
#warning funky warning

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nWow no issues, you're awesome!\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
#pragma warning restore

        /// <summary>
        /// This method will only disable the CS1030 warning, which is the warning compiler directive number.
        /// </summary>
#pragma warning disable CS1030
        [CLSCompliant(false)]
        public static void NoSpecificWarnings()
        {
#warning funky warning

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nWow only 1 warning!\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
#pragma warning restore CS1030

        #endregion

        #region Line directive functions

        /// <summary>
        /// This method will modify from which file and on which line the exception was thrown
        /// </summary>
        public static void ExceptionWithCustomLine()
        {
#line 1337 "awesome.file"
            throw new ApplicationException("Sorry to foil your plans amigo.");
#line default
        }
        
        /// <summary>
        /// This method will hide code form the debugger so the program can't step into/over those lines of code
        /// </summary>
        public static void HideFromDebugger()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("I was too early to be sneaky.");
            Console.ForegroundColor = ConsoleColor.White;
#line hidden
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Just in time to be sneaky.");
            Console.WriteLine("Sneaky sneaky line.");
            Console.WriteLine("Almost too late to be sneaky.");
            Console.ForegroundColor = ConsoleColor.White;
#line default
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("I was too late to be sneaky.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion

        #region DebuggerStepThrough functions

        /// <summary>
        /// This method uses an attribute to disable stepping through it with a debugger
        /// </summary>
        [DebuggerStepThrough]
        public static void DebuggerStepThrough()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n");
            Console.WriteLine("This");
            Console.WriteLine("method");
            Console.WriteLine("is");
            Console.WriteLine("totally");
            Console.WriteLine("sneaky.");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion
    }
}
