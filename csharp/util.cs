
using System.Diagnostics;
using System.Text;

public static class util {
//     public const string BasePath = @"d:\dev\aoc\data\";
    public const string BasePath = @"c:\users\mini\aoc22\data\";
    public static List<string> ReadFile(string fileName,bool skipBlankLines=false, bool consoleOut = false)
    {
	    var lines =  File.ReadLines($"{BasePath}{fileName}").ToList();

        if(skipBlankLines) {
            lines= lines.Where(l => string.IsNullOrWhiteSpace(l)==false).ToList();
        } 


        if(consoleOut) {
            foreach(var data in lines.Select((line,index) => new {line,index} )) {
                Console.WriteLine($"{data.index:D5}. {data.line}");
            }
        }

        return lines;
    }

    public static string ReadFileString(string fileName)
    {
	    return File.ReadAllText($"{BasePath}{fileName}").Trim();
    }

    


    public static T Measure<T>(Action<bool> setup, bool isp1, Func<T> act, int runs) {
        T res=default;
        List<long> mtime=new List<long>();
        var sw = new Stopwatch();
        for(int i=0;i<runs;i++) {
            setup(isp1);
            sw.Reset();
            sw.Start();
            res = act.Invoke();
            sw.Stop();
            mtime.Add(sw.ElapsedMilliseconds);
        }
        Console.WriteLine($"Runs {runs}, avg = {mtime.Average()} max={mtime.Max()} min={mtime.Min()} sum={mtime.Sum()}");
        return res;




    }

    public static void printCave(char[,] cave, int fromX, int toX, int fromY, int toY)
    {

        StringBuilder b = new StringBuilder();
        for (int y = fromY; y < toY; y++, b.AppendLine($"  {y - 1}"))
            for (int x = fromX; x < toX; x++)
                b.Append(cave[x, y]);
        Console.WriteLine(b);
    }


}