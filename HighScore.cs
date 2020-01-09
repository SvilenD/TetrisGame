namespace TetrisGame
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    internal class HighScore
    {
        private const string HighScoresTitle = "Tetris Score Board:";
        private const string DateTimeFormat = "dd/MM/yy - hh:mm:ss";
        private const string DateUserSeparator = " -> ";
        private Dictionary<string, int> topScores;

        public HighScore()
        {
            this.topScores = new Dictionary<string, int>();
        }

        internal void Record(int score, string userName)
        {
            if (File.Exists(Settings.HighScoresFilePath) == false)
            {
                File.AppendAllText(Settings.HighScoresFilePath, HighScoresTitle + Environment.NewLine + Environment.NewLine);
            }

            var text = $"{DateTime.Now.ToString(DateTimeFormat)}{DateUserSeparator}{userName} - {score}.";
            File.AppendAllText(Settings.HighScoresFilePath, text + Environment.NewLine);

            GetTopScores();
        }

        internal void GetTopScores()
        {
            var allScores = File.ReadAllLines(Settings.HighScoresFilePath);
            foreach (var score in allScores)
            {
                var match = Regex.Match(score, @"^(.+) -> (.+) - (?<score>\d+).$");
                if (match.Success)
                {
                    var userScore = int.Parse(match.Groups["score"].Value);
                    var dateName = score.Substring(0, score.ToString().Length - (userScore.ToString().Length + 3));
                    this.topScores.Add(dateName, userScore);
                }
            }

            this.topScores = topScores
                .OrderByDescending(t => t.Value)
                .Take(5)
                .ToDictionary(x => x.Key, x => x.Value);
            int row = 7;
            foreach (var kvp in topScores)
            {
                var start = DateTimeFormat.Length + DateUserSeparator.Length;
                var userName = kvp.Key.Substring(start, kvp.Key.Length - (start + 1));
                Writer.Write($"{userName} - {kvp.Value}", row, 3, ConsoleColor.White);
                row++;
            }
        }
    }
}