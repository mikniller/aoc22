using Aoc.Common;

namespace Aoc._2023
{
    internal class Day1 : SolveDay2023
    {
        List<string> verbs = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        public override string SolvePart1()
        {
            return _lines.Select(l => GetVal(l)).Sum().ToString();
        }

        public override string SolvePart2()
        {
            int sum = 0;
            foreach (var l in _lines.Select(l => Convert(l)))
            {
                int curLow = 0;
                int curHigh = 0;
                int lowIndex = 999;
                int highIndex = 0;
                for (int i = 0; i < verbs.Count; i++)
                {
                    var pos = Util.FindSubStringPositions(l, verbs[i]);
                    if (pos.Any())
                    {
                        var minMax = Util.FirstAndLast(pos);
                        if (minMax.Item1 <= lowIndex)
                        {
                            lowIndex = minMax.Item1;
                            curLow = i + 1;
                        }
                        if (minMax.Item2 >= highIndex)
                        {
                            highIndex = minMax.Item2;
                            curHigh = i + 1;
                        }
                    }
                }
                sum += curLow * 10 + curHigh;
            }
            return sum.ToString();
        }

        public override void Setup(bool isPart1)
        {
        }

        public override bool IsReady() => true;

        private int GetVal(string line)
        {
            int first = line.Where(l => l >= '0' && l <= '9').First() - '0';
            int last = line.Where(l => l >= '0' && l <= '9').Last() - '0';
            return first * 10 + last;
        }

        private string Convert(string l)
        {
            for (int i = 0; i < verbs.Count; i++)
            {
                l = l.Replace(i + 1 + "", $" {verbs[i]} ");
            }
            return l;


        }
    }
}
