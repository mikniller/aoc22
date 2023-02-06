using Aoc.Common;
using System.Linq;

namespace Aoc._2021
{
    internal class Day3 : SolveDay2021
    {
        public override string SolvePart1() {
            string gamma = "";
            string epsilon = "";
            int midt = _lines.Count() / 2;
            for (int i = 0; i < _lines[0].Length; i++) {
                int zeroes = _lines.Count(l => l[i] == '0');
                gamma += zeroes > midt ? '0' : '1';
                epsilon += zeroes < midt ? '0' : '1';
            }

            //GetWriter().WriteLine($"{gamma} {epsilon}");
            //GetWriter().WriteLine($"{Convert.ToInt32(gamma,2)} {Convert.ToInt32(epsilon,2)}");

            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2)+"";

        }

        public override string SolvePart2() {
            List<string> ox = _lines.Select(l => l).ToList();
            List<string> co = _lines.Select(l => l).ToList();

            for (int i = 0; i < _lines[0].Length; i++)
            {
                if (ox.Count>1)
                {
                    int zeroes = ox.Count(l => l[i] == '0');
                    int ones = ox.Count(l => l[i] == '1');
                    ox.RemoveAll(o => o[i] == ((ones >= zeroes) ? '0' : '1'));
                }

                if (co.Count > 1)
                {
                    int zeroes = co.Count(l => l[i] == '0');
                    int ones = co.Count(l => l[i] == '1');
                    co.RemoveAll(c => c[i] == ((zeroes <= ones) ? '1' : '0'));
                }
            }

       //     GetWriter().WriteLine($"{ox.First()} {co.First()}");

            return Convert.ToInt32(ox.First(), 2) * Convert.ToInt32(co.First(), 2) + "";
        }


        public override void Setup(bool isPart1) {
        }

        public override bool IsReady() => true;

    }
}
