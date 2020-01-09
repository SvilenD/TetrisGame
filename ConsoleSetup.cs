namespace TetrisGame
{
    using System;

    internal class ConsoleSetup
    {
        internal ConsoleSetup()
        {
            this.Setup();
        }

        private void Setup()
        {
            Console.Title = Settings.GameTitle;
            Console.CursorVisible = false;
            Console.WindowHeight = Settings.ConsoleRows + 1;
            Console.WindowWidth = Settings.ConsoleCols;
            Console.BufferHeight = Settings.ConsoleRows + 1;
            Console.BufferWidth = Settings.ConsoleCols;
        }
    }
}
