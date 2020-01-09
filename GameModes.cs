namespace TetrisGame
{
    using System;
    using System.Threading;

    internal static class GameModes
    {
        internal static void GameOver(int score, string userName)
        {
            Writer.Write("Game Over!", 2, Settings.TetrisCols / 3, ConsoleColor.White);
            Writer.Write($"Score: {score.ToString()}", 3, Settings.TetrisCols / 3, ConsoleColor.Red);
            Writer.Write("Press Space to Exit", 5, 1, ConsoleColor.White);
            var key = Console.ReadKey();
            var highScore = new HighScore();
            highScore.Record(score, userName);

            while (key.Key != ConsoleKey.Spacebar)
            {
                key = Console.ReadKey();
            }
            Environment.Exit(0);
        }

        internal static void Pause()
        {
            Writer.Write("Game Paused", 2, Settings.TetrisCols / 3, ConsoleColor.White);
            Writer.Write("Press Esc to continue", 3, 1, ConsoleColor.White);
            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.Escape)
            {
                Thread.Sleep(1000000);
                key = Console.ReadKey();
            }
        }

        internal static void Sleep()
        {
            Thread.Sleep(Settings.SleepTime);
        }
    }
}