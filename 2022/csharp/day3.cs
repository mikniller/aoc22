using Aoc.Common;

namespace Aac._2022
{
    internal class Day3 : SolveDay
    {
        public Day3(int year)
            : base(year) { }

        Func<char, int> score = (c) => c < 'a' ? c - 'A' + 27 : c - 'a' + 1;

        public override string SolvePart1()
        {
            int sum = 0;
            for (int i = 0; i < _lines.Count; i++)
            {
                int halfSize = _lines[i].Count() / 2;
                sum += score(
                    _lines[i].Take(halfSize).Intersect(_lines[i].TakeLast(halfSize)).First()
                );
            }
            return sum + "";
        }

        public override string SolvePart2()
        {
            int sum = 0;
            for (int i = 0; i < _lines.Count; i += 3)
                sum += score(_lines[i].Intersect(_lines[i + 1].Intersect(_lines[i + 2])).First());

            return sum + "";
        }

        public override void Setup(bool isPart1) { }
    }
}
