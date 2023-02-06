using Aoc.Common;

namespace Aoc._2022
{
    internal class Day11 : SolveDay2022
    {
        List<Monkey> monkeys = new List<Monkey>();
        ulong divisorSum = 1;

        public override string SolvePart1()
        {
            return Solve(3, 20) + "";
        }

        public override string SolvePart2()
        {
            return Solve(1, 10000) + "";

        }

        public override void Setup(bool isPart1)
        {
            monkeys = new List<Monkey>();
            divisorSum = 1;
            for (int i = 0; i < _linesWithoutBlank.Count(); i++)
            {
                if (_linesWithoutBlank[i][0] == 'M')
                {
                    var items = _linesWithoutBlank[++i].Split(':')[1].Split(',').Select(p => int.Parse(p)).ToList();
                    var opStr = _linesWithoutBlank[++i].Split(':')[1].Trim();
                    var test = ulong.Parse(_linesWithoutBlank[++i].Split(' ').Last());
                    divisorSum *= test;
                    var recieverTrue = Int32.Parse(_linesWithoutBlank[++i].Split(' ').Last());
                    var recieverFalse = Int32.Parse(_linesWithoutBlank[++i].Split(' ').Last());
                    monkeys.Add(new Monkey(items, opStr, test, recieverTrue, recieverFalse));
                }
            }
        }

        public override bool IsReady() => true;


        internal ulong Solve(ulong div, int rounds)
        {

            for (int i = 0; i < rounds; i++)
                monkeys.ForEach(m => m.Next(monkeys, divisorSum));

            monkeys = monkeys.OrderByDescending(m => m.count).ToList();
            var sum = (ulong)(monkeys[0].count) * (ulong)(monkeys[1].count);

            return (sum);
        }



        internal class Monkey
        {
            public static ulong div = 1;
            public int count = 0;
            List<ulong> items = new List<ulong>();
            Func<ulong, ulong, ulong> op;
            int rt;
            int rf;
            ulong td;
            ulong oval;



            internal Monkey(List<int> ims, string opStr, ulong testDivisor, int rTrue, int rFalse)
            {
                this.items = ims.Select(i => (ulong)i).ToList();
                this.td = testDivisor;
                this.rt = rTrue;
                this.rf = rFalse;
                var args = opStr.Split('=')[1].Trim().Split(' ');

                bool hasoVal = ulong.TryParse(args[2], out oval);

                if (args[1].Trim() == "*")
                    op = !hasoVal ? (wl, v) => (wl * wl) / div : (wl, v) => (wl * v) / div;
                else
                    op = !hasoVal ? (wl, v) => (wl + wl) / div : (wl, v) => (wl + v) / div;
            }


            internal void Next(List<Monkey> monkeys, ulong divsum)
            {
                count += items.Count();
                for (int i = 0; i < items.Count(); i++)
                {
                    var res = op(items[i], oval) % divsum;
                    int rec = (res % td) == 0 ? rt : rf;
                    monkeys[rec].items.Add(res);
                }

                items.Clear();
            }
        }
    }

}