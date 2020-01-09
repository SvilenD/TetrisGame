namespace TetrisGame
{
    using System;

    internal static class GameStates
    {
        internal static void GameOver()
        {
            Writer.Write("Game Over!", 2, Settings.TetrisCols / 3, ConsoleColor.White);
            Writer.Write($"Score: {StartUp.Score.ToString()}", 3, Settings.TetrisCols / 3, ConsoleColor.Red);
            Writer.Write("Press Space to Exit", 5, 1, ConsoleColor.White);
            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.Spacebar)
            {
                key = Console.ReadKey();
            }
            Environment.Exit(0);
        }
    }
}
