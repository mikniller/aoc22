using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc.Common
{

    public static class Util
    {
        public const string BasePath = @"c:\dev\aoc22\";

        public static void Run(int day, int year, bool isSample,int runs,Writer writer)
        {
            if (Solvers.TryGetValue((day, year), out var solver))
            {
                var res = solver.Solve(isSample, day, false,runs,writer);
            }
        }

        public static void RunAllSolved(int year, int runs, Writer writer, bool runSample, bool runActual)
        {
            for (int day = 0; day <= 25; day++)
            {
                if (Solvers.TryGetValue((day, year), out var solver))
                {
                    if (solver.IsReady())
                    {
                        (string,string) res = ("","");
                        if(runSample)
                            res = solver.Solve(true, day, false, runs, writer);
                        if(runActual)
                            res = solver.Solve(false, day, false, runs, writer);
                    }
                }
            }

        } 
        public static List<string> ReadFile(string fileName,int year, bool skipBlankLines = false, bool consoleOut = false)
        {
            Console.WriteLine($"looking for {BasePath}\\{year}\\data\\{fileName}");
            
            var lines = File.ReadLines($"{BasePath}\\{year}\\data\\{fileName}").ToList();


            if (skipBlankLines)
            {
                lines = lines.Where(l => string.IsNullOrWhiteSpace(l) == false).ToList();
            }


            if (consoleOut)
            {
                foreach (var data in lines.Select((line, index) => new { line, index }))
                {
                    Console.WriteLine($"{data.index:D5}. {data.line}");
                }
            }

            return lines;
        }

        public static string ReadFileString(string fileName)
        {
            return File.ReadAllText($"{BasePath}{fileName}").Trim();
        }

        public static T Measure<T>(string info,Action<bool> setup, bool isp1, Func<T> act, int runs)
        {
            T res = default;
            List<long> mtime = new List<long>();
            var sw = new Stopwatch();
            for (int i = 0; i < runs; i++)
            {
                setup(isp1);
                sw.Reset();
                sw.Start();
                res = act.Invoke();
                sw.Stop();
                mtime.Add(sw.ElapsedMilliseconds);
            }
            Console.WriteLine($"{info}: {res.ToString().PadLeft(15)}   ==>  [{runs} Run(s), measure (ms) avg = {mtime.Average()} max={mtime.Max()} min={mtime.Min()} sum={mtime.Sum()}] ");
            return res;
        }
        public static IEnumerable<int> AllIntsInLine(string line, bool onlyPositive)
        {
            string regex = onlyPositive ? "\\d+" : "-?\\d+";
            var matches = Regex.Matches(line, regex);
            return matches.Select(m => int.Parse(m.Value));
        }

        public static int GetHashCode(int[] values)
        {
            int result = 0;
            int shift = 0;
            for (int i = 0; i < values.Length; i++)
            {
                shift = (shift + 11) % 21;
                result ^= (values[i] + 1024) << shift;
            }
            return result;
        }

        public static List<int> FindSubStringPositions(string input, string subString) {
            List<int> indexes = new List<int>();
            int index = input.IndexOf(subString);
        
            while (index != -1)
            {
                indexes.Add(index);
                index = input.IndexOf(subString, index + 1);
            }
        
            return indexes;
        }

         public static (T?,T?) FirstAndLast<T>(List<T> ts) {
            if(ts.Any())
                return (ts.First(),ts.Last());
            return (default(T),default(T));     
        }

        public static Dictionary<(int, int), SolveDay> Solvers = new Dictionary<(int, int), SolveDay>() {
            { (1,2021), new Aoc._2021.Day1() },
            { (2,2021), new Aoc._2021.Day2() },
            { (3,2021), new Aoc._2021.Day3() },
            { (4,2021), new Aoc._2021.Day4() },
            { (5,2021), new Aoc._2021.Day5() },
            { (6,2021), new Aoc._2021.Day6() },
            { (7,2021), new Aoc._2021.Day7() },
            { (8,2021), new Aoc._2021.Day8() },
            { (9,2021), new Aoc._2021.Day9() },
            { (10,2021), new Aoc._2021.Day10() },
            { (11,2021), new Aoc._2021.Day11() },
            { (12,2021), new Aoc._2021.Day12() },
            { (13,2021), new Aoc._2021.Day13() },
            { (14,2021), new Aoc._2021.Day14() },
            { (15,2021), new Aoc._2021.Day15() },
            { (16,2021), new Aoc._2021.Day16() },
            { (17,2021), new Aoc._2021.Day17() },
            { (18,2021), new Aoc._2021.Day18() },
            { (19,2021), new Aoc._2021.Day19() },
            { (20,2021), new Aoc._2021.Day20() },
            { (21,2021), new Aoc._2021.Day21() },
            { (22,2021), new Aoc._2021.Day22() },
            { (23,2021), new Aoc._2021.Day23() },
            { (24,2021), new Aoc._2021.Day24() },
            { (25,2021), new Aoc._2021.Day25() },


            { (1,2022), new Aoc._2022.Day1() },
            { (2,2022), new Aoc._2022.Day2() },
            { (3,2022), new Aoc._2022.Day3() },
            { (4,2022), new Aoc._2022.Day4() },
            { (5,2022), new Aoc._2022.Day5() },
            { (6,2022), new Aoc._2022.Day6() },
            { (7,2022), new Aoc._2022.Day7() },
            { (8,2022), new Aoc._2022.Day8() },
            { (9,2022), new Aoc._2022.Day9() },
            { (10,2022), new Aoc._2022.Day10() },
            { (11,2022), new Aoc._2022.Day11() },
            { (12,2022), new Aoc._2022.Day12() },
            { (13,2022), new Aoc._2022.Day13() },
            { (14,2022), new Aoc._2022.Day14() },
            { (15,2022), new Aoc._2022.Day15() },
            { (16,2022), new Aoc._2022.Day16() },
            { (17,2022), new Aoc._2022.Day17() },
            { (18,2022), new Aoc._2022.Day18() },
            { (19,2022), new Aoc._2022.Day19() },
            { (20,2022), new Aoc._2022.Day20() },
            { (21,2022), new Aoc._2022.Day21() },
            { (22,2022), new Aoc._2022.Day22() },
            { (23,2022), new Aoc._2022.Day23() },
            { (24,2022), new Aoc._2022.Day24() },
            { (25,2022), new Aoc._2022.Day25() },


            { (1,2023), new Aoc._2023.Day1() },
            { (2,2023), new Aoc._2023.Day2() },
            { (3,2023), new Aoc._2023.Day3() },
            { (4,2023), new Aoc._2023.Day4() },
            { (5,2023), new Aoc._2023.Day5() },
            { (6,2023), new Aoc._2023.Day6() },
            { (7,2023), new Aoc._2023.Day7() },
            { (8,2023), new Aoc._2023.Day8() },
            { (9,2023), new Aoc._2023.Day9() },
            { (10,2023), new Aoc._2023.Day10() },
            { (11,2023), new Aoc._2023.Day11() },
            { (12,2023), new Aoc._2023.Day12() },
            { (13,2023), new Aoc._2023.Day13() },
            { (14,2023), new Aoc._2023.Day14() },
            { (15,2023), new Aoc._2023.Day15() },
            { (16,2023), new Aoc._2023.Day16() },
            { (17,2023), new Aoc._2023.Day17() },
            { (18,2023), new Aoc._2023.Day18() },
            { (19,2023), new Aoc._2023.Day19() },
            { (20,2023), new Aoc._2023.Day20() },
            { (21,2023), new Aoc._2023.Day21() },
            { (22,2023), new Aoc._2023.Day22() },
            { (23,2023), new Aoc._2023.Day23() },
            { (24,2023), new Aoc._2023.Day24() },
            { (25,2023), new Aoc._2023.Day25() }


        };



    }

}
