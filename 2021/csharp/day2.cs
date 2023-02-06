using Aoc.Common;

namespace Aoc._2021
{
    internal class Day2 : SolveDay2021
    {
        List<(char dir, int val)> commands;

        (int x, int y) pos = (0, 0);
        int aim = 0;

        public override string SolvePart1() {
            foreach(var c in commands)
            {
               // GetWriter().WriteLine($"{c}");
                switch(c.dir)
                {
                    case 'f': 
                        pos.x += c.val;
                        break;
                    case 'd':
                        pos.y += c.val;
                        break;

                    case 'u':
                        pos.y -= c.val;
                        break;

                }

            }
            return (pos.x * pos.y) + "";
        }

        public override string SolvePart2() {
            foreach (var c in commands)
            {
                // GetWriter().WriteLine($"{c}");
                switch (c.dir)
                {
                    case 'f':
                        pos.x += c.val;
                        pos.y += c.val*aim;
                        break;
                    case 'd':
                        aim += c.val;
                        break;

                    case 'u':
                        aim -= c.val;
                        break;

                }

            }
            return (pos.x * pos.y) + "";
        }


        public override void Setup(bool isPart1) {
            pos = (0, 0);
            aim = 0;
            commands = _lines.Select(l => (l.Split(' ')[0][0], Int32.Parse(l.Split(' ')[1]))).ToList();

        }

        public override bool IsReady() => true;

    }
}
