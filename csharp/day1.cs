
internal class Day1 {
    internal static (int p1 ,int p2) Solve() {

        List<string> content = util.ReadFile("day1.txt");
	    List<Int32> sums = new List<Int32>();

	    int curSum = 0;
	    foreach (var l in content)
	    {
		    if (string.IsNullOrWhiteSpace(l))
		    {
			    sums.Add(curSum);
			    curSum = 0;
		    }
		    else
		    {
			    curSum += Int32.Parse(l);
		    }

	}
	if (curSum != 0)
		sums.Add(curSum);

	return (sums.Max(),sums.OrderByDescending(s => s).Take(3).Sum());
    }
}