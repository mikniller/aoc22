
internal class Day9
{
    internal static (int, int) Solve()
    {
        return (Solve(2), Solve(10));
    }

    internal static int Solve(int knotCount)
    {
        (int x, int y)[] knots = new (int x, int y)[knotCount];
        int t = 1;

        for (t = 1; t < knotCount; t++)
            knots[t] = new(0, 0);

        HashSet<(int, int)> seen = new HashSet<(int, int)>() { (0, 0) }; // add initial pos		

        List<string> content = util.ReadFile("day9.txt");

        Func<Int32, Int32> sign = (i) => i == 0 ? 0 : (i < 0) ? -1 : 1;


        foreach (var instr in content)
        {
            var parts = instr.Split(' ');
            var amp = Int32.Parse(parts[1].Trim());
            var dir = parts[0].Trim();
            for (int i = 0; i < amp; i++)
            {
                knots[0].y += dir switch { "U" => -1, "D" => 1, _ => 0 };
                knots[0].x += dir switch { "L" => -1, "R" => 1, _ => 0 };


                for (t = 1; t < knotCount; t++)
                {
                    var dx = knots[t - 1].x - knots[t].x;
                    var dy = knots[t - 1].y - knots[t].y;

                    if (dx > 1 || dx < -1 || dy > 1 || dy < -1)
                    {
                        knots[t].x += sign(dx);
                        knots[t].y += sign(dy);
                    }
                    else
                        break;
                }

                if (t == knotCount)
                    seen.Add(knots[knotCount - 1]);
            }


        }
        return seen.Count;
    }
}