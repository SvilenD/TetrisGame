namespace TetrisGame
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    internal class HighScore
    {
        private const string MatchPattern = @"^(.+) -> (.+) - (?<score>\d+).$";
        private Dictionary<string, int> topScores;
        private int topScoresWriteRow = 7;

        public HighScore()
        {
            this.topScores = new Dictionary<string, int>();
        }

        internal void Record(int score, string userName)
        {
            if (File.Exists(Settings.HighScoresFilePath) == false)
            {
                File.AppendAllText(Settings.HighScoresFilePath, ConstantMsgs.HighScoresTitle + Environment.NewLine + Environment.NewLine);
            }

            var text = $"{DateTime.Now.ToString(ConstantMsgs.DateTimeFormat)}{ConstantMsgs.DateUserSeparator}{userName} - {score}.";
            File.AppendAllText(Settings.HighScoresFilePath, text + Environment.NewLine);

            GetTopScores();
        }

        internal void GetTopScores()
        {
            var allScores = File.ReadAllLines(Settings.HighScoresFilePath);
            foreach (var score in allScores)
            {
                var match = Regex.Match(score, MatchPattern);
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

            foreach (var kvp in topScores)
            {
                var start = ConstantMsgs.DateTimeFormat.Length + ConstantMsgs.DateUserSeparator.Length;
                var userName = kvp.Key.Substring(start, kvp.Key.Length - (start + 1));
                Writer.Write($"{userName} - {kvp.Value}", topScoresWriteRow++, 3, ConsoleColor.White);
            }
        }
    }
}