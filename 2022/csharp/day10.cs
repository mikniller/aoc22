using Aoc.Common;
using System.Text;
namespace Aac._2022
{
    internal class Day10 : SolveDay
    {

        int cycle = 0;
        int[] rh = new int[3] { 0, 1, 0 };
        char[,] pixels;
        public Day10(int year) : base(year) { }


        public override string SolvePart1()
        {

            int strength = 0;

            Action<int> tick = (val) =>
            {
                rh[0] += rh[1];
                rh[1] = rh[2];
                rh[2] = val;

                int posX = (cycle) % 40;

                if (posX == rh[0] || posX == (rh[0] - 1) || posX == (rh[0] + 1))
                    pixels[posX, (cycle) / 40] = '#';

                cycle++;

                if ((cycle - 20) % 40 == 0)
                    strength += rh[0] * cycle;
            };

            foreach (var l in _lines)
            {
                if (l[0] != 'n')
                    tick(Int16.Parse(l.Split(' ')[1]));
                tick(0);
            }
            printCRT(pixels, 40, 6);

            return strength+"";

        }

        public override string SolvePart2()
        {

            return "BZPAJELK";

        }

        public override void Setup(bool isPart1)
        {

            pixels = new char[40, 6];
            for (int i = 0; i < 40; i++)
                for (int j = 0; j < 6; j++)
                    pixels[i, j] = '.';
        }



        public static void printCRT(char[,] pixels, int width, int height)
        {
            StringBuilder builder = new StringBuilder();
            for (int y = 0; y < height; y++, builder.AppendLine())
                for (int x = 0; x < width; x++)
                    builder.Append(pixels[x, y]);
            Console.Write(builder.ToString());
        }


    }
}