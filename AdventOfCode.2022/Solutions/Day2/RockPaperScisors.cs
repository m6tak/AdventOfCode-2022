using AdventOfCode._2022.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Solutions.Day2
{
    internal class RockPaperScisors : ISolution
    {
        public string GetName()
        {
            return "--- Day 2: Rock Paper Scissors ---";
        }

        public void Run()
        {
            var input = File.ReadAllText("./Solutions/Day2/input-01.txt");

            // part 1
            var normalizedInput = input.Replace("\r\n", "\n");

            // A - rock, B - paper, C - scissors  
            // X - rock, Y - paper, Z - scissors
            // Rock - 1, Paper - 2, Scissors - 3
            // Loss - 0, Draw - 3, Win - 6

            var normalizationDict = new Dictionary<string, List<string>>
            {
                { "Rock", new List<string> { "A", "X" } },
                { "Paper", new List<string> { "B", "Y" } },
                { "Scissors", new List<string> { "C", "Z" } }
            };

            var losingRules = new Dictionary<string, string>
            {
                { "Rock", "Paper" },
                { "Paper", "Scissors" },
                { "Scissors", "Rock" },
            };

            var scores = new Dictionary<string, int>
            {
                { "Rock", 1 },
                { "Paper", 2 },
                { "Scissors", 3 }
            };

            var totalPoints = 0;
            foreach(var gameRound in normalizedInput.Split("\n"))
            {
                var shapesPlayed = gameRound.Trim().Split(" ");

                var opponentShape = normalizationDict.Where(x => x.Value.Contains(shapesPlayed[0])).First().Key;
                var myShape = normalizationDict.Where(x => x.Value.Contains(shapesPlayed[1])).First().Key;

                var isRoundLost = losingRules[myShape] == opponentShape;
                var isTie = myShape == opponentShape;

                var baseScore = scores[myShape];

                if (isRoundLost)
                {
                    totalPoints += baseScore;
                    continue;
                }

                if(isTie)
                {
                    totalPoints += baseScore + 3;
                    continue;
                }

                totalPoints += baseScore + 6;
            }

            Console.WriteLine($"[1/2] Final score for this strategy is {totalPoints}");
        }
    }
}
