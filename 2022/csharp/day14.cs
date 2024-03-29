using Aoc.Common;
namespace Aoc._2022
{
    internal class Day14 : SolveDay2022
    {
        char[,] cave = new char[1000, 1000];
        List<List<(int x, int y)>> allPoints = new List<List<(int x, int y)>>();

        int maxY;

        public override string SolvePart1()
        {
            int sand = 0;
            int startx = 500;
            int starty = 0;
            Func<int, int, bool> tryCell = (x, y) => cave[x, y] == '.';

            while (starty <= maxY)
            {
                starty++;
                if (tryCell(startx, starty)) { }
                else if (tryCell(startx - 1, starty)) { startx--; }
                else if (tryCell(startx + 1, starty)) { startx++; }
                else
                {
                    cave[startx, starty - 1] = 'o';
                    sand++;
                    startx = 500;
                    starty = 0;
                }
            }
            return sand + "";
        }

        public override string SolvePart2()
        {
            int sand = 0;
            int startx = 500;
            int starty = 0;
            Func<int, int, bool> tryCell = (x, y) => cave[x, y] == '.';

            while (tryCell(500, 0))
            {
                starty++;
                if (tryCell(startx, starty)) { }
                else if (tryCell(startx - 1, starty)) { startx--; }
                else if (tryCell(startx + 1, starty)) { startx++; }
                else
                {
                    cave[startx, starty - 1] = 'o';
                    sand++;
                    startx = 500;
                    starty = 0;
                }
            }
            return sand + "";
        }

        public override void Setup(bool isPart1)
        {
            cave = new char[1000, 1000];
            allPoints = new List<List<(int x, int y)>>();
            foreach (var l in _linesWithoutBlank)
            {
                var curP = new List<(int x, int y)>();
                l.Split("->").ToList().ForEach(p => curP.Add((Int32.Parse(p.Split(',')[0]), Int32.Parse(p.Split(',')[1]))));

                allPoints.Add(curP);
            }
            maxY = allPoints.SelectMany(p => p).Max(m => m.y);

            for (int i = 0; i < 1000; i++)
                for (int j = 0; j < 1000; j++)
                    cave[i, j] = '.';

            foreach (var cp in allPoints)
                for (int j = 0; j < cp.Count() - 1; j++)
                    addLine(cave, cp[j], cp[j + 1]);

            if (!isPart1)
                addLine(cave, (0, maxY + 2), (999, maxY + 2));
        }

        public override bool IsReady() => true;



        private void addLine(char[,] cave, (int x, int y) from, (int x, int y) to)
        {
            // gotta be an easier way
            int xdir = from.x - to.x == 0 ? 0 : from.x - to.x < 0 ? 1 : -1;
            int ydir = from.y - to.y == 0 ? 0 : from.y - to.y < 0 ? 1 : -1;
            while (from.x != to.x || from.y != to.y)
            {
                cave[from.x, from.y] = '#';
                from.x += xdir;
                from.y += ydir;
            }
            cave[to.x, to.y] = '#';
        }


    }

}