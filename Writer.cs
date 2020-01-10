namespace TetrisGame
{
    using System;

    internal static class Writer
    {
        private static string line;

        internal static void Write(string text, int row, int col, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(col, row);
            Console.Write(text);
        }

        internal static void DrawBorder()
        {
            Console.SetCursorPosition(0, 0);
            string topLine = "╔";
            topLine += new string('═', Settings.TetrisCols);

            topLine += "╦";
            topLine += new string('═', Settings.InfoCols);
            topLine += "╗";
            Console.Write(topLine);

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

        internal static void DrawInfo(int score, int level)
        {
            Writer.Write("Score:", 1, 3 + Settings.TetrisCols);
            Writer.Write(score.ToString(), 2, 3 + Settings.TetrisCols);
            Writer.Write("Level:", 4, 3 + Settings.TetrisCols);
            Writer.Write(level.ToString(), 5, 3 + Settings.TetrisCols);
        }

        internal static void DrawCurrentFigure(bool[,] currentFigure, int figureRow, int figureCol)
        {
            for (int row = 0; row < currentFigure.GetLength(0); row++)
            {
                line = string.Empty;
                for (int col = 0; col < currentFigure.GetLength(1); col++)
                {
                    if (currentFigure[row, col])
                    {
                        line += '*';
                    }
                    else
                    {
                        line += ' '; 
                    }
                }
                Writer.Write(line, row + 1 + figureRow, 1 + figureCol);
            }
        }

        internal static void DrawTetrisField(bool[,] tetrisField)
        {
            for (int row = 0; row < tetrisField.GetLength(0); row++)
            {
                line = string.Empty;
                for (int col = 0; col < tetrisField.GetLength(1); col++)
                {
                    if (tetrisField[row, col])
                    {
                        line += '*';
                    }
                    else
                    {
                        line += ' ';
                    }
                }
                Writer.Write(line, row + 1, 1);
            }
        }
    }
}