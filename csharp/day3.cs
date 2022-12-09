

internal class Day3
{
    internal static (int p1, int p2) Solve()
    {
        int sum = 0;
        int sum1= 0;
	    List<string> content = util.ReadFile("day3.txt");

        Func<char,int> score = (c) => c < 'a' ? c - 'A' + 27 : c - 'a' + 1;

	    for (int i = 0; i < content.Count; i++)
	    {   
            int halfSize = content[i].Count()/2;
            sum +=score (content[i].Take(halfSize).Intersect(content[i].TakeLast(halfSize)).First());
        }
	
	    for (int i = 0; i < content.Count; i += 3)
		    sum1 += score ( content[i].Intersect(content[i + 1].Intersect(content[i + 2])).First() );

        return (sum,sum1);
    }
}
