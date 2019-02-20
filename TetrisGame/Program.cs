using System;
using System.Collections.Generic;
using System.Threading;

namespace Tetris
{
    class Program
    {
        // Settings
        static int TetrisRows = 20;
        static int TetrisCols = 10;
        static int InfoCols = 10;
        static int ConsoleRows = 1 + TetrisRows + 1;
        static int ConsoleCols = 1 + TetrisCols + 1 + InfoCols + 1;
        static List<bool[,]> TetrisFigures = new List<bool[,]>()

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
        static Random random = new Random(); // added random used for getting a random figure to come next on the field
        static int Score = 0;
        static int Frame = 0;
        static int FramesToMoveFigure = 15;
        static int CurrentFigureRow = 0;
        static int CurrentFigureCol = 0;
        static bool[,] TetrisField = new bool[TetrisRows, TetrisCols];
        static int CurrentFigureIndex = random.Next(0, TetrisFigures.Count);
        static bool[,] currentFigure = TetrisFigures[CurrentFigureIndex];

        static void Main(string[] args)
        {
            Console.Title = "Tetris v1.0";
            Console.CursorVisible = false;
            Console.WindowHeight = ConsoleRows + 1;
            Console.WindowWidth = ConsoleCols;
            Console.BufferHeight = ConsoleRows + 1;
            Console.BufferWidth = ConsoleCols;
            while (true)
            {
                Frame++;
                // Read user input
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();

                    if (key.Key == ConsoleKey.Escape)
                    {
                        // Environment.Exit(0);
                        return; // Becase of Main()
                    }
                    if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.A)
                    {
                        if (CurrentFigureCol > 0) // added limitations for moving left
                        {
                            CurrentFigureCol--; 
                        }
                    }
                    if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.D)
                    {
                        if (CurrentFigureCol < TetrisCols - currentFigure.GetLength(1))
                        {
                            CurrentFigureCol++; // added limitations for moving right
                        }
                    }
                    if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                    {
                        Frame = 1;
                        Score++;
                        if (CurrentFigureRow < TetrisRows - currentFigure.GetLength(0))
                        {
                            CurrentFigureRow++; //TODO: change limits for moving down corresponding to last free line on the field
                        }
                    }
                    if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                    {
                        // TODO: Implement 90-degree rotation of the current figure
                    }
                }

                // Update the game state
                if (Frame % FramesToMoveFigure == 0)
                {
                    if (CurrentFigureRow < TetrisRows - currentFigure.GetLength(0))
                    {
                        CurrentFigureRow++; //TODO: change limits for moving down corresponding to last free line on the field
                                                    //TODO: keep the figure on the field.
                    }
                    else
                    {
                        CurrentFigureRow = 0;
                        CurrentFigureCol = 0;
                        DrawCurrentFigure();
                    }
                    
                    Frame = 0;
                }
                // // TODO: if (Collision())
                // {
                //      AddCurrentFigureToTetrisField()
                //      CheckForFullLines()
                //      if (lines remove) Score++;
                // }

                // Redraw UI
                DrawBorder();
                DrawInfo();
                // TODO: DrawTetrisField();
                DrawCurrentFigure();

                Thread.Sleep(40);
            }
        }

        static void DrawBorder()
        {
            Console.SetCursorPosition(0, 0);
            string line = "╔";
            line += new string('═', TetrisCols);
            /* for (int i = 0; i < TetrisCols; i++)
            {
                line += "═";
            } */

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

        static void DrawInfo()
        {
            Write("Score:", 1, 3 + TetrisCols);
            Write(Score.ToString(), 2, 3 + TetrisCols);
            Write("Frame:", 4, 3 + TetrisCols);
            Write(Frame.ToString(), 5, 3 + TetrisCols);
        }

        static void DrawCurrentFigure()
        {
            
            for (int row = 0; row < currentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < currentFigure.GetLength(1); col++)
                {
                    if (currentFigure[row, col])
                    {
                        Write("*", row + 1 + CurrentFigureRow, col + 1 + CurrentFigureCol);
                    }
                }
            }
        }

        static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.ResetColor();
        }
    }
}