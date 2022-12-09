internal class Day6
{
    internal static (int p1, int p2) Solve()
    {

        string content = util.ReadFile("day6.txt").First();
        int i = 3;
        do
        {
            if (content[i] == content[i - 1]) i += 3;
            else if (content[i] == content[i - 2]) i += 2;
            else if (content[i - 1] == content[i - 2]) i += 2;
            else if (content[i] == content[i - 3]) i += 1;
            else if (content[i - 1] == content[i - 3]) i += 1;
            else if (content[i - 2] == content[i - 3]) i += 1;
            else break;
        } while (i < content.Length);


        int j = 0;
        do
        {
            if (content.Skip(j++).Take(14).Distinct().Count() == 14)
                break;
        } while (true);

        return (i + 1, j + 13);
    }
}

