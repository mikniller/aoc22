
using System.Drawing;
using System.Text;

internal class Day14
{
    internal static (int, int) Solve()
    {
          var lines = util.ReadFile("day14.txt").Where(l => String.IsNullOrWhiteSpace(l)==false).ToList();

          List<List<Point>> points = new List<List<Point>>();
          char[,] cave = new char[1000,1000];
          for(int i=0;i<1000;i++)
            for(int j=0;j<1000;j++)
                cave[i,j]='.';


          int sandCount = 0;
          List<List<(int x, int y)>> allPoints = new List<List<(int x,int y)>>(); 

          foreach(var l in lines) {
             var curP = new  List<(int x, int y)>();
              l.Split("->").ToList().ForEach( p => curP.Add( (Int32.Parse(p.Split(',')[0]),Int32.Parse(p.Split(',')[1]))));

             allPoints.Add(curP);
          }
          var flatten = allPoints.SelectMany(p=> p);
          int maxX=flatten.Max(m => m.x);
          int maxY=flatten.Max(m => m.y);
          int minX=flatten.Min(m => m.x);


          foreach(var cp in allPoints) 
             for(int j=0;j<cp.Count()-1;j++) 
                addLine(cave,cp[j],cp[j+1]);

   //     printCave(cave,minX-2,maxX+2,0,maxY+2);


        Func<int,int,bool> tryCell = (x,y) => cave[x,y]=='.';

        int startx=500;
        int starty=0;
        while(starty<=maxY) {
               starty++;
               
               if(tryCell(startx,starty)) {} 
               else if(tryCell(startx-1,starty)) {startx--; }
               else if(tryCell(startx+1,starty)) {startx++; }
               else {
                   cave[startx,starty-1]='o';
                   sandCount++;
                   startx=500;
                   starty=0;
               }
        }
        int s1=sandCount;
        

  //      printCave(cave,minX-2,maxX+2,0,maxY+2);

        addLine(cave,(0,maxY+2),(999,maxY+2));

      //  printCave(cave,minX-2,maxX+2,0,maxY+4);

          for(int i=0;i<1000;i++)
            for(int j=0;j<1000;j++)
                if (cave[i,j]=='o') cave[i,j]='.';

        sandCount=0;
        while(tryCell(500,0)) {
               starty++;
               
               if(tryCell(startx,starty)) {} 
               else if(tryCell(startx-1,starty)) {startx--; }
               else if(tryCell(startx+1,starty)) {startx++; }
               else {
                   cave[startx,starty-1]='o';
                   sandCount++;
                   startx=500;
                   starty=0;
               }
        }

      //  printCave(cave,minX-20,maxX+20,0,maxY+4);


        return (s1,sandCount);
    }

    private static void addLine(char[,] cave, (int x,int y) from, (int x, int y)  to) {
        int xdir = from.x-to.x == 0 ? 0 : from.x-to.x < 0 ? 1 : -1;
        int ydir = from.y-to.y == 0 ? 0 : from.y-to.y < 0 ? 1 : -1;
        while(from.x!=to.x || from.y!=to.y) {
            cave[from.x,from.y] ='#';
            from.x += xdir;
            from.y += ydir;
        }
         cave[to.x,to.y] ='#';
    }


    private static void printCave(char[,] cave, int fromX, int toX,int fromY,int toY) {

        StringBuilder b = new StringBuilder();    
        for(int y=fromY;y<toY;y++,b.AppendLine($"  {y-1}")) 
            for(int x=fromX;x<toX;x++) 
                b.Append(cave[x,y]);
        Console.WriteLine(b);
    }

}