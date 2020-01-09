namespace TetrisGame
{
    using System;

    public static class Calculator
    {
        static readonly int ChangeLevel = 1000;

        public static int GetLevel(int score)
        {
            return Math.Min(15, 1 + (score / ChangeLevel));
        }

        public static int FramesToMoveFigure(int level)
        {
            return 21 - level;
        }
    }
}