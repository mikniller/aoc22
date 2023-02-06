using Aoc.Common;
namespace Aoc._2022
{
    internal class Day12_2 : SolveDay2022
    {
        private int rows;
        private int cols;

        private (char c, bool visited)[,] map;

        private Queue<(int dist, int x, int y)> queue;

        private (int x, int y) source;
        private (int x, int y) target;

        public override string SolvePart1()
        {
            return run(false) + "";
        }

        public override string SolvePart2()
        {
            return run(true) + "";
        }

        public override void Setup(bool isPart1)
        {

            cols = _lines[0].Length;
            rows = _lines.Count();
            map = new (char c, bool v)[cols, rows];

            for (int x = 0; x < cols; x++)
                for (int y = 0; y < rows; y++)
                {
                    map[x, y] = (_lines[y][x], false);
                    if (map[x, y].c == 'S')
                    {
                        map[x, y].c = 'a';
                        target = (x, y);
                    }
                    if (map[x, y].c == 'E')
                    {
                        map[x, y].c = 'z';
                        source = (x, y);
                    }
                }

            for (int x = 0; x < cols; x++)
                for (int y = 0; y < rows; y++)
                    map[x, y].visited = false;
            queue = new Queue<(int dist, int x, int y)>();
            queue.Enqueue((0, source.x, source.y));
        }

        public override bool IsReady() => true;

        private static void setup(bool isP1)
        {

        }

        private  int run(bool isp2)
        {
            while (queue.Any())
            {
                (int dist, int x, int y) from = queue.Dequeue();

                foreach ((int x, int y) to in new (int, int)[] { (from.x + 1, from.y), (from.x - 1, from.y), (from.x, from.y + 1), (from.x, from.y - 1) })
                {

                    if (to.x >= cols || to.y >= rows || to.x < 0 || to.y < 0 || map[to.x, to.y].visited)
                        continue;

                    if (map[to.x, to.y].c < (map[from.x, from.y].c - 1))
                        continue;

                    if ((!isp2 && to.x == target.x && to.y == target.y) || (isp2 && map[to.x, to.y].c == 'a'))
                        return from.dist + 1;

                    map[to.x, to.y].visited = true;
                    queue.Enqueue((from.dist + 1, to.x, to.y));
                }
            }
            return -1;
        }

    }

}