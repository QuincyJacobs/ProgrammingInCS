using System;
using System.Diagnostics;
using System.Text;

namespace ConsoleAppDiagnostics
{
    public class Map
    {
        public Vector2 Size { get; set; }
        public object[,] Tiles;

        public Map(int width, int height)
        {
            Size = new Vector2(width, height);
            // +2 for borders
            InitMap(width+2, height+2);
        }

        public void InitMap(int width, int height)
        {
            Tiles = new object[width, height];
            for (var i = 0; i < width; i++)
            {
                Tiles[i, 0] = "-";
                Tiles[i, height - 1] = "-";
            }
            for (var i = 0; i < height; i++)
            {
                Tiles[0, i] = "|";
                Tiles[width-1, i] = "|";
            }
        }

        public void Draw()
        {
            Trace.TraceInformation("Started drawing the map");
            Console.Clear();

            var mapString = new StringBuilder();

            for (int j = 0; j < Size.Y+2; j++)
            {
                for (int i = 0; i < Size.X+2; i++)
                {
                    if(Tiles[i,j] != null)
                    {
                        if(Tiles[i,j] is GameObject)
                        {
                            mapString.Append((Tiles[i, j] as GameObject).Draw());
                        }
                        else if( Tiles[i,j] is string)
                        {
                            mapString.Append(Tiles[i, j]);
                        }
                        else
                        {
                            mapString.Append(" ");
                        }
                    }
                    else
                    {
                        mapString.Append(" ");
                    }
                }
                mapString.AppendLine();
            }

            Console.WriteLine(mapString.ToString());
            Trace.TraceInformation("Finished drawing the map");
        }

        public Vector2 RequestPosition(int x, int y, GameObject gameObject)
        {
            Trace.TraceInformation("Requested a position from map");
            var result = new Vector2(x, y);

            // bounds check
            if(x > Size.X)
            {
                Debug.WriteLine("X too big!");
                Trace.TraceWarning("X too big!");
                result.X = Size.X;
            }
            else if(x <= 0)
            {
                Debug.WriteLine("X too small!");
                Trace.TraceWarning("X too small!");
                result.X = 1;
            }
            if (y > Size.Y)
            {
                Debug.WriteLine("Y too big!");
                Trace.TraceWarning("Y too big!");
                result.Y = Size.Y;
            }
            else if (y <= 0)
            {
                Debug.WriteLine("Y too small!");
                Trace.TraceWarning("Y too small!");
                result.Y = 1;
            }

            // TODO(Quincy): do a check to see what is in the original spot
            Tiles[result.X, result.Y] = gameObject;
            if(result.X == x && result.Y == y)
            {
                Tiles[gameObject.Position.X, gameObject.Position.Y] = null;
            }

            Trace.TraceInformation("Finished requesting a position from map");

            return result;
        }
    }
}
