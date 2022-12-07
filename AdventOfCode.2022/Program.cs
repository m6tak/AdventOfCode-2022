using AdventOfCode._2022.Infrastructure;

namespace AdventOfCode._2022
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent Of Code 2022 - Solutions");

            var solutions = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(type => typeof(ISolution).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            if(solutions.Count() == 0)
            {
                Console.WriteLine("No solutions yet. Stop fucking around and start coding.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            var solutionInstances = solutions.Select(x => (ISolution)Activator.CreateInstance(x)!);

            while(true)
            {
                Console.Clear();

                Console.WriteLine("Advent Of Code 2022 - Solutions");

                foreach (var instance in solutionInstances.Select((solution, index) => new { solution, index }))
                {
                    Console.WriteLine($"{instance.index + 1}. {instance.solution.GetName()}");
                }

                Console.Write("Select solution: ");
                var result = Console.ReadLine();

                var isValidInput = int.TryParse(result, out var selectedSolutionIndex);
                if (!isValidInput && result == "q")
                {
                    Environment.Exit(0);
                }
                else if(!isValidInput)
                {
                    continue;
                }

                var selectedSolution = solutionInstances.ElementAtOrDefault(selectedSolutionIndex - 1);
                if (selectedSolution is null) continue;

                Console.Clear();

                Console.WriteLine($"Solution for: {selectedSolution.GetName()}");
                selectedSolution.Run();
                
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}