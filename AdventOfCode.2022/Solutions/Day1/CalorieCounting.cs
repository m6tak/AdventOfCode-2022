using AdventOfCode._2022.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Solutions.Day1
{
    internal class CalorieCounting : ISolution
    {
        public string GetName()
        {
            return "--- Day 1: Calorie Counting ---";
        }

        public void Run()
        {
            var input = File.ReadAllText("./Solutions/Day1/input-01.txt");

            // part 1
            var normalizedInput = input.Replace("\r\n", "\n");

            var elvesStock = new List<int>();
            var currentSum = 0;
            foreach (var line in normalizedInput.Split("\n"))
            {
                if (string.IsNullOrEmpty(line))
                {
                    elvesStock.Add(currentSum);
                    currentSum = 0;
                    continue;
                }

                currentSum += int.Parse(line.Trim());
            }

            Console.WriteLine($"[1/2] Higest calories count for single elve: {elvesStock.Max()}");


            // part 2
            Console.WriteLine($"[2/2] Total calories count of top 3 elves: {elvesStock.OrderByDescending(x => x).Take(3).Sum()} ");
        }
    }
}
