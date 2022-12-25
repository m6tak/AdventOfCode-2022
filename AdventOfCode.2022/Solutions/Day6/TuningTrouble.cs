using AdventOfCode._2022.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Solutions.Day6
{
    internal class TuningTrouble : ISolution
    {
        public string GetName()
        {
            return "--- Day 6: Tuning Trouble ---";
        }

        public void Run()
        {
            var input = File.ReadAllText("./Solutions/Day6/input-01.txt");

            var buffer = new List<char>();
            var result = 0;
            foreach(var (c, index) in input.Select((c, index) => (c, index + 1)))
            {
                buffer.Add(c);

                if(buffer.Count == 4)
                {
                    if(buffer.Distinct().Count() == 4)
                    {
                        result = index;
                        break;
                    }

                    buffer.RemoveAt(0);
                }
            } 

            Console.WriteLine($"[1/2] Result: {result}");

            buffer = new List<char>();
            result = 0;
            foreach (var (c, index) in input.Select((c, index) => (c, index + 1)))
            {
                buffer.Add(c);

                if (buffer.Count == 14)
                {
                    if (buffer.Distinct().Count() == 14)
                    {
                        result = index;
                        break;
                    }

                    buffer.RemoveAt(0);
                }
            }

            Console.WriteLine($"[2/2] Result: {result}");
        }
    }
}
