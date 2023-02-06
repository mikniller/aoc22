using Aoc.Common;

namespace Aoc._2022
{
    internal class Day14_2 : SolveDay2022
    {
        HashSet<(int x, int y)> vals = new HashSet<(int x, int y)>();
        List<List<(int x, int y)>> allPoints = new List<List<(int x, int y)>>();
        int maxY;

        public override string SolvePart1()
        {
            int startx = 500;
            int starty = 0;
            Func<(int, int), bool> tryCell = (p) => vals.Contains(p) == false;
            int cnt = vals.Count();
            while (starty <= maxY)
            {
                starty++;
                if (tryCell((startx, starty))) { }
                else if (tryCell((startx - 1, starty))) { startx--; }
                else if (tryCell((startx + 1, starty))) { startx++; }
                else
                {
                    vals.Add((startx, starty - 1));
                    startx = 500;
                    starty = 0;
                }
            }
            return (vals.Count() - cnt) + "";
        }

        public override string SolvePart2()
        {
            int startx = 500;
            int starty = 0;
            Func<(int, int), bool> tryCell = (p) => vals.Contains(p) == false;
            int cnt = vals.Count();
            while (tryCell((500, 0)))
            {
                starty++;
                if (tryCell((startx, starty))) { }
                else if (tryCell((startx - 1, starty))) { startx--; }
                else if (tryCell((startx + 1, starty))) { startx++; }
                else
                {
                    vals.Add((startx, starty - 1));
                    startx = 500;
                    starty = 0;
                }
            }
            return (vals.Count() - cnt) + "";
        }

        public override void Setup(bool isPart1)
        {
            vals = new HashSet<(int x, int y)>();
            allPoints = new List<List<(int x, int y)>>();

            foreach (var l in _linesWithoutBlank)
            {
                var curP = new List<(int x, int y)>();
                l.Split("->").ToList().ForEach(p => curP.Add((Int32.Parse(p.Split(',')[0]), Int32.Parse(p.Split(',')[1]))));

                allPoints.Add(curP);
            }
            maxY = allPoints.SelectMany(p => p).Max(m => m.y);

            vals.Clear();

            foreach (var cp in allPoints)
                for (int j = 0; j < cp.Count() - 1; j++)
                    addLine(cp[j], cp[j + 1]);

            if (!isPart1)
                addLine((0, maxY + 2), (999, maxY + 2));
        }

        public override bool IsReady() => true;


        private void addLine((int x, int y) from, (int x, int y) to)
        {
            vals.Add(from);
            vals.Add(to);
            // gotta be an easier way
            int xdir = from.x - to.x == 0 ? 0 : from.x - to.x < 0 ? 1 : -1;
            int ydir = from.y - to.y == 0 ? 0 : from.y - to.y < 0 ? 1 : -1;
            while (from.x != to.x || from.y != to.y)
            {
                vals.Add((from));
                from.x += xdir;
                from.y += ydir;
            }
        }
    }
}