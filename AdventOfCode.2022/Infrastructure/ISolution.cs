using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2022.Infrastructure
{
    internal interface ISolution
    {
        public void Run();
        public string GetName();
    }
}
