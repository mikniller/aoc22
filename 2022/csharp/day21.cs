
using Aoc.Common;
namespace Aoc._2022
{

    internal class Day21 : SolveDay2022
    {
        static Dictionary<string, Monkey> Monkeys;


        public override string SolvePart1()
        {
            Calc(OrderMonkeys());

            return Monkeys["root"].Val + "";
        }

        public override string SolvePart2()
        {
            var monkeys = OrderMonkeys();
            // is humn in left or right branch ?;
            Monkey root = Monkeys["root"];
            var LeftNode = Monkeys[root.LeftNode];
            var RightNode = Monkeys[root.RightNode];
            bool isLeft = HasHuman(Monkeys[root.LeftNode]);
            Calc(monkeys);
            long ValToSearchFor = isLeft ? RightNode.Val : LeftNode.Val;
            var NodeToMatch = isLeft ? LeftNode : RightNode;
            var human = Monkeys["humn"];

            (long low, long high) mid = (0, 9999999999999);

            while (ValToSearchFor != NodeToMatch.Val)
            {
                long valToTry = (mid.Item1 + mid.Item2) / 2; // center;
                human.Val = valToTry;

                Calc(monkeys);
                //    base.WriteLine($"Trying {human.Val} gave {NodeToMatch.Val} diff = {NodeToMatch.Val - ValToSearchFor}");

                if (NodeToMatch.Val - ValToSearchFor > 0)
                    mid.low = valToTry + 1;
                else
                    mid.high = valToTry;
            }

            return human.Val + "";
        }

        public override void Setup(bool isPart1)
        {
            Monkeys = new Dictionary<string, Monkey>();
            foreach (var l in _linesWithoutBlank)
            {
                Monkey m = new Monkey();
                var parts = l.Split(' ');
                m.Name = parts[0].Replace(":", "");
                m.Val = parts.Length == 2 ? Int32.Parse(parts[1].Trim()) : 0;
                m.LeftNode = parts.Length > 2 ? parts[1].Trim() : "";
                m.RightNode = parts.Length > 2 ? parts[3].Trim() : "";
                m.Operand = parts.Length > 2 ? parts[2].Trim()[0] : ' ';
                m.IsHuman = m.Name == "humn";
                m.IsRoot = m.Name == "root";
                Monkeys.Add(m.Name, m);
            }
        }

        public override bool IsReady() => true;

        public List<Monkey> OrderMonkeys(string startMonkey = "root")
        {

            Queue<Monkey> q = new Queue<Monkey>();
            List<Monkey> inOrder = new List<Monkey>();

            Action<string> tryMonkey = (name) =>
            {
                if (name == "")
                    return;
                var mon = Monkeys[name];
                if (!mon.Seen)
                {
                    q.Enqueue(mon);
                    mon.Seen = true;
                }
            };

            q.Enqueue(Monkeys[startMonkey]);
            while (q.Any())
            {
                var m = q.Dequeue();
                inOrder.Add(m);
                tryMonkey(m.LeftNode);
                tryMonkey(m.RightNode);
            }
            inOrder.Reverse();



            return inOrder;
        }

        // requires ordered monkey list
        void Calc(List<Monkey> monkeys)
        {
            // calculate all.
            foreach (var monkey in monkeys)
            {
                if (monkey.Operand == ' ')
                    continue; // nothing to calc.
                var lm = Monkeys[monkey.LeftNode];
                var rm = Monkeys[monkey.RightNode];


                monkey.Val = monkey.Operand switch
                {
                    '*' => lm.Val * rm.Val,
                    '+' => lm.Val + rm.Val,
                    '-' => lm.Val - rm.Val,
                    '/' => lm.Val / rm.Val,
                    _ => monkey.Val
                };

            }
        }

        static bool HasHuman(Monkey start)
        {
            Queue<Monkey> q = new Queue<Monkey>();
            q.Enqueue(start);

            while (q.Any())
            {
                var mon = q.Dequeue();
                if (mon.IsHuman)
                    return true;
                if (mon.LeftNode != "")
                    q.Enqueue(Monkeys[mon.LeftNode]);
                if (mon.RightNode != "")
                    q.Enqueue(Monkeys[mon.RightNode]);
            }
            return false;
        }

    }

    public class Monkey
    {
        public string Name;

        public string LeftNode;
        public string RightNode;

        public long Val;
        public char Operand;

        public bool IsHuman = false;

        public bool IsRoot = false;

        public bool Seen = false;

        public string Print()
        {

            return $"{Name} {Val} {LeftNode} {Operand} {RightNode}";

        }

    }

}

