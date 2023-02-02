
using System.Text;
using Aoc.Common;

namespace Aac._2022 {
internal class Day24 : SolveDay
{
    
    public Day24(int year) : base(year) {}
    public List<Dictionary<(int x, int y), Blizzard>> blizzardStates;
    public (int x, int y) startPoint = (1, 0);
    public (int x, int y) endPoint = (6, 6);

    public List<Blizzard> allBlizzards;

    public int height;
    public int width;

    public override void Setup(bool p1) {

        List<string> lines = _content;
        height = lines.Count() - 1;
        width = lines[0].Count() - 1;
        endPoint = (width - 1, height);
        
        allBlizzards = new List<Blizzard>();
        blizzardStates = new List<Dictionary<(int x, int y), Blizzard>>();
        blizzardStates.Add(new Dictionary<(int x, int y), Blizzard>());

        for (int y = 1; y < height; y++)
        {
            for (int x = 1; x < width; x++)
            {
                var b = Blizzard.Create(x, y,lines[y][x]);
                if (b !=null)
                {
                    allBlizzards.Add(b);
                    blizzardStates[0].Add((b.x, b.y), b); // first state
                }
            }
        }

        FillMap(100,false);
    }

    public override string SolvePart1() {
        return Move(startPoint,endPoint,0)+"";
    } 

     public override string SolvePart2() {
        int total = Move(startPoint,endPoint,0);
        total += Move(endPoint,startPoint,total)-total;
        total += Move(startPoint,endPoint,total)-total;
        return total+"";
    } 

   
    public int Move((int x,int y) from, (int x,int y) to, int minu) {

        Queue<ElfPos>  moveQueue = new Queue<ElfPos>();
        HashSet<string> seenStates = new HashSet<string>();

        int best = Int32.MaxValue;

        moveQueue.Enqueue( new ElfPos() {x=from.x,y=from.y,minute=minu,hasWaited = false} );

        Action<ElfPos> TryAddToQueue = (np) => { 
            if(np!=null && seenStates.Contains(np.key)==false && blizzardStates[np.minute].ContainsKey((np.x,np.y))==false) {
                    moveQueue.Enqueue(np);
                    seenStates.Add(np.key);
            }
        };

      
        while(moveQueue.Any()) {
            var lastMove = moveQueue.Dequeue();
            
            if(lastMove.minute>=best) // already found better path
                continue;

            if(lastMove.x==to.x && lastMove.y==to.y) {
                best = Math.Min(best,lastMove.minute);
            }
            
            if(lastMove.minute>= blizzardStates.Count()-1) 
                FillMap(100); // add 100 more.
            
            // try add a waiting pos unless it alread 
            TryAddToQueue( new ElfPos() { x=lastMove.x, y=lastMove.y, hasWaited=true, minute=lastMove.minute+1} );
            // try all four directions
            TryAddToQueue(lastMove.TryMove(width,height,(0,1),to,lastMove.minute+1));
            TryAddToQueue(lastMove.TryMove(width,height,(0,-1),to,lastMove.minute+1));
            TryAddToQueue(lastMove.TryMove(width,height,(1,0),to,lastMove.minute+1));
            TryAddToQueue(lastMove.TryMove(width,height,(-1,0),to,lastMove.minute+1));

        }
        return best;


    }  

    public void print((int x, int y) curPoint, int curState)
    {
        StringBuilder b = new StringBuilder();
        Dictionary<(int x, int y), Blizzard> state = blizzardStates[curState];
        Console.WriteLine($"Minute {curState}");
        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                if (curPoint == (x, y)) 
                    b.Append("E");
                else if (x == 0 || x == width || y == 0 || y == height)
                {
                    char c = '#';
                    if (startPoint == (x, y)) c = '.';
                    if (endPoint == (x, y)) c = '.';
                    b.Append(c);
                }
                else if (state.TryGetValue((x, y), out var blz))
                {
                    b.Append(blz.c);
                }
                else b.Append(".");
            }
            b.AppendLine();
        }
        Console.WriteLine(b);
    }

    public void FillMap(int minutes,bool doprint = false) 
    {
        
         for(int i=1;i<=minutes;i++) 
         {
             Dictionary<(int x, int y), Blizzard> newState = new Dictionary<(int x, int y), Blizzard>();
             blizzardStates.Add(newState);
             foreach(var b in allBlizzards) {
                b.Move(width,height);
                newState[(b.x,b.y)]=b;

            }
            if(doprint) 
                print((0,0),i);
         }
    }

}


public class Blizzard
    {

        public static Blizzard Create(int x, int y, char c)
        {
            if("><v^".IndexOf(c)==-1)
                return null;

            Blizzard b = new Blizzard();
            b.x = x;
            b.y = y;
            b.dir = c switch { '>' => (1, 0), '<' => (-1, 0), 'v' => (0, 1), '^' => (0, -1), _ => (0,0) };

            b.c = c;
            return b;
        }

        public char c;

        public int x;
        public int y;

        public (int x, int y) dir;


        public void Move(int maxX, int maxY)
        {
            x += dir.x;
            y += dir.y;

            if (x <= 0)
                x = maxX - 1;

            if (x >= maxX)
                x = 1;

            if (y <= 0)
                y = maxY - 1;

            if (y >= maxY)
                y = 1;
        }
    }

 public class ElfPos  {
        public int x;
        public int y;
        public int minute;
        public bool hasWaited;

        public string key => $"{x}_{y}_{hasWaited}_{minute}";

        public ElfPos TryMove(int maxX, int maxY, (int x,int y) vec,  (int x,int y) ep, int m)
        {
            
                int nx = x+vec.x;    
                int ny = y+vec.y;

                if((nx,ny)==ep)
                    return new ElfPos() { x=nx, y=ny, minute=m};

                if(nx<=0 || ny<=0 || ny>=maxY || nx>=maxX)
                    return null;

                return new ElfPos() { x=nx, y=ny, minute=m};
        }


    }


}