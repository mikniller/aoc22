using Aoc.Common;

namespace Aac._2022
{
    internal class Day25 : SolveDay
    {
        public Day25(int year) :  base(year)
        {
        }

        public List<Snafu> Snafus;

        public override void Setup(bool IsP1)
        {
            Snafus = _lines.Select(l => new Snafu(l)).ToList();
        }

        public override string SolvePart1()
        {
            var sum = Snafus.Sum(s => s.ToLong);
            return Snafu.ToSnafu(sum);
        }

        public override string SolvePart2()
        {
            return "Done!";
        }
    }

    class Snafu
    {
        public static int BASE_NUMBER = 5;

        public List<int> SnafuValues = new List<int>();

        public String SnafuString;

        public Snafu(string s)
        {
            SnafuString = s;
            foreach (var c in s)
            {
                if (c == '-')
                    SnafuValues.Add(-1);
                else if (c == '=')
                    SnafuValues.Add(-2);
                else
                    SnafuValues.Add(c - '0');
            }
            SnafuValues.Reverse();
        }

        long? _asLong = null;

        public long ToLong
        {
            get
            {
                if (_asLong == null)
                {
                    long res = 0;
                    for (int i = 0; i < SnafuValues.Count(); i++)
                    {
                        res += (long) Math.Pow(BASE_NUMBER, i) * SnafuValues[i];
                    }
                    _asLong = res;
                }
                return _asLong.Value;
            }
        }

        public static string ToSnafu(long value)
        {
            string snafuStr = "";
            while (value != 0)
            {
                long rem = value % BASE_NUMBER;
                if (rem == 3)
                {
                    snafuStr = "=" + snafuStr;
                    value += BASE_NUMBER;
                }
                else if (rem == 4)
                {
                    snafuStr = "-" + snafuStr;
                    value += BASE_NUMBER;
                }
                else
                {
                    snafuStr = rem + "" + snafuStr;
                }
                value /= BASE_NUMBER;
            }
            return snafuStr;
        }

        public string print()
        {
            return $"{SnafuString.PadLeft(10)} = {ToLong}";
        }
    }
}
