using System.Text.RegularExpressions;
using Aoc.Common;

namespace Aac._2022
{
    internal class Day5 : SolveDay
    {
        public Day5(int year) : base(year) { }

        private string Solve(int part)
        {
            Regex r = new Regex(@"\d+", RegexOptions.Compiled);
            Stack<char>[] stack = Enumerable.Range(1, 9).Select(r => new Stack<char>()).ToArray();

            int idx = -1;
            while (_lines[++idx].Any(l => l == '[')) ;
            int startIdx = idx + 1;
            while (idx-- > 0)
            {
                for (int i = 1; i < 36; i += 4)
                    if (_linesWithoutBlank[idx][i] != ' ')
                        stack[i / 4].Push(_linesWithoutBlank[idx][i]);
            }

            for (; startIdx < _linesWithoutBlank.Count; startIdx++)
            {
                var matches = r.Matches(_linesWithoutBlank[startIdx]);
                if (matches.Count == 3)
                {
                    (int move, int from, int to) res = (int.Parse(matches[0].Value), matches[1].Value[0] - '0', matches[2].Value[0] - '0');

                    var toMove = Enumerable.Range(1, res.move).Select(s => stack[res.from - 1].Pop());
                    if (part == 1)
                        toMove.ToList().ForEach(t => stack[res.to - 1].Push(t));
                    else
                        toMove.Reverse().ToList().ForEach(t => stack[res.to - 1].Push(t));
                }
            }
            return string.Join("", stack.Select(s => s.Peek()));
        }

        public override string SolvePart1()
        {
            return Solve(1) + "";

        }

        public override string SolvePart2()
        {
            return Solve(2) + "";
        }

        public override void Setup(bool isPart1)
        {

        }
    }

}