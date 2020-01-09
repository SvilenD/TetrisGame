namespace TetrisGame
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    class Program
    {
        // Settings
        static readonly int TetrisRows = 20;
        static readonly int TetrisCols = 16;
        static readonly int InfoCols = 10;
        static readonly int ConsoleRows = 1 + TetrisRows + 1;
        static readonly int ConsoleCols = 1 + TetrisCols + 1 + InfoCols + 1;
        static readonly List<bool[,]> TetrisFigures = new List<bool[,]>()
            {
                new bool[,] // ----
                {
                    { true, true, true, true }
                },
                new bool[,] // O
                {
                    { true, true },
                    { true, true }
                },
                new bool[,] // T
                {
                  { false, true, false },
                  { true, true, true },
                },
                new bool[,] // S
                {
                    { false, true, true, },
                    { true, true, false, },
                },
                new bool[,] // Z
                {
                    { true, true, false },
                    { false, true, true },
                },
                new bool[,] // J
                {
                    { true, false, false },
                    { true, true, true }
                },
                new bool[,] // L
                {
                    { false, false, true },
                    { true, true, true }
                },
            };

        // State
        static readonly Random random = new Random();
        static bool[,] CurrentFigure = TetrisFigures[random.Next(0, TetrisFigures.Count)];
        static int CurrentFigureRow = 0;
        static int CurrentFigureCol = 0;
        static int Frame = 0;
        static int Score = 0;
        static bool[,] TetrisField = new bool[TetrisRows, TetrisCols]; //keeps all figures that are already in the game
        static int Level;
        static int FramesToMoveFigure;  // changes game speed
        static bool SuppressKeyPress;

        static void Main()
        {
            Console.Title = "Tetris v1.0";
            Console.CursorVisible = false;
            Console.WindowHeight = ConsoleRows + 1;
            Console.WindowWidth = ConsoleCols;
            Console.BufferHeight = ConsoleRows + 1;
            Console.BufferWidth = ConsoleCols;

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
                        Write("Game Paused", 2, TetrisCols / 3, ConsoleColor.White);
                        Write("Press Esc to continue", 3, 1, ConsoleColor.White);
                        key = Console.ReadKey();
                        while (key.Key != ConsoleKey.Escape)
                        {
                            Thread.Sleep(1000000000);
                            key = Console.ReadKey();
                        }
                    }

                    //move left
                    if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                    {
                        if (CurrentFigureCol >= 1)
                        {
                            CurrentFigureCol--;
                        }
                    }
                    //move right
                    if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                    {
                        if (CurrentFigureCol < TetrisCols - CurrentFigure.GetLength(1))
                        {
                            CurrentFigureCol++;
                        }
                    }
                    //move down
                    if ((key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S) && SuppressKeyPress == false)
                    {
                        if (CurrentFigureRow < ConsoleRows)
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
                    SuppressKeyPress = false;
                }

                //Redraw UI
                DrawBorder();
                DrawInfo();
                DrawTetrisField();
                DrawCurrentFigure();
                //TODO:
                if (Collision())
                {
                    AddCurrentFigureToTetrisField();
                    //CheckForFullLines()
                    //if (lines remove) Score++;
                    CurrentFigure = TetrisFigures[random.Next(0, TetrisFigures.Count)];
                    CurrentFigureRow = 0;
                    CurrentFigureCol = 0;
                }
                Thread.Sleep(40);
            }
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
            if (CurrentFigureRow + CurrentFigure.GetLength(0) == TetrisRows)
            {
                return true;
            }
            return false;
        }

        private static void DrawTetrisField()
        {
            for (int row = 0; row < TetrisField.GetLength(0); row++)
            {
                for (int col = 0; col < TetrisField.GetLength(1); col++)
                {
                    if (TetrisField[row, col])
                    {
                        Write("*", row + 1,  col + 1);
                    }
                }
            }
        }

        private static void DrawBorder()
        {
            Console.SetCursorPosition(0, 0);
            string line = "╔";
            line += new string('═', TetrisCols);

            line += "╦";
            line += new string('═', InfoCols);
            line += "╗";
            Console.Write(line);

            for (int i = 0; i < TetrisRows; i++)
            {
                string middleLine = "║";
                middleLine += new string(' ', TetrisCols);
                middleLine += "║";
                middleLine += new string(' ', InfoCols);
                middleLine += "║";
                Console.Write(middleLine);
            }

            string endLine = "╚";
            endLine += new string('═', TetrisCols);
            endLine += "╩";
            endLine += new string('═', InfoCols);
            endLine += "╝";
            Console.Write(endLine);
        }

        private static void DrawInfo()
        {
            Write("Score:", 1, 3 + TetrisCols);
            Write(Score.ToString(), 2, 3 + TetrisCols);
            Write("Level:", 4, 3 + TetrisCols);
            Write(Level.ToString(), 5, 3 + TetrisCols);
            //Write("Frame:", 6, 3 + TetrisCols);
            //Write(Frame.ToString(), 7, 3 + TetrisCols);
            //Write("ToMoveFig:", 8, 3 + TetrisCols);
            //Write(FramesToMoveFigure.ToString(), 9, 3 + TetrisCols);
        }

        private static void DrawCurrentFigure()
        {
            for (int row = 0; row < CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentFigure.GetLength(1); col++)
                {
                    if (CurrentFigure[row, col])
                    {
                        Write("*", row + 1 + CurrentFigureRow, col + 1 + CurrentFigureCol);
                    }
                }
            }
        }

        private static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.ResetColor();
        }
    }
}