
using System.Collections;

internal class Day12_2
{
    private static int rows;
    private static int cols;

    private static (char c, bool visited)[,] map;

    private static Queue<(int dist,int x,int y)> q; 

    private static (int x, int y) source;
    private static (int x, int y) target;

    private static bool isp2=false;

    internal static (int, int) Solve()
    {

        var lines = util.ReadFile("day12.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();

        cols = lines[0].Length;
        rows = lines.Count();
        map = new (char c, bool v)[cols, rows];

        for (int x = 0; x < cols; x++) 
            for (int y = 0; y < rows; y++) {
                map[x,y] = (lines[y][x],false);
                if(map[x,y].c=='S') {
                    map[x,y].c='a';
                    target = (x,y);
                }
                if(map[x,y].c=='E') {
                    map[x,y].c='z';
                    source = (x,y);
                }
            }

        int res1 = util.Measure(setup, run, 1);
        isp2=true;
        int res2 =  util.Measure(setup, run, 1);

        return (res1, res2);
    }

    private static void setup()
    {
          for (int x = 0; x < cols; x++) 
            for (int y = 0; y < rows; y++) 
                map[x,y].visited=false;
        q = new Queue<(int dist,int x,int y)>(); 
        q.Enqueue((0,source.x,source.y));
    }

    private static int run()
    {
        while(q.Any()) {
            (int dist,int x,int y) from = q.Dequeue();

        foreach ((int x, int y) to in new (int, int)[] { (from.x + 1, from.y), (from.x - 1, from.y), (from.x, from.y + 1), (from.x, from.y - 1) })
        {

               if (to.x >= cols || to.y >= rows || to.x < 0 || to.y < 0 || map[to.x, to.y].visited)
                    continue;
               
               if(map[to.x, to.y].c < (map[from.x, from.y].c-1))
                    continue;

                if((!isp2 && to.x==target.x && to.y==target.y) ||  (isp2 && map[to.x, to.y].c=='a'))
                     return from.dist+1;
                
                map[to.x, to.y].visited=true;
                q.Enqueue((from.dist+1,to.x,to.y));
        }
        }
    return-1;
    }

}
