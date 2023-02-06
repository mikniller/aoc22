using Aoc.Common;
namespace Aoc._2022
{
    internal class Day8 : SolveDay2022
    {
        (short v, bool b, int sum)[,] vals;
        int rows;
        int cols;

        public override string SolvePart1()
        {
            int r = 0;
            int c = 0;
            int found = 0;
            for (r = rows - 1; r >= 0; r--)
            {
                for (c = cols - 1; c >= 0; c--)
                {
                    if (vals[r, c].v > vals[rows + 1, c].v)
                    {
                        vals[rows + 1, c].v = vals[r, c].v; // max so far 
                        vals[r, c].b = true;
                    }

                    if (vals[r, c].v > vals[r, cols + 1].v)
                    {
                        vals[r, cols + 1].v = vals[r, c].v; // max so far 
                        vals[r, c].b = true;
                    }
                    if (vals[r, c].b == true)
                        found++;
                }
            }
            return found + "";

        }

        public override string SolvePart2()
        {

            int r = 0;
            int c = 0;

            Action<int, int, int> calc = (vr, vc, treeHeight) =>
            {
                int r1 = r + vr;
                int c1 = c + vc;
                int sum = 0;
                while (r1 >= 0 && r1 < rows && c1 >= 0 && c1 < cols)
                {
                    sum++;
                    if (vals[r1, c1].v >= treeHeight)
                        break;
                    r1 = r1 + vr;
                    c1 = c1 + vc;

                };

                vals[r, c].sum *= sum;
            };

            int max = 0;
            for (r = 1; r < rows - 1; r++)
                for (c = 1; c < cols - 1; c++)
                {
                    int treeHeight = vals[r, c].v;

                    calc(0, -1, treeHeight);
                    calc(0, 1, treeHeight);

                    calc(-1, 0, treeHeight);
                    calc(1, 0, treeHeight);

                    if (vals[r, c].sum > max)
                        max = vals[r, c].sum;
                }

            return max + "";


        }

        public override void Setup(bool isPart1)
        {
            rows = _lines.Count;
            cols = _lines.First().Count();
            vals = new (short, bool, int)[rows + 2, cols + 2];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    vals[r, c] = ((short)(_lines[r][c] - '0' + 1), false, 1); // add 1 to avoid problems with '0' otherwise  

                    if (vals[r, c].v > vals[rows, c].v)
                    {
                        vals[rows, c].v = vals[r, c].v; // max so far 
                        vals[r, c].b = true;
                    }

                    if (vals[r, c].v > vals[r, cols].v)
                    {
                        vals[r, cols].v = vals[r, c].v; // max so far 
                        vals[r, c].b = true;
                    }
                    if (c == cols - 1 || r == rows - 1) // remember lasr col and row 
                        vals[r, c].b = true;
                }
            }
        }

        public override bool IsReady() => true;

    }

}