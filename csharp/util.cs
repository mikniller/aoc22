
public static class util {
//    public const string BasePath = @"d:\dev\aoc\data\";
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
}