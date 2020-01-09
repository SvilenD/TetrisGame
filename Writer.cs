namespace TetrisGame
{
    using System;

    internal static class Writer
    {
        internal static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
            Console.ResetColor();
        }

        internal static void DrawBorder()
        {
            Console.SetCursorPosition(0, 0);
            string line = "╔";
            line += new string('═', Settings.TetrisCols);

            line += "╦";
            line += new string('═', Settings.InfoCols);
            line += "╗";
            Console.Write(line);

            for (int i = 0; i < Settings.TetrisRows; i++)
            {
                string middleLine = "║";
                middleLine += new string(' ', Settings.TetrisCols);
                middleLine += "║";
                middleLine += new string(' ', Settings.InfoCols);
                middleLine += "║";
                Console.Write(middleLine);
            }

            string endLine = "╚";
            endLine += new string('═', Settings.TetrisCols);
            endLine += "╩";
            endLine += new string('═', Settings.InfoCols);
            endLine += "╝";
            Console.Write(endLine);
        }

        internal static void DrawInfo()
        {
            Writer.Write("Score:", 1, 3 + Settings.TetrisCols);
            Writer.Write(StartUp.Score.ToString(), 2, 3 + Settings.TetrisCols);
            Writer.Write("Level:", 4, 3 + Settings.TetrisCols);
            Writer.Write(StartUp.Level.ToString(), 5, 3 + Settings.TetrisCols);
        }

        internal static void DrawCurrentFigure()
        {
            for (int row = 0; row < StartUp.CurrentFigure.GetLength(0); row++)
            {
                for (int col = 0; col < StartUp.CurrentFigure.GetLength(1); col++)
                {
                    if (StartUp.CurrentFigure[row, col])
                    {
                        Writer.Write("*", row + 1 + StartUp.CurrentFigureRow, col + 1 + StartUp.CurrentFigureCol);
                    }
                }
            }
        }

        internal static void DrawTetrisField()
        {
            for (int row = 0; row < StartUp.TetrisField.GetLength(0); row++)
            {
                for (int col = 0; col < StartUp.TetrisField.GetLength(1); col++)
                {
                    if (StartUp.TetrisField[row, col])
                    {
                        Writer.Write("*", row + 1, col + 1);
                    }
                }
            }
        }
    }
}