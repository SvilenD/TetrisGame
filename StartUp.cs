namespace TetrisGame
{
    using System;

    internal class StartUp
    {
        private static readonly Random random = new Random();
        private static int CurrentFigureRow = 0;
        private static int CurrentFigureCol = 0;
        private static int Frame = 0;
        private static int FramesToMoveFigure;  //changes game speed
        private static bool[,] TetrisField = new bool[Settings.TetrisRows, Settings.TetrisCols]; //keeps all figures that are already in the game
        private static string UserName;
        private static bool SuppressKeyPress;
        private static int Score = 0;
        private static int Level = 1;
        private static bool[,] CurrentFigure = Settings.TetrisFigures[random.Next(0, Settings.TetrisFigures.Count)];

        static void Main()
        {
            GetUserName();
            new ConsoleSetup();

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
                        GameModes.Pause();
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
                Writer.DrawInfo(Score, Level);
                Writer.DrawTetrisField(TetrisField);
                Writer.DrawCurrentFigure(CurrentFigure, CurrentFigureRow, CurrentFigureCol);

                if (Collision())
                {
                    AddCurrentFigureToTetrisField();
                    //CheckForFullLines();

                    CurrentFigure = Settings.TetrisFigures[random.Next(0, Settings.TetrisFigures.Count)];
                    CurrentFigureRow = 0;
                    CurrentFigureCol = 0;
                    if (Collision())
                    {
                        GameModes.GameOver(Score, UserName);
                    }
                }

                SuppressKeyPress = Frame % 5 == 0;
                GameModes.Sleep();
            }
        }

        private static void GetUserName()
        {
            Writer.Write("Please enter your Name: ", 3, 5, ConsoleColor.White);
            UserName = Console.ReadLine();
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