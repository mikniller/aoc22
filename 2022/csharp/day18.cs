using Aoc.Common;
namespace Aac._2022 {


internal class Day18 : SolveDay
{
    public Day18(int year) : base(year) {}
    public List<Cube> cubes = new List<Cube>();
    public List<Cube> holes = new List<Cube>();
    int freeEdgeSum = 0;
 
    public static void MarkAsConnected(Cube c, List<Cube> all) {
        
        foreach(var cc in all.Where(a => a.MarkAsConnected==false &&  a.Adjacant(c) )) {
            cc.MarkAsConnected = true;
            MarkAsConnected(cc,all);
        }
    }

        public override string SolvePart1()
        {
            return freeEdgeSum + "";
        }

        public override string SolvePart2()
        {
            int minx = cubes.Min(c => c.StartPoint.x);
            int miny = cubes.Min(c => c.StartPoint.y);
            int minz = cubes.Min(c => c.StartPoint.z);

            int maxx = cubes.Max(c => c.StartPoint.x);
            int maxy = cubes.Max(c => c.StartPoint.y);
            int maxz = cubes.Max(c => c.StartPoint.z);


            // find missing cubes.
            for (int x = minx; x <= maxx; x++)
                for (int y = miny; y <= maxy; y++)
                    for (int z = minz; z <= maxz; z++)
                    {
                        var existing = cubes.FirstOrDefault(c => c.IsSameStartPoint(x, y, z));
                        if (existing == null)
                        {
                            holes.Add(new Cube(x, y, z, (x == minx || y == miny || z == minz || x == maxx || y == maxy || z == maxz)));
                        }
                    }


            foreach (var h in holes.Where(h => h.IsBorder))
            {
                h.MarkAsConnected = true;
                MarkAsConnected(h, holes);
            }

            var trappedList = holes.Where(h => h.MarkAsConnected == false);
            int trappedSum = freeEdgeSum - cubes.Sum(c => c.AdjacantCount(trappedList));

            return trappedSum + "";
        }

        public override void Setup(bool isPart1)
        {
            cubes = new List<Cube>();
            holes = new List<Cube>();

            foreach (var l in _linesWithoutBlank)
            {
                var c = l.Split(',');
                cubes.Add(new Cube(Int32.Parse(c[0]), Int32.Parse(c[1]), Int32.Parse(c[2])));
            }

            freeEdgeSum = cubes.Sum(c => c.NotAdjacantCount(cubes));
        }
    }


    public class Cube
    {

        List<(int x, int y, int z)> Points = new List<(int x, int y, int z)>();

        public (int x, int y, int z) StartPoint;

        public bool IsBorder;

        public bool MarkAsConnected = false;

        public Cube(int x, int y, int z, bool isborder) : this(x, y, z)
        {
            IsBorder = isborder;
        }


        public Cube(int x, int y, int z)
        {
            StartPoint = (x, y, z);
            Points.Add((x, y, z)); //0
            Points.Add((x + 1, y, z)); //a
            Points.Add((x + 1, y + 1, z)); // e
            Points.Add((x + 1, y + 1, z + 1)); //f
            Points.Add((x, y + 1, z)); //h
            Points.Add((x, y + 1, z + 1)); //g
            Points.Add((x, y, z + 1)); //c
            Points.Add((x + 1, y, z + 1)); //c
        }

        public bool IsSameStartPoint(int x, int y, int z)
        {
            return StartPoint.x == x && StartPoint.y == y && StartPoint.z == z;

        }

        public bool Adjacant(Cube c)
        {
            if (c == this)
                return false;

            return c.Points.Intersect(Points).Count() == 4;
        }

        public int AdjacantCount(IEnumerable<Cube> cubes)
        {
            return cubes.Count(c => c.Adjacant(this));
        }

        public int NotAdjacantCount(IEnumerable<Cube> cubes)
        {

            return 6 - cubes.Count(c => c.Adjacant(this));
        }

        public override string ToString()
        {
            return ($"{StartPoint.x},{StartPoint.y},{StartPoint.z} {IsBorder} {MarkAsConnected}");

        }
    }

}