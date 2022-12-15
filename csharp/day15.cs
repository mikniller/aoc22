
internal class Day15
{
    private static int targetRow=10;


    static HashSet<(int x,int y)> vals = new HashSet<(int x, int y)>();
    static SortedList<int,bool> ls = new System.Collections.Generic.SortedList<int,bool>();
    
    

    internal static (int, int) Solve()
    {
       // var lines = util.ReadFile("day14.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();
        var lines = util.ReadFile("day15_sample.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();

        List<(int x,int y,char t)> points = new List<(int x,int y,char t)>();

        
        foreach(var line in lines) {
           var parts = (line+":").Split('=');     
           var p1=(Int32.Parse(parts[1].Split(',')[0]),Int32.Parse(parts[2].Split(':')[0]),'S' ); 
           var p2=(Int32.Parse(parts[3].Split(',')[0]),Int32.Parse(parts[4].Split(':')[0]),'B');
           points.Add(p1);
           points.Add(p2);
           
        }

        int val=-points.Where(r => r.y==targetRow && r.t=='B').Select(r => r.x).Distinct().Count();     

        Dictionary<int,bool> res = new Dictionary<int,bool>();

        

        for(int i=0;i<points.Count();i+=2) {
            var lss = AddLine(points[i],points[i+1],2000000);
            foreach(var k in lss.Keys)
                res[k]=true;
        }
        val += res.Count();


        return (val,0);
    }

    private static  Dictionary<int,bool>  AddLine((int x, int y, char c) S, (int x, int y,char c) B,int row) {
        int dist=Math.Abs(S.y-B.y)+Math.Abs(S.x-B.x);
        Console.WriteLine($"testing {S.x},{S.y} {B.x},{B.y}");

        Dictionary<int,bool> ls = new Dictionary<int,bool>();
        Func<int,int,int> Testval2 = (x,y) => {
            if(y==row) {
                ls[x]=true;
                return 1;
            }
            return 0;
        };
      



        for(int y=0;y<=dist;y++) {
            if(S.y-y==row || S.y+y==row) {
                for(int x=0;x<=dist-y;x++) {
                    ls[S.x-x]=true;
                    ls[S.x+x]=true;
                }
            }
        }    

        return ls;


    }
}


