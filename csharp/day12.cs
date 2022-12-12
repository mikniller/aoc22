
internal class Day12
{
    private static int rows;
    private static int cols;
   
    private static int min = Int32.MaxValue;

    private static (char alt, int visits)[,] map;

    private static (int x,int y) source;
    private static (int x,int y) target;

    internal static (int, int) Solve()
    {

         var lines = util.ReadFile("day12.txt").Where(l => String.IsNullOrWhiteSpace(l)==false).ToList();

         cols = lines[0].Length;
         rows = lines.Count();
         map = new (char alt, int visits)[cols,rows];

         for(int y=0;y<rows;y++ ){
            for(int x=0;x<cols;x++) {
                map[x,y]=(lines[y][x],Int32.MaxValue);

                if(map[x,y].alt=='E') {
                    map[x,y].alt='z';
                    source = (x,y);
                }
                if(map[x,y].alt=='S') {
                    map[x,y].alt='a';
                    target= (x,y);
                }
            }
         }   

         climb(source.x,source.y,0,true);
         int sum1=min;

        //  // reset stuff
         for(int r=0;r<rows;r++ )
            for(int c=0;c<cols;c++) 
                map[c,r].visits=Int32.MaxValue;
         
         min=Int32.MaxValue;

         climb(source.x,source.y,0,false);

        return (sum1,min);
    }


    private static void climb(int x,int y, int cnt,bool p1) {

        if((p1 && x==target.x && y==target.y ) || (!p1 &&  map[x,y].alt=='a')) {
            min = Math.Min(min,cnt);
            return;
        }
        map[x,y].visits = (cnt++); 

        foreach((int x,int y) point in new (int,int)[]{(x+1,y),(x-1,y),(x,y+1),(x,y-1)}) {
            if(point.x<cols && point.y<rows && point.x>=0 && point.y >=0 && map[point.x,point.y].visits>cnt && map[x,y].alt<=(map[point.x,point.y].alt+1) ) 
                climb(point.x,point.y,cnt,p1);  
        }
    }
}
