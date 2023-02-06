using Aoc.Common;
namespace Aoc._2022
{
    internal class Day6 : SolveDay2022
    {
        public override string SolvePart1()
        {
            int i = 3;
            var content = _lines.First();
            do
            {
                if (content[i] == content[i - 1]) i += 3;
                else if (content[i] == content[i - 2]) i += 2;
                else if (content[i - 1] == content[i - 2]) i += 2;
                else if (content[i] == content[i - 3]) i += 1;
                else if (content[i - 1] == content[i - 3]) i += 1;
                else if (content[i - 2] == content[i - 3]) i += 1;
                else break;
            } while (i < content.Length);

            return (i + 1) + "";

        }

        public override string SolvePart2()
        {
            int j = 0;
            do
            {
                if (_lines.First().Skip(j++).Take(14).Distinct().Count() == 14)
                    break;
            } while (true);
            return (j + 13) + "";
        }

        public override void Setup(bool isPart1)
        {
        }
        public override bool IsReady() => true;

    }

}
