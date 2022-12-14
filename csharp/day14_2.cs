
using System.Drawing;
using System.Text;

internal class Day14_2
{
    static HashSet<(int x,int y)> vals = new HashSet<(int x, int y)>();
    static List<List<(int x, int y)>> allPoints = new List<List<(int x, int y)>>();

    static int maxY;

    internal static (int, int) Solve()
    {
        var lines = util.ReadFile("day14.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();


        foreach (var l in lines)
        {
            var curP = new List<(int x, int y)>();
            l.Split("->").ToList().ForEach(p => curP.Add((Int32.Parse(p.Split(',')[0]), Int32.Parse(p.Split(',')[1]))));

            allPoints.Add(curP);
        }
        maxY = allPoints.SelectMany(p => p).Max(m => m.y);

        int s1 = util.Measure(setup,true,run1,1);
        int s2 = util.Measure(setup,false,run2,1);

        return (s1, s2);
    }


    private static void setup(bool isP1)
    {
        vals.Clear();

        foreach (var cp in allPoints)
            for (int j = 0; j < cp.Count() - 1; j++)
                addLine(cp[j], cp[j + 1]);

        if (!isP1)
            addLine((0, maxY + 2), (999, maxY + 2));

    }

    private static int run1()
    {
        int startx = 500;
        int starty = 0;
        Func<(int, int), bool> tryCell = (p) => vals.Contains(p)==false;
        int cnt = vals.Count();
        while (starty <= maxY)
        {
            starty++;
            if (tryCell((startx, starty))) { }
            else if (tryCell((startx - 1, starty))) { startx--; }
            else if (tryCell((startx + 1, starty))) { startx++; }
            else
            {
                vals.Add((startx,starty-1));
                startx = 500;
                starty = 0;
            }
        }
        return vals.Count()-cnt;
    }

    private static int run2()
    {
        
        int startx = 500;
        int starty = 0;
        Func<(int, int), bool> tryCell = (p) => vals.Contains(p)==false;
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
         return vals.Count()-cnt;
    }



    private static void addLine((int x, int y) from, (int x, int y) to)
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