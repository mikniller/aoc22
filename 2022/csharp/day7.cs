using System.Text.RegularExpressions;
using Aoc.Common;
namespace Aoc._2022
{
    internal class Day7 : SolveDay2022
    {
        Node start = new Node("root", null);

        public override string SolvePart1()
        {
            return start.solve() + "";

        }

        public override string SolvePart2()
        {
            ulong req = 30000000 - (70000000 - start.size);
            return (start.solve2(req, start).size) + "";
        }

        public override void Setup(bool isPart1)
        {
            Node curNode = null;
            MatchCollection matches = Regex.Matches(_contentAsString, @"^\$\s(cd)\s(.*)\s|^(\d+)\s+(.*)", RegexOptions.Multiline);

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
                        var newNode = new Node(val2, curNode);
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

        }

        public override bool IsReady() => true;

    }



    public class Node
    {
        public Node(string n, Node p)
        {
            children = new List<Node>();
            files = new List<(string, ulong)>();
            parent = p;
            name = n;
            size = 0;

        }
        public ulong size;
        public List<Node> children;
        public Node parent;
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

        public Node solve2(ulong required, Node curnode)
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
