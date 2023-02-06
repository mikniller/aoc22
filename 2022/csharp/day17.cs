
using Aoc.Common;
using System.Text;

namespace Aoc._2022
{

    internal class Day17 : SolveDay2022
    {

        Shape shape1 = new Shape(1, 4, new int[,] { { 1, 1, 1, 1 } });
        Shape shape2 = new Shape(3, 3, new int[,]{{0,1,0},
                                                {1,1,1},
                                                {0,1,0} });
        Shape shape3 = new Shape(3, 3, new int[,]{{0,0,1},
                                                {0,0,1},
                                                {1,1,1}});
        Shape shape4 = new Shape(4, 1, new int[,]{{1},
                                                {1},
                                                {1},
                                                {1}});

        Shape shape5 = new Shape(2, 2, new int[,]{{1,1},
                                                {1,1} });


        List<Shape> shapes;
        List<int> dirs;


        public override string SolvePart1()
        {
            return DoSolve(2022) + "";
        }

        public override string SolvePart2()
        {
            return "";
            //return DoSolve(1000000000000) + "";
        }

        public override void Setup(bool isPart1)
        {
            dirs = _linesWithoutBlank[0].Select(l => l == '<' ? -1 : 1).ToList();
            shapes = new List<Shape>() { shape1, shape2, shape3, shape4, shape5 };
        }

        public override bool IsReady() => true;



        public long DoSolve(long runs)
        {
            Board b = new Board(7, 1);

            long cnt = 0;
            int curY = 0;
            int curX = 2;
            int gp = 0;

            while (cnt < runs)
            {
                var next = shapes[(int)(cnt % 5)];
                cnt++;

                if (cnt % (1000000000000 / 1000) == 0)
                    GetWriter().Write(".");

                b.RemoveFromTop();
                b.RemoveFromBottom();

                for (int t = 0; t < (3 + next.height); t++)
                {
                    b.AddEmptyRow();
                }
                curY = 0;
                curX = 2;

                while (curY < b.height && b.CanPosition(curX, curY, next))
                {
                    int movement = dirs[gp % dirs.Count()];
                    curX += movement;

                    if (b.CanPosition(curX, curY, next) == false)
                        curX -= dirs[gp % dirs.Count()];

                    curY++;
                    gp++;


                }
                curY = curY - 1;



                b.DoPosition(curX, curY, next);




            }
            b.RemoveFromTop();


            return b.height + b.removed;

        }


    }

    public class Board
    {
        public int height;
        public int width;

        public int removed = 0;

        public List<int[]> pattern = new List<int[]>();

        public Board(int w, int h)
        {
            width = w;
            height = h;
            for (int i = 0; i < h; i++)
                pattern.Add(new int[w]);
        }

        public bool isSet(int x, int y)
        {
            if (y >= height || x >= width || y < 0 || x < 0)
                return false;
            return pattern[y][x] == 1;
        }

        public void DoSet(int x, int y, int val)
        {
            if (y >= height || x >= width || y < 0 || x < 0)
                return;
            pattern[y][x] |= val;
        }

        public bool CanPosition(int startx, int starty, Shape s)
        {
            bool res = true;
            for (int y = 0; y < s.height && res == true; y++)
                for (int x = 0; x < s.width && res == true; x++)
                    if (startx + x < 0 || startx + x >= width || starty + y < 0 || starty + y >= height || s.isSet(x, y) && isSet(startx + x, starty + y))
                        res = false;

            return res;
        }

        public void DoPosition(int startx, int starty, Shape s)
        {
            for (int y = 0; y < s.height; y++)
                for (int x = 0; x < s.width; x++)
                    DoSet(startx + x, starty + y, s.Val(x, y));
        }

        public bool isEmpty(int idx)
        {

            return pattern[idx].Sum() == 0;
        }

        public void RemoveFromTop()
        {
            while (pattern.Count() > 0 && isEmpty(0))
                pattern.RemoveAt(0);
            // int val=EmptyRowsAtTop();
            // pattern.RemoveRange(0,val);



            height = pattern.Count();
        }

        public int EmptyRowsAtTop()
        {
            if (height < 3)
                return 0;
            int val = pattern[0].Sum() == 0 ? 1 : 0;
            val += pattern[1].Sum() == 0 ? 1 : 0;
            val += pattern[2].Sum() == 0 ? 1 : 0;
            return val;
        }

        public void RemoveFromBottom()
        {
            if (pattern.Count() > 1000)
            {
                pattern.RemoveRange(pattern.Count() - 500, 500);
                removed += 500;
                height = pattern.Count();
            }



        }



        public void AddEmptyRow()
        {
            pattern.Insert(0, new int[width]);
            height++;

        }

        public void printLine(int from, int to, int shapeX, int shapeY, Shape p, Writer writer)
        {
            StringBuilder builder = new StringBuilder();
            for (int y = from; y < to; y++)
            {
                builder.Append($"\n{y:D3}");
                for (int x = 0; x < width; x++)
                {
                    char boardp = (pattern[y][x] == 1) ? '#' : '.';
                    if (x >= shapeX && x <= shapeX + p.width && y >= shapeY && y < shapeY + p.height)
                    {
                        boardp = p.Val(x - shapeX, y - shapeY) == 0 ? boardp : boardp == '#' ? 'X' : '@';
                    }

                    builder.Append(boardp);

                }
            }
            writer.WriteLine(builder.ToString());

        }
    }



    public class Shape
    {
        public int height;
        public int width;

        public int[,] pattern;

        public Shape(int h, int w, int[,] p)
        {
            pattern = p;
            height = h;
            width = w;
        }

        public bool isSet(int x, int y)
        {
            if (y >= height || x >= width || y < 0 || x < 0)
                return false;
            return pattern[y, x] == 1;
        }

        public int Val(int x, int y)
        {
            if (y >= height || x >= width || y < 0 || x < 0)
                return 0;
            return pattern[y, x];
        }

    }



}