using AdventOfCode._2022.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Solutions.Day4
{
    internal class ClampCleanup : ISolution
    {
        public string GetName()
        {
            return "--- Day 4: Camp Cleanup ---";
        }

        public void Run()
        {
            var input = File.ReadAllText("./Solutions/Day4/input-01.txt");

            // part 1
            var normalizedInput = input.Replace("\r\n", "\n");

            var result = 0;
            foreach(var group in normalizedInput.Split("\n"))
            {
                var pairs = group.Split(',');
                var sectionBoundaries = pairs.Select(p => p.Split('-').Select(b => int.Parse(b)).ToList()).ToList();
                
                if ((sectionBoundaries[0][0] >= sectionBoundaries[1][0] &&
                    sectionBoundaries[0][1] <= sectionBoundaries[1][1]) ||
                    (sectionBoundaries[1][0] >= sectionBoundaries[0][0] &&
                    sectionBoundaries[1][1] <= sectionBoundaries[0][1]))
                {
                    result++;
                }
            }

            Console.WriteLine($"[1/2] Result: {result}");

            result = 0;
            foreach(var group in normalizedInput.Split('\n'))
            {
                var pairs = group.Split(',');
                var sectionBoundaries = pairs.Select(p => p.Split('-').Select(b => int.Parse(b)).ToList()).ToList();

                var sections = sectionBoundaries.Select(x => Enumerable.Range(x[0], (x[1] - x[0]) + 1).ToList()).ToList();

                var checkSum = sections.Sum(s => s.Count);
                var range = sections.SelectMany(x => x).ToList();
                var isCrossSection = range.Distinct().Count() != checkSum;

                if (isCrossSection) result++;
            }

            Console.WriteLine($"[2/2] Result: {result}");
        }
    }
}
