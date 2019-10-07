using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDiagnostics
{
    public class GameObject
    {
        public Vector2 Position = new Vector2();

        public virtual string Draw()
        {
            return "O";
        }
    }
}
