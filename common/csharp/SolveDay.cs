
namespace Aoc.Common
{

    public abstract class SolveDay
    {
        protected bool WriteEnabled = true;
        protected readonly int _year = 2023;
        protected Writer _writer = new NullWriter();

        protected Writer GetWriter()
        {
            return _writer ?? new NullWriter();
        }

        protected List<string> _lines = new List<string>();
        protected List<string> _linesWithoutBlank = new List<string>();

        protected string _contentAsString = "";

        public SolveDay(int year)
        {
            _year = year;
        }


        public abstract string SolvePart1();

        public abstract string SolvePart2();

        public abstract void Setup(bool isPart1);

        public abstract bool IsReady();


        public (string, string) Solve(bool isSample, int day, bool consoleOut, int runs, Writer writer )
        {
            _writer = writer;
            try
            {
                _lines = Util.ReadFile($"day{day}{(isSample ? "_sample" : "")}.txt", _year, false, consoleOut);
                _contentAsString = _lines.Aggregate("", (current, s) => current + s+"\n").Trim();
                _linesWithoutBlank = _lines.Where(l => string.IsNullOrWhiteSpace(l) == false).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Year {_year} day {day} {(isSample ? "(s)" : "")} file not found");
                return ("", "");
            }

            string res1 = "";
            try
            {
                res1 = Util.Measure($"Year {_year} day {day} part 1 {(isSample ? "(s)" : "   ")}", Setup, true, SolvePart1, runs);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Year {_year} day {day} part 1  {(isSample ? "(s)" : "   ")} failed with {ex.Message}");

            }
            string res2 = "";
            try
            {
                res2 = Util.Measure($"Year {_year} day {day} part 2 {(isSample ? "(s)" : "   ")}", Setup, true, SolvePart2, runs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Year {_year} day {day} part 2  {(isSample ? "(s)" : "   ")} failed with {ex.Message}");
            }

            return (res1, res2);

        }

     

    }
}




