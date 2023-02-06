using System.Text;
using Aoc.Common;

namespace Aoc._2022
{


    internal class Day23 : SolveDay2022
    {
        int minX() => field.Min(f => f.x);

        int maxX() => field.Max(f => f.x);

        int minY() => field.Min(f => f.y);

        int maxY() => field.Max(f => f.y);

        char[] dirs = { 'N', 'S', 'W', 'E' };

        Dictionary<(int x, int y), List<Elf>> proposed = new Dictionary<(int x, int y), List<Elf>>();

        private List<Elf> field;



        public override string SolvePart1()
        {
            int curDir = 3;

            for (int i = 0; i < 10; i++)
            {
                curDir = (curDir + 1) % 4;
                moveElves(curDir);
            }
            int cnt = 0;

            for (int y = minY(); y <= maxY(); y++)
            {
                for (int x = minX(); x <= maxX(); x++)
                {
                    cnt += Elf.IsTaken(x, y, field) ? 0 : 1;
                }
            }
            return cnt + "";
        }

        public override string SolvePart2()
        {
            int i = 0;
            while (true)
            {
                int cnt = moveElves(i % 4);
                i++;
                GetWriter().WriteLine($"Move {i} gave {cnt}");
                if (cnt == 0) break;
            }
            return i + "";
        }

        public override void Setup(bool isp1)
        {
            field = new List<Elf>();

            int y = 0;
            int cnt = 0;

            foreach (var l in _lines)
            {
                for (int x = 0; x < l.Length; x++)
                {
                    if (l[x] == '#')
                    {
                        field
                            .Add(new Elf()
                            { x = x, y = y, name = $"Elf_{x}_{y}" });
                        cnt++;
                    }
                }
                y++;
            }
        }

        public override bool IsReady() => true;

        public int moveElves(int curDir)
        {
            Action<Elf, int, int> doMove =
                (elf, x, y) =>
                {
                    if (proposed.ContainsKey((elf.x + x, elf.y + y)) == false)
                    {
                        proposed.Add((elf.x + x, elf.y + y), new List<Elf>());
                    }
                    elf.hasProposed = true;

                    proposed[(elf.x + x, elf.y + y)].Add(elf);
                };

            foreach (var elf in field)
            {
                if (elf.HasNeighbor(field))
                {
                    for (int p = 0; p < 4; p++)
                    {
                        switch (dirs[(curDir + p) % 4])
                        {
                            case 'N':
                                if (
                                    elf.hasProposed == false &&
                                    elf.CanMoveNorth(field)
                                ) doMove(elf, 0, -1);
                                break;
                            case 'S':
                                if (
                                    elf.hasProposed == false &&
                                    elf.CanMoveSouth(field)
                                ) doMove(elf, 0, 1);
                                break;
                            case 'W':
                                if (
                                    elf.hasProposed == false &&
                                    elf.CanMoveWest(field)
                                ) doMove(elf, -1, 0);
                                break;
                            case 'E':
                                if (
                                    elf.hasProposed == false &&
                                    elf.CanMoveEast(field)
                                ) doMove(elf, 1, 0);
                                break;
                        }
                    }
                }
            }
            int mvCount = 0;
            foreach (var pro in proposed)
            {
                if (pro.Value.Count() == 1)
                {
                    var moveElf =
                        field.FirstOrDefault(f => f.name == pro.Value[0].name);
                    moveElf.x = pro.Key.x;
                    moveElf.y = pro.Key.y;
                    mvCount++;
                }
            }

            proposed.Clear();
            field.ForEach(f => f.hasProposed = false);
            return mvCount;
        }

        public void print()
        {
            StringBuilder builder = new StringBuilder();
            for (int y = minY(); y <= maxY(); y++)
            {
                for (int x = minX(); x <= maxX(); x++)
                {
                    var f = field.FirstOrDefault(f => f.x == x && f.y == y);
                    char c = f == null ? '.' : '#';
                    builder.Append(c);
                }
                builder.AppendLine();
            }
            GetWriter().WriteLine(builder.ToString());
        }
    }

    class Elf
    {
        public string name;

        public int x;

        public int y;

        public bool hasProposed = false;

        public static bool IsTaken(int x, int y, List<Elf> field)
        {
            return field.Any(f => f.x == x && f.y == y);
        }

        public static bool IsTaken(Elf e, int y, List<Elf> field)
        {
            return IsTaken(e.x, e.y, field);
        }

        public bool CanMoveNorth(List<Elf> field)
        {
            if (
                Elf.IsTaken(x, y - 1, field) == false &&
                Elf.IsTaken(x - 1, y - 1, field) == false &&
                Elf.IsTaken(x + 1, y - 1, field) == false
            ) return true;
            return false;
        }

        public bool CanMoveSouth(List<Elf> field)
        {
            if (
                Elf.IsTaken(x, y + 1, field) == false &&
                Elf.IsTaken(x - 1, y + 1, field) == false &&
                Elf.IsTaken(x + 1, y + 1, field) == false
            ) return true;
            return false;
        }

        public bool CanMoveWest(List<Elf> field)
        {
            if (
                Elf.IsTaken(x - 1, y, field) == false &&
                Elf.IsTaken(x - 1, y - 1, field) == false &&
                Elf.IsTaken(x - 1, y + 1, field) == false
            ) return true;
            return false;
        }

        public bool CanMoveEast(List<Elf> field)
        {
            if (
                Elf.IsTaken(x + 1, y - 1, field) == false &&
                Elf.IsTaken(x + 1, y, field) == false &&
                Elf.IsTaken(x + 1, y + 1, field) == false
            ) return true;
            return false;
        }

        public bool HasNeighbor(List<Elf> field)
        {
            for (int fy = -1; fy <= 1; fy++)
            {
                for (int fx = -1; fx <= 1; fx++)
                {
                    if (
                        (fx == 0 && fy == 0) == false &&
                        Elf.IsTaken(x + fx, y + fy, field)
                    ) return true;
                }
            }
            return false;
        }
    }
}
