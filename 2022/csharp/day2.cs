using Aoc.Common;

namespace Aoc._2022
{
    internal class Day2 : SolveDay2022
    {
        Dictionary<char, int> lookup = new Dictionary<char, int>
        {
            { 'X', 1 },
            { 'Y', 2 },
            { 'Z', 3 }
        };
        Dictionary<string, int> res = new Dictionary<string, int>
        {
            { "A X", 3 },
            { "A Y", 6 },
            { "A Z", 0 },
            { "B X", 0 },
            { "B Y", 3 },
            { "B Z", 6 },
            { "C X", 6 },
            { "C Y", 0 },
            { "C Z", 3 }
        };

        Dictionary<string, string> res2 = new Dictionary<string, string>
        {
            { "A X", "A Z" },
            { "A Y", "A X" },
            { "A Z", "A Y" },
            { "B X", "B X" },
            { "B Y", "B Y" },
            { "B Z", "B Z" },
            { "C X", "C Y" },
            { "C Y", "C Z" },
            { "C Z", "C X" }
        };

        public override string SolvePart1()
        {
            return _lines.Select(c => lookup[c[2]] + res[c]).Sum() + "";
        }

        public override string SolvePart2()
        {
            return _lines.Select(c => lookup[res2[c][2]] + res[res2[c]]).Sum() + "";
        }

        public override void Setup(bool isPart1) { }

        public override bool IsReady() => true;

    }
}
