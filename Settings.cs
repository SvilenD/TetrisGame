namespace TetrisGame
{
    using System.Collections.Generic;

    internal static class Settings
    {
        internal static readonly int TetrisRows = 20;
        internal static readonly int TetrisCols = 16;
        internal static readonly int InfoCols = 10;
        internal static readonly int ConsoleRows = 1 + TetrisRows + 1;
        internal static readonly int ConsoleCols = 1 + TetrisCols + 1 + InfoCols + 1;
        internal static readonly int MaxLevel = 10;
        internal static readonly int ChangeLevelPoints = 5000;
        internal static readonly int[] ScorePerLines = { 0, 20, 50, 150, 300 };
        internal static readonly int MaxFrames = 20;
        internal static readonly List<bool[,]> TetrisFigures = new List<bool[,]>()
            {
                new bool[,] // square
                {
                    { true, true },
                    { true, true }
                },
                new bool[,] // ----
                {
                    { true, true, true, true }
                },
                new bool[,] // |
                {
                    {true},
                    {true},
                    {true},
                    {true}
                },
                new bool[,] // T
                {
                  { true, true, true },
                  { false, true, false },
                },
                new bool[,] // _|_
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
        internal static readonly int SleepTime = 30;
        internal static readonly string HighScoresFilePath = "../../../HighScores.txt";
    }
}