namespace TetrisGame
{
    using System;
    using System.Threading;

    internal static class GameModes
    {
        internal static void GameOver(int score)
        {
            Console.Beep();
            Writer.Write(ConstantMsgs.GameOver, 2, Settings.TetrisCols / 3, ConsoleColor.White);
            Writer.Write(String.Format(ConstantMsgs.FinalScore, score), 3, Settings.TetrisCols / 3, ConsoleColor.Red);
            Writer.Write(ConstantMsgs.HighScoresTitle, 5, 3, ConsoleColor.White);
            Writer.Write(ConstantMsgs.Exit, 13, 1, ConsoleColor.White);

            var key = Console.ReadKey();

            while (key.Key != ConsoleKey.Spacebar)
            {
                key = Console.ReadKey();
            }
            Environment.Exit(0);
        }

        internal static void Pause()
        {
            Writer.Write(ConstantMsgs.GamePaused, 2, Settings.TetrisCols / 3, ConsoleColor.White);

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