using System;
using System.Threading;

namespace v._2
{
    class Program
    {
        private static readonly Random random = new Random();
        private static readonly object locker = new object();
        private static readonly int _width;
        private static readonly int _height;


        static Program()
        {
            Console.CursorVisible = false;
            _width = Console.WindowWidth = 100;
            _height = Console.WindowHeight = 30;
        }

        static void Main()
        {
            for (int i = 0; i < _width; i++)
            {
                new Thread(new ParameterizedThreadStart(Update))
                    .Start(new Tuple<int, int>(i, random.Next(0, 1000)));
            }          
        }

        /// <param name="tuple">
        /// Tuple which contains three items : column, time of thread`s stop(speed),  
        /// </param>
        private static void Update(object tuple)
        {
            // Set starting values

            Tuple<int, int> inputTuple = tuple as Tuple<int, int>;
            int x = inputTuple.Item1;
            int stop = inputTuple.Item2;
            int heightOfStartingNewThread = random.Next((int)_height/2, _height-2); // random position of starting new Thread
            int y = 0;
            int length = random.Next(3,5); // random length of chain

            while (true)
            {
                Thread.Sleep(stop);
                if (isTimeToStartNewThread(y, heightOfStartingNewThread))
                {
                    new Thread(new ParameterizedThreadStart(Update))
                    .Start(tuple);
                }

                if (isTimeToRemoveThread(y, length))
                {
                    Thread.CurrentThread.Abort();
                }
                lock (locker) 
                { 
                    DrawChain(x, y, length);
                };
                y++;
            }
        }

        private static void DrawChain(int x, int y, int length)
        {
            DrawChar(x, y, ConsoleColor.White, (char)random.Next(48, 57));
            DrawChar(x, y - 1, ConsoleColor.Green, (char)random.Next(48, 57));
            for (int i = 2; i < length; i++)
            {
                DrawChar(x, y - i, ConsoleColor.DarkGreen, (char)random.Next(48, 57));
            }
            DrawChar(x, y - length, ConsoleColor.Gray, ' ');
        }

        private static bool isTimeToRemoveThread(int y, int length) =>
            (y - length) > _height;

        private static bool InFrame(int y) =>
            y >= 0 && y < _height;

        private static bool isTimeToStartNewThread(int y, int heightOfStartingNewThread) =>
            y == heightOfStartingNewThread;

        private static void DrawChar(int x, int y, ConsoleColor color, Char ch)
        {
            if (InFrame(y))
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write(ch);
            }
        }
    }
}
