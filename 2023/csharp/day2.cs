using System.Text.RegularExpressions;
using Aoc.Common;
using Microsoft.VisualBasic;

namespace Aoc._2023
{
    internal class Day2 : SolveDay2023
    {
        List<Game> games = new List<Game>();
        public override string SolvePart1() {
            return games.Where(g => g.maxGreen<=13 && g.maxRed<=12 && g.maxBlue <=14).Select(g => g.id).Sum().ToString();
        }

        public override string SolvePart2() {
            return games.Sum(g => g.maxGreen *  g.maxRed * g.maxBlue).ToString();
        }

        public override void Setup(bool isPart1) {

            games = new List<Game>();

            string blueRegex = "(\\d+)\\s+blue";
            string redRegex = "(\\d+)\\s+red";
            string greenRegex = "(\\d+)\\s+green";
            foreach (var l in _linesWithoutBlank.Select(l => l.Split(':')))
            {
                Game g = new Game();
                g.id = Util.AllIntsInLine(l[0],true).First();
                
                var matches = Regex.Matches(l[1], blueRegex);
                g.maxBlue = matches.Any() ? matches.Max(m => int.Parse(m.Groups[1].Value)) : 0;    
                g.minBlue = matches.Any() ? matches.Min(m => int.Parse(m.Groups[1].Value)) : 0;    
                
                matches = Regex.Matches(l[1], redRegex);
                g.maxRed = matches.Any() ? matches.Max(m => int.Parse(m.Groups[1].Value)) : 0;
                g.minRed = matches.Any() ? matches.Min(m => int.Parse(m.Groups[1].Value)) : 0;
                
                matches = Regex.Matches(l[1], greenRegex);
                g.maxGreen = matches.Any() ? matches.Max(m => int.Parse(m.Groups[1].Value)) : 0;
                g.minGreen = matches.Any() ? matches.Min(m => int.Parse(m.Groups[1].Value)) : 0;
                games.Add(g);
            }
            
        }

        public override bool IsReady() => true;
    }
}

struct Game {
    internal int id;
    internal int maxBlue;
    internal int minBlue;
    internal int maxRed;
    internal int minRed;
    internal int maxGreen;
    internal int minGreen;
}
