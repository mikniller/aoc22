using System.Text.RegularExpressions;
internal class Day7
{
    internal static (ulong p1, ulong p2) Solve()
    {
        string content = util.ReadFileString("day7.txt");
        node start = new node("root", null);
        node curNode = null;
        MatchCollection matches = Regex.Matches(content, @"^\$\s(cd)\s(.*)\s|^(\d+)\s+(.*)", RegexOptions.Multiline);

        foreach (var g in matches.Select(m => m.Groups))
        {
            string val1 = g[1].Value.ToString().Trim();
            string val2 = g[2].Value.ToString().Trim();

            if (val1 == "cd") // cd match
            { // it is a cd.
                if (val2 == "/")
                    curNode = start;
                else if (val2 == "..")
                    curNode = curNode.parent;
                else
                {
                    var newNode = new node(val2, curNode);
                    curNode.children.Add(newNode);
                    curNode = newNode;
                }

            }
            else
            { // file/dir match
                string val3 = g[3].Value.ToString().Trim();
                string val4 = g[4].Value.ToString().Trim();
                ulong size = ulong.Parse(val3);
                curNode.files.Add((val4, size));
            }
        }

        start.calcSize();
        ulong req = 30000000 - (70000000 - start.size);
        var found = start.solve2(req, start);

        return (start.solve(), start.solve2(req, start).size );
    }

    private class node
    {
        public node(string n, node p)
        {
            children = new List<node>();
            files = new List<(string, ulong)>();
            parent = p;
            name = n;
            size = 0;

        }
        public ulong size;
        public List<node> children;
        public node parent;
        public string name;
        public List<(string file, ulong size)> files;


        public ulong calcSize()
        {
            this.size = (ulong)files.Sum(f => (decimal)f.size);
            this.size += (ulong)children.Sum(c => (decimal)c.calcSize());


            return this.size;
        }

        public ulong solve()
        {
            ulong res = 0;
            if (this.size < 100000)
                res += this.size;

            res += (ulong)children.Sum(c => (decimal)c.solve());


            return res;
        }

        public node solve2(ulong required, node curnode)
        {
            foreach (var c in children)
                curnode = c.solve2(required, curnode);

            if (this.size > required && this.size - required < curnode.size - required)
                return this;
            return curnode;
        }


        public string print(string spaces = "")
        {
            string msg = spaces;
            msg += $"{name} : {size}\n";
            foreach (var c in files)
            {
                msg += "  " + spaces + c.ToString() + "\n";
            }

            foreach (var c in children)
            {
                msg += c.print(spaces + "  ");
            }

            return msg;
        }
    }
}
