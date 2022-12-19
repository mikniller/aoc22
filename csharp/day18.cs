
public class Cube {

    List<(int x, int y, int z)> Points = new List<(int x, int y, int z)>();

    public Cube(int x, int y, int z) {
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

}

internal class Day18
{
    public static List<Cube> cubes = new List<Cube>();
    internal static (int, int) Solve()
    {
         var lines = util.ReadFile("day18.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();

         foreach(var l in lines) {
            var c= l.Split(',');
             cubes.Add( new Cube(Int32.Parse(c[0]),Int32.Parse(c[1]),Int32.Parse(c[2])));   
         }   

        int v1 = cubes.Sum(c => c.NotAdjacantCount(cubes));


        return (v1,0);
    }
}