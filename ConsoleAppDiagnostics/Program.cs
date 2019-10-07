using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDiagnostics
{
    public class Program
    {
        static void Main(string[] args)
        {
            // use 'w', 'a', 's', 'd' to move and generate debug and trace

            // comment next line for assert error
            GameState.Map = new Map(30, 25);

            GameState.Character = new Character(10, 10);
            GameState.Map.Draw();

            for(;;)
            {
                Trace.WriteLine("Iteration of the eternal input loop");
                var read = Console.ReadKey().KeyChar;
                Trace.WriteLine("Read user input");
                GameState.Character.ProcessInput(read);
                Trace.WriteLine("Processed user input");
                GameState.Map.Draw();
                Trace.WriteLine("Drawn the map");
                Trace.WriteLine("End of the iteration of the eternal loop");
            }
        }
    }
}
