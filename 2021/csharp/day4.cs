using Aoc._2022;
using Aoc.Common;

namespace Aoc._2021
{
    internal class Day4 : SolveDay2021
    {
        List<int> Draws = new List<int>();
        List<BingoBoard> boards = new List<BingoBoard>();
        //List< List<int>> boards = new List<List<int>>();

        public override string SolvePart1()
        {
            foreach (var d in Draws)
            {
                foreach (var b in boards)
                {
                    b.Mark(d);
                    if (b.hasWon)
                    {
                        return b.Sum() * d + "";
                    }
                }
            }

            return "";
        }

        public override string SolvePart2()
        {
            foreach (var d in Draws)
            {
                foreach (var b in boards)
                {
                    b.Mark(d);
                    if (boards.All(bb => bb.hasWon))
                    {
                        return b.Sum() * d + "";
                    }
                }
            }

            return "";
        }


        public override void Setup(bool isPart1)
        {
            Draws = Util.AllIntsInLine(_linesWithoutBlank.First(), false).ToList();
            boards = new List<BingoBoard>();
            for (int i = 1; i < _linesWithoutBlank.Count; i += 5)
            {
                BingoBoard b = new BingoBoard();
                b.InitFromRows(Util.AllIntsInLine(_linesWithoutBlank[i], false).ToList(),
                    Util.AllIntsInLine(_linesWithoutBlank[i + 1], false).ToList(),
                    Util.AllIntsInLine(_linesWithoutBlank[i + 2], false).ToList(),
                    Util.AllIntsInLine(_linesWithoutBlank[i + 3], false).ToList(),
                    Util.AllIntsInLine(_linesWithoutBlank[i + 4], false).ToList());

                boards.Add(b);
            }
        }

        public override bool IsReady() => true;
    }

    public class BingoBoard
    {
        public List<int> values;
        public bool hasWon = false;

        public void InitFromRows(List<int> r1, List<int> r2, List<int> r3, List<int> r4, List<int> r5)
        {
            values = r1;
            values.AddRange(r2);
            values.AddRange(r3);
            values.AddRange(r4);
            values.AddRange(r5);
        }

        public void Mark(int val)
        {
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                {
                    if (values[y * 5 + x] == val)
                    {
                        values[y * 5 + x] += 1000;

                        if (values[y * 5] > 999 && values[y * 5 + 1] > 999 && values[y * 5 + 2] > 999 && values[y * 5 + 3] > 999 && values[y * 5 + 4] > 999)
                        {
                            hasWon = true;
                        }

                        if (values[x] > 999 && values[5 + x] > 999 && values[10 + x] > 999 && values[15 + x] > 999 && values[20 + x] > 999)
                        {
                            hasWon = true;
                        }

                    }
                }
        }

        public int Sum()
        {
            return values.Where(v => v < 999).Sum();
        }
    }
}
