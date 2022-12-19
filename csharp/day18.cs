
public class Cube {

    List<(int x, int y, int z)> Points = new List<(int x, int y, int z)>();

    public (int x, int y, int z) StartPoint;

    public bool IsBorder;

  public Cube(int x, int y, int z, bool isborder):this(x,y,z) {
        
        IsBorder = isborder;

    }


    public Cube(int x, int y, int z) {
        StartPoint = (x,y,z);
        Points.Add((x,y,z)); //0
        Points.Add((x+1,y,z)); //a
        Points.Add((x+1,y+1,z)); // e
        Points.Add((x+1,y+1,z+1)); //f
        Points.Add((x,y+1,z)); //h
        Points.Add((x,y+1,z+1)); //g
        Points.Add((x,y,z+1)); //c
        Points.Add((x+1,y,z+1)); //c
    }

    public bool Adjacant(Cube c) {
        if(c==this)
            return false;

        return c.Points.Intersect(Points).Count()==4;
    }

    public int AdjacantCount(List<Cube> cubes) {
        return cubes.Count(c => c.Adjacant(this));
    }

    public int NotAdjacantCount(List<Cube> cubes) {
        
        return 6-cubes.Count(c => c.Adjacant(this));
    }

    public void Print() {
       Console.WriteLine($"{StartPoint.x},{StartPoint.y},{StartPoint.z} {IsBorder}");

    }

}

internal class Day18
{
    public static List<Cube> cubes = new List<Cube>();
    public static List<Cube> holes = new List<Cube>();
    internal static (int, int) Solve()
    {
         var lines = util.ReadFile("day18_sample.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();

         foreach(var l in lines) {
            var c= l.Split(',');
             cubes.Add( new Cube(Int32.Parse(c[0]),Int32.Parse(c[1]),Int32.Parse(c[2])));   
         }   

        int v1 = cubes.Sum(c => c.NotAdjacantCount(cubes));

        int minx = cubes.Min(c => c.StartPoint.x);
        int miny = cubes.Min(c => c.StartPoint.y);
        int minz = cubes.Min(c => c.StartPoint.z);

        int maxx = cubes.Max(c => c.StartPoint.x);
        int maxy = cubes.Max(c => c.StartPoint.y);
        int maxz = cubes.Max(c => c.StartPoint.z);


        // find missing cubes.
        for(int x=minx;x<=maxx;x++)
            for(int y=miny;y<=maxy;y++)
                for(int z=minz;z<=maxz;z++) 
                    if(cubes.Any(c => c.StartPoint==(x,y,z)==false) ) 
                        holes.Add(new Cube(x,y,z,(x==minx || y==miny || z==minz || x==maxx || y==maxy || z==maxz) ));

        
        foreach(var h in holes)
            h.Print();

       


        
        
        return (v1,0);

        
        

    }
}