using AdventOfCode._2022.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Solutions.Day7
{
    internal class NoSpaceLeftOnDevice : ISolution
    {
        public string GetName()
        {
            return "--- Day 7: No Space Left On Device ---";
        }

        public void Run()
        {
            var input = File.ReadAllText("./Solutions/Day7/input-01.txt");

            var normalizedInput = input.Replace("\r\n", "\n");

            // part 1

            var root = new Dir { Name = "/" };

            var path = new List<string> { "/" };
            var currentDir = root;

            foreach (var instruction in normalizedInput.Split('\n'))
            {
                if (instruction.StartsWith("$"))
                {
                    var parts = instruction.Split(' ').Skip(1).ToList();
                    if (parts[0] == "cd")
                    {
                        if (parts[1] == "..")
                        {
                            currentDir = currentDir.Parent;
                            path = path.SkipLast(1).ToList();
                        }
                        else
                        {
                            if(parts[1] == "/")
                            {
                                currentDir = root;
                                continue;
                            }

                            var target = currentDir.Items.Where(x => x.Name == parts[1].TrimStart('/')).First().Name;
                            currentDir = (Dir)currentDir.Items.Where(x => x.Name == target).First();
                            path.Add(target);
                        }
                    }
                }
                else
                {
                    var parts = instruction.Split(" ");

                    if (parts[0] == "dir")
                    {
                        var name = parts[1];
                        currentDir.Items.Add(new Dir { Name = name, Parent = currentDir });
                    }
                    else
                    {
                        var size = int.Parse(parts[0]);
                        var name = parts[1];

                        currentDir.Items.Add(new Item { Name = name, Size = size, Parent = currentDir });
                    }
                }
            }

            var maxSize = 100000;

            var directoriesWithMaxSize = FindDirectoriesWithMaxSize(maxSize, root);
            var totalSize = directoriesWithMaxSize.Sum(x => x.CalculateSize());

            Console.WriteLine($"[1/2] Result: {totalSize}");

            // part 2

            var totalSpace = 70000000;
            var rootSize = root.CalculateSize();
            var requiredSpace = 30000000;
            var availableSpace = totalSpace - rootSize;
            var neededSpace = requiredSpace - availableSpace;

            var allDirs = GetAllDirectories(root);

            var dir = allDirs.Where(x => x.CalculateSize() >= neededSpace).MinBy(x => x.CalculateSize());

            Console.WriteLine($"[2/2] Result: {dir.CalculateSize()}");
        }

        List<Dir> FindDirectoriesWithMaxSize(int maxSize, Dir root)
        {
            var items = new List<Dir>();

            var directories = root.Items.Where(x => x is Dir).ToList();

            items.AddRange(directories.Where(x => (x as Dir)!.CalculateSize() <= maxSize).Cast<Dir>());

            foreach(var dir in directories)
            {
                var results = FindDirectoriesWithMaxSize(maxSize, (Dir)dir);
                items.AddRange(results);
            }

            return items;
        }

        List<Dir> GetAllDirectories(Dir root)
        {
            var result = new List<Dir> { root };

            var allChildDirs = root.Items.Where(x => x is Dir).Cast<Dir>();
            result.AddRange(allChildDirs);

            foreach(var dir in allChildDirs)
            {
                result.AddRange(GetAllDirectories(dir));
            }

            return result;
        }

        abstract class FilesystemItem
        {
            public Dir Parent { get; set; }
            public string Name { get; set; }
        }

        class Dir : FilesystemItem
        {
            public List<FilesystemItem> Items { get; set; } = new List<FilesystemItem>();

            public int CalculateSize()
            {
                var fileSizes = Items.Where(x => x is Item).Sum(x => (x as Item)!.Size);
                var dirSizes = Items.Where(x => x is Dir).Sum(x => (x as Dir)!.CalculateSize());

                return fileSizes + dirSizes;
            }
        }

        class Item : FilesystemItem
        {
            public int Size { get; set; }
        }

    }
}
