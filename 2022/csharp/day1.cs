using Aoc.Common;

namespace Aoc._2022
{
    internal class Day1 : SolveDay2022
    {
        public override string SolvePart1() {
            return Calculate().Max()+"";
        }

        public override string SolvePart2() {
            return Calculate().OrderByDescending(s => s).Take(3).Sum()+"";
        }

        private List<Int32> Calculate() {
            List<Int32> sums = new List<Int32>();
            int curSum = 0;
            foreach (var l in _lines)
            {
                if (string.IsNullOrWhiteSpace(l))
                {
                    sums.Add (curSum);
                    curSum = 0;
                }
                else
                {
                    curSum += Int32.Parse(l);
                }
            }
            if (curSum != 0) sums.Add(curSum);
            return sums;
        }

        public override void Setup(bool isPart1) {
        }

        public override bool IsReady() => true;

    }
}
