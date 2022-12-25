using AdventOfCode._2022.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Solutions.Day3
{
    internal class RucksackReorganization : ISolution
    {
        public string GetName()
        {
            return "--- Day 3: Rucksack Reorganization ---";
        }

        public void Run()
        {
            var input = File.ReadAllText("./Solutions/Day3/input-01.txt");

            // part 1
            var normalizedInput = input.Replace("\r\n", "\n");

            // a - 96
            // A - 64

            var total = 0;
            foreach(var bag in normalizedInput.Split("\n"))
            {
                var first = bag[..(bag.Length / 2)];
                var second = bag[(bag.Length / 2)..];

                var firstGroups = first.GroupBy(x => x);
                var secondGroups = second.GroupBy(x => x);

                var commonItem = "";
                foreach(var group in firstGroups)
                {
                    if(secondGroups.Any(x => x.Key == group.Key))
                    {
                        commonItem = group.Key.ToString();
                        break;
                    }
                }
                
                var val = Encoding.ASCII.GetBytes(commonItem)[0];
                var score = char.IsUpper(commonItem[0]) ? val - 38 : val - 96;
                total += score;
            }

            Console.WriteLine($"[1/2] Result: {total}");

            //part 2
            total = 0;
            var elfGroup = new List<string>();
            foreach(var bag in normalizedInput.Split('\n'))
            {
                elfGroup.Add(bag);
                if(elfGroup.Count == 3)
                {
                    var groups = elfGroup.SelectMany(x => x.GroupBy(y => y).Select(x => x.Key));
                    var badge = groups.GroupBy(x => x).Select(x => (x.Key, x.Count())).Where(x => x.Item2 == 3).First().Key.ToString();

                    var val = Encoding.ASCII.GetBytes(badge)[0];
                    var score = char.IsUpper(badge[0]) ? val - 38 : val - 96;
                    total += score;

                    elfGroup.Clear();
                }
            }

            Console.WriteLine($"[2/2] Result: {total}");
        }
    }
}
