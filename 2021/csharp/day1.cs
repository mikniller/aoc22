using Aoc.Common;
using System.Runtime.Intrinsics;

namespace Aoc._2021
{
    internal class Day1 : SolveDay2021
    {
        private List<int> lineValues;

        public override string SolvePart1() {
            int count = 0;
            for(int i=1;i<lineValues.Count;i++)
                count += (lineValues[i]> lineValues[i-1] ? 1 : 0);

            return count+"";
        }

        public override string SolvePart2() {
            int count = 0;
            for (int i = 1; i < lineValues.Count - 2; i++)
            {
                int v1 = lineValues[i - 1] + lineValues[i] + lineValues[i + 1];
                int v2 = lineValues[i] + lineValues[i + 1] + lineValues[i + 2];
                // GetWriter().WriteLine($"{v1} {v2}");
                count += v2 > v1 ? 1 : 0; 
            }

            return count + "";

        }


        public override void Setup(bool isPart1) {
            this.lineValues = _lines.Select(l => Int32.Parse(l)).ToList();
        }

        public override bool IsReady() => true;

    }
}
