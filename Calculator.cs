namespace TetrisGame
{
    using System;

    public static class Calculator
    {
        public static int GetLevel(int score)
        {
            return Math.Min(Settings.MaxLevel, 1 + (score / Settings.ChangeLevelPoints));
        }

        public static int FramesToMoveFigure(int level)
        {
            return Settings.MaxFrames - level;
        }
    }
}