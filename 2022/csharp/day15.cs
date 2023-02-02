
using Aoc.Common;
namespace Aac._2022 {

public static class Day15 : SolveDay
{

    public Day15(int year) : base(year) {}
    static List<sensor> sensors = new List<sensor>();

    internal static (int, long) Solve()
    {
        // var lines = util.ReadFile("day14.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();
        var lines = util.ReadFile("day15.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();

        foreach (var line in lines)
        {
            var parts = (line + ":").Split('=');
            (int x, int y) p1 = (Int32.Parse(parts[1].Split(',')[0]), Int32.Parse(parts[2].Split(':')[0]));
            (int x, int y) p2 = (Int32.Parse(parts[3].Split(',')[0]), Int32.Parse(parts[4].Split(':')[0]));

            sensors.Add(new sensor(p1.x, p1.y, p2.x, p2.y));
        }


        int p1val = util.Measure((x) => { }, false, p1, 1);
        long p2val = util.Measure((x) => { }, false, p2, 1);
        return (p1val, p2val);

    }

    private static int p1()
    {
        int maxX = sensors.Max(s => s.maxX);
        int minX = sensors.Min(s => s.minX);

        HashSet<int> part1 = new HashSet<int>();
        for (int x = minX; x < maxX; x++)
        {
            foreach (var s in sensors)
            {
                if (s.canReach(x, 2000000) && s.IsBeacon(x, 2000000) == false)
                    part1.Add(x);
            }
        }

        return part1.Count();

    }


    private static long p2()
    {
        foreach (var s in sensors)
        {
            (int x, int y, bool b) res = s.canEdgebeReachedByOthers(sensors, 4000000);
            if (res.b == false)
            {
                return 4000000 * (long)res.x + (long)res.y;
            }
        }
        return 0;
    }

}

class sensor
{
    public int X;
    public int Y;
    public int bX;
    public int bY;
    public int distance;
    public int halfdist;

    public int maxX;
    public int maxY;
    public int minX;
    public int minY;

    public sensor(int x, int y, int bx, int by)
    {
        X = x;
        Y = y;
        bX = bx;
        bY = by;
        distance = Math.Abs(Y - bY) + Math.Abs(X - bX);
        halfdist = distance / 2;

        minX = x - distance;
        minY = y - distance;
        maxX = x + distance;
        maxY = y + distance;
    }

    public bool canReach(int x, int y)
    {
        return Math.Abs(Y - y) + Math.Abs(X - x) <= distance;
    }

    public (int x, int y, bool b) canEdgebeReachedByOthers(List<sensor> sensors, int size)
    {

        int sx = X;
        int sy = Math.Max((Y - distance) - 1, 0);
        int ey = Math.Min((Y + distance) + 1, size);

        for (int y = sy; y < ey; y++)
        {
            int x1 = X + (distance + 1) - Math.Abs(Y - y);
            bool reachable = true;
            if (x1 >= 0 && x1 <= size)
            {
                reachable = false;
                foreach (var sen in sensors.Where(s => s != this))
                {
                    if (sen.canReach(x1, y))
                    {
                        reachable = true;
                        break;
                    }
                }
            }
            if (reachable == false)
            {
                return (x1, y, false);
            }

            reachable = true;
            x1 = X - (distance + 1) - Math.Abs(Y - y);
            if (x1 >= 0 && x1 <= size)
            {
                reachable = false;
                foreach (var sen in sensors.Where(s => s != this))
                {
                    if (sen.canReach(x1, y))
                    {
                        reachable = true;
                        break;
                    }
                }
            }
            if (reachable == false)
            {
                return (x1, y, false);
            }
        }
        return (0, 0, true);


    }

    public bool IsBeacon(int x, int y)
    {
        return x == bX && y == bY;
    }


}

}


