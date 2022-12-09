
internal class Day2
{
    internal static (int p1, int p2) Solve()
    {

        var lookup = new Dictionary<char, int> { { 'X', 1 }, { 'Y', 2 }, { 'Z', 3 } };
        var res = new Dictionary<string, int> {  { "A X",3}, {"A Y",6},{"A Z",0},
                                                 { "B X",0}, {"B Y",3},{"B Z",6},
                                                 { "C X",6}, {"C Y",0},{"C Z",3} };

        var res2 = new Dictionary<string, string> {
            { "A X","A Z"}, {"A Y","A X"},{"A Z","A Y"},
            { "B X","B X"}, {"B Y","B Y"},{"B Z","B Z"},
            { "C X","C Y"}, {"C Y","C Z"},{"C Z","C X"} };

        int sum = 0;
        int sum2 = 0;
        foreach (var c in util.ReadFile("day2.txt"))
        {
            sum += lookup[c[2]] + res[c];
            sum2 += lookup[res2[c][2]] + res[res2[c]];
        }

        return (sum, sum2);

    }
}