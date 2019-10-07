using System;
using System.Diagnostics;

namespace ConsoleAppDiagnostics
{
    public class Character : GameObject
    {
        private int _health = 100;
        private int _magic = 50;
        private int _stamina = 25;
        private bool _sitting;

        public Character()
        {
            Debug.WriteLine("Empty character created");
        }

        public Character(int positionX, int positionY)
        {
            Position.X = positionX;
            Position.Y = positionY;
            Move(positionX, positionY);
            Debug.WriteLine("Character created at " + Position.X + " : " + Position.Y);
        }

        public void Move(int x, int y)
        {
            Debug.WriteLine("Old position: x = " + Position.X + " : y =  " + Position.Y);
            Debug.Indent();
            Debug.WriteLine("Moving the character " + x + " : " + y);

            Debug.Assert(GameState.Map != null, "There was no Map defined in the GameState.");

            Position = GameState.Map?.RequestPosition(Position.X + x, Position.Y + y, this);

            Debug.Indent();

            Debug.Unindent();
            Debug.Unindent();
            Debug.WriteLine("New position: x = " + Position.X + " : y =  " + Position.Y);
        }

        public void ProcessInput(char input)
        {
            Trace.TraceInformation("Ended input reading");

            switch(input)
            {
                case 'w':
                    Move(0, -1);
                    break;
                case 'a':
                    Move(-1, 0);
                    break;
                case 's':
                    Move(0, 1);
                    break;
                case 'd':
                    Move(1, 0);
                    break;
                default:
                    Debug.WriteLineIf(input.Equals('q'), "\r\nYou can check-out any time you like, but you can never leave!\r\n");
                    Trace.TraceWarning("Wrong input given: " + input);
                    break;
            }

            Trace.TraceInformation("Ended input reading");

        }

        public override string Draw()
        {
            return "H";
        }
    }
}
