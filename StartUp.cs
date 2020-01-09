namespace TetrisGame
{
    using System;
    using System.Threading;

    internal class StartUp
    {
        internal static int CurrentFigureRow = 0;
        internal static int CurrentFigureCol = 0;
        internal static int Frame = 0;
        internal static int Score = 0;
        internal static bool[,] TetrisField = new bool[Settings.TetrisRows, Settings.TetrisCols]; //keeps all figures that are already in the game
        internal static int Level;
        internal static readonly Random random = new Random();
        internal static bool[,] CurrentFigure = Settings.TetrisFigures[random.Next(0, Settings.TetrisFigures.Count)];
        static int FramesToMoveFigure;  // changes game speed
        static bool SuppressKeyPress;

        static void Main()
        {
            Console.Title = "Tetris v1.0";
            Console.CursorVisible = false;
            Console.WindowHeight = Settings.ConsoleRows + 1;
            Console.WindowWidth = Settings.ConsoleCols;
            Console.BufferHeight = Settings.ConsoleRows + 1;
            Console.BufferWidth = Settings.ConsoleCols;

            while (true)
            {
                FramesToMoveFigure = Calculator.FramesToMoveFigure(Level);
                Level = Calculator.GetLevel(Score);
                Frame++;

                // Read user input
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();

                    //pause game
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Writer.Write("Game Paused", 2, Settings.TetrisCols / 3, ConsoleColor.White);
                        Writer.Write("Press Esc to continue", 3, 1, ConsoleColor.White);
                        key = Console.ReadKey();
                        while (key.Key != ConsoleKey.Escape)
                        {
                            Thread.Sleep(10000000);
                            key = Console.ReadKey();
                        }
                    }

                    //move left
                    if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                    {
                        if (CurrentFigureCol >= 1 && !TetrisField[CurrentFigureRow, CurrentFigureCol - 1])
                        {
                            CurrentFigureCol--;
                        }
                    }
                    //move right
                    if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                    {
                        if (CurrentFigureCol < Settings.TetrisCols - CurrentFigure.GetLength(1) && !TetrisField[CurrentFigureRow, CurrentFigureCol + 1])
                        {
                            CurrentFigureCol++;
                        }
                    }
                    //move down
                    if ((key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S) && SuppressKeyPress == false)
                    {
                        if (CurrentFigureRow < Settings.ConsoleRows)
                        {
                            Frame = 1;
                            Score++;
                            CurrentFigureRow++;
                        }
                        SuppressKeyPress = true;
                    }
                    if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                    {
                        // TODO: Implement 90-degree rotation of the current figure
                    }
                }

                // Update the game state
                if (Frame % FramesToMoveFigure == 0)
                {
                    CurrentFigureRow++;
                    Frame = 0;
                }

                //Redraw UI
                Writer.DrawBorder();
                Writer.DrawInfo();
                Writer.DrawTetrisField();
                Writer.DrawCurrentFigure();

                if (Collision())
                {
                    AddCurrentFigureToTetrisField();
                    //CheckForFullLines();

                    CurrentFigure = Settings.TetrisFigures[random.Next(0, Settings.TetrisFigures.Count)];
                    CurrentFigureRow = 0;
                    CurrentFigureCol = 0;
                    if (Collision())
                    {
                        GameStates.GameOver();
                    }
                }
                Thread.Sleep(40);
                SuppressKeyPress = Frame % 5 == 0;
            }
        }

        private static void CheckForFullLines()
        {
            throw new NotImplementedException();
        }

        private static void AddCurrentFigureToTetrisField()
        {
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    if (CurrentFigure[row, col] == true)
                    {
                        TetrisField[CurrentFigureRow + row, CurrentFigureCol + col] = true;
                    }
                }
            }
        }

        private static bool Collision()
        {
            if (CurrentFigureRow + CurrentFigure.GetLength(0) == Settings.TetrisRows)
            {
                return true;
            }
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    if (CurrentFigure[row, col] && TetrisField[CurrentFigureRow + row + 1, CurrentFigureCol + col])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}