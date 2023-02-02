namespace Aoc.Common {

public abstract class SolveDay {

    protected readonly int _year=2022;

    protected List<string> _lines = new List<string>();
    protected List<string> _linesWithoutBlank = new List<string>();

    protected string _contentAsString = "";

    public SolveDay(int year)  {
        _year=year;
    }

    public abstract string SolvePart1();

    public abstract string SolvePart2();

    public abstract void Setup(bool isPart1);

/*
    public override string SolvePart1() {

    }

    public override string SolvePart2() {

    }

    public override void Setup(bool isPart1) {

    }

*/  


    public (string,string) Solve(bool isSample,int day, bool consoleOut = false) 
    {
        
        _lines = Util.ReadFile($"day{day}{(isSample ? "_sample" : "")}.txt",false,consoleOut);
        _contentAsString = File.ReadAllText($"day{day}{(isSample ? "_sample" : "")}.txt").Trim();
        _linesWithoutBlank = _lines.Where(l => string.IsNullOrWhiteSpace(l)==false).ToList();

        Setup(true);
        var v1 = SolvePart1();
        
        Setup(false);
        var v2 = SolvePart2();

        return (v1,v2);
    }

   

}
}




 