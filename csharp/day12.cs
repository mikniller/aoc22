
using System.Text;

internal class Day12
{
    private static int rows;
    private static int cols;
    private static List<(int x,int y)> vecs = new List< (int x,int y) >{(1,0),(-1,0),(0,1),(0,-1)};
    
    private static int min = Int32.MaxValue;

    private static char[,] hm;
    private static int[,] mvc;

    private static (int c,int r) source;
    private static (int c,int r) target;

    internal static (int, int) Solve()
    {

         var lines = util.ReadFile("day12.txt").Where(l => String.IsNullOrWhiteSpace(l)==false).ToList();

         cols = lines[0].Length;
         rows = lines.Count();
         hm = new char[cols,rows];
         mvc = new int[cols,rows];
         for(int r=0;r<rows;r++ ){
            for(int c=0;c<cols;c++) {
                hm[c,r]=lines[r][c];
                if(hm[c,r]=='E') {
                    hm[c,r]='z';
                    source = (c,r);
                }
                if(hm[c,r]=='S') {
                    hm[c,r]='a';
                    target= (c,r);
                }
            }
         }   

         climb(source.c,source.r,-1,true);
         int sum1=min;

         // reset stuff
         mvc = new int[cols,rows];
         min=Int32.MaxValue;

         climb(source.c,source.r,-1,false);

        return (sum1,min);
    }


    private static void climb(int col,int row, int cnt,bool p1) {
        cnt++;            

        if(mvc[col,row]!=0 && mvc[col,row] <= cnt) // other quicker path already passed this cell 
            return;
        mvc[col,row] = cnt;    

        if((p1 && col==target.c && row==target.r ) || (!p1 &&  hm[col,row]=='a')) {
            min = Math.Min(min,cnt);
            return;
        }
        
        for(int i=0;i<vecs.Count();i++) {
            int nc=col+vecs[i].x;
            int nr=row+vecs[i].y;
            if(nc<cols && nr<rows && nc>=0 && nr >=0 && hm[col,row]<= (hm[nc,nr]+1)) 
                climb(nc,nr,cnt,p1);  
        }

    }



}
