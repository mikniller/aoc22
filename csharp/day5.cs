using System.Text.RegularExpressions;
internal class Day5
{
	internal static (string,string)  Solve() {
			return(Solve(1),Solve(2));
		}

    private static string  Solve(int part) {
        Regex r = new Regex(@"\d+", RegexOptions.Compiled);
	    List<string> content = util.ReadFile("day5.txt",true);
	    Stack<char>[] stack = Enumerable.Range(1, 9).Select(r => new Stack<char>()).ToArray();

	    int idx = -1;
	    while (content[++idx].Any(l => l == '[')) ;
	    int startIdx = idx + 1;
	    while (idx-- > 0)
	    {
		    for (int i = 1; i < 36; i += 4)
			    if (content[idx][i] != ' ')
				    stack[i / 4].Push(content[idx][i]);
	    }

	    for (; startIdx < content.Count; startIdx++)
	    {
		    var matches = r.Matches(content[startIdx]);
		    if (matches.Count == 3)
		    {
			    (int move, int from, int to) res = (int.Parse(matches[0].Value), matches[1].Value[0] - '0', matches[2].Value[0] - '0');

			    var toMove = Enumerable.Range(1, res.move).Select(s => stack[res.from - 1].Pop());
			    if(part==1)
                    toMove.ToList().ForEach(t => stack[res.to - 1].Push(t)); 
                else
			        toMove.Reverse().ToList().ForEach(t => stack[res.to - 1].Push(t));
		    }
	    }
	    return string.Join("", stack.Select(s => s.Peek()));
    }
}