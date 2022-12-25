using AdventOfCode._2022.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Solutions.Day5
{
    internal class SupplyStacks : ISolution
    {
        public string GetName()
        {
            return "--- Day 5: Supply Stacks ---";
        }

        public void Run()
        {
            var input = File.ReadAllText("./Solutions/Day5/input-01.txt");

            var normalizedInput = input.Replace("\r\n", "\n");

            var lines = normalizedInput.Split("\n");

            // neat :D
            for(var part = 1; part <= 2; part ++)
            {
                var stacksInput = new List<string>();
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line)) break;

                    stacksInput.Add(line);
                }

                stacksInput.RemoveAt(stacksInput.Count - 1);

                var stacks = ParseStacks(stacksInput);

                foreach (var instruction in lines.Skip(10))
                {
                    var parts = instruction.Split(' ');
                    var count = int.Parse(parts[1]);
                    var from = int.Parse(parts[3]);
                    var to = int.Parse(parts[5]);

                    var elementsToMove = stacks[from].TakeLast(count).ToList();

                    if(part == 1) elementsToMove.Reverse();

                    stacks[to].AddRange(elementsToMove);
                    stacks[from] = stacks[from].SkipLast(count).ToList();
                }

                var result = string.Join(" ", stacks.Values.Select(x => x.Last()));

                Console.WriteLine($"[{part}/2] Result: {result}");
            }
        }

        // burn this later...
        private Dictionary<int, List<string>> ParseStacks(List<string> stacksInput)
        {
            var stacks = new Dictionary<int, List<string>>
            {
                {1, new List<string>()},
                {2, new List<string>()},
                {3, new List<string>()},
                {4, new List<string>()},
                {5, new List<string>()},
                {6, new List<string>()},
                {7, new List<string>()},
                {8, new List<string>()},
                {9, new List<string>()}
            };

            stacksInput.Reverse();

            foreach (var line in stacksInput)
            {
                var index = 1;
                var item = "";
                var skip = false;
                foreach (var c in line)
                {
                    if (skip)
                    {
                        skip = false;
                        continue;
                    }

                    item += c;

                    if (item.Length == 3)
                    {
                        if (!string.IsNullOrWhiteSpace(item)) stacks[index].Add(item);

                        skip = true;
                        item = "";
                        index++;
                    }
                }
            }

            return stacks;
        }
    }
}
