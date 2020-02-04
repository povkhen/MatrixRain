using System;
using System.Threading;

namespace v1
{

    class Program
    {
        /// <summary>
        /// Get random char
        /// </summary>
        private static char Char
        {
            get
            {
                return (char)(HelperConst.rand.Next(48, 57));
            }
        }

        static void Main()
        {
            Console.WindowHeight = HelperConst.WINDOWHEIGHT +20;
            Console.WindowWidth = HelperConst.WINDOWWIDTH + 20;
            
            Console.CursorVisible = false;
            

            int width = HelperConst.WINDOWWIDTH;

            Column[] columns = new Column[width];
            Thread[] thread = new Thread[width];

            for (int x = 0; x < width; ++x)
            {
                int speed = HelperConst.rand.Next(HelperConst.MAXSPEEDOFCOLUMN, HelperConst.MINSPEEDOFCOLUMN);
                columns[x] = new Column();
                ParameterizedThreadStart paramDel = new ParameterizedThreadStart(columns[x].Update);
                thread[x] = new Thread(paramDel);
                thread[x].Start(speed);
                
            }
            while (true)
            {
                Redraw(columns);
            }
        }
        /// <summary>
        /// Check if position of Y position in window
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool InFrame(int y)
        {
            return y >= 0 && y < HelperConst.WINDOWHEIGHT;
        }

        private static void DrawChar(int x, int y, ConsoleColor color)
        {
            if (InFrame(y))
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write(Char);
            }
        }
        /// <summary>
        /// Redraw all updated columns
        /// </summary>
        private static void Redraw( Column[] columns)
        {
                int width = columns.Length;
                for (int i = 0; i < width; i++)
                {
                    if (!columns[i].isUpdated) continue;
                    ClearColumn(i);
                    var chains = columns[i].Chains.ToArray();
                    for (int c = 0; c < chains.Length; c++)
                    {
                        DrawChar(i, chains[c].CurrentLocationHead, ConsoleColor.White);
                        DrawChar(i, chains[c].CurrentLocationHead-1, ConsoleColor.Green);
                        for (int g = 2; g < chains[c].LengthOfChain; g++)
                        {
                            DrawChar(i, chains[c].CurrentLocationHead - g, ConsoleColor.DarkGreen);
                        }
                    }
                    columns[i].isUpdated = false;
                }
        }

        private static void ClearColumn(int x)
        {
            for (int i = 0; i < HelperConst.WINDOWHEIGHT; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write(" ");
            }
        }
    } 
}