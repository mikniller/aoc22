
using Aoc.Common;
namespace Aoc._2022
{
    internal class Day20 : SolveDay2022
    {
        DoubleLinkedList<long> dll;
        int nodeCount = 0;
        List<Node<long>> nodeList;

        public override string SolvePart1()
        {
            DoShiftNumbers();

            return ExtractResult() + "";
        }

        public override string SolvePart2()
        {
            for (int i = 0; i < 10; i++)
                DoShiftNumbers();

            return ExtractResult() + "";
        }


        public override void Setup(bool isPart1)
        {
            long mul = isPart1 ? 1 : 811589153;
            List<long> codes = _linesWithoutBlank.Select(l => long.Parse(l) * mul).ToList();
            dll = new DoubleLinkedList<long>(codes);
            nodeList = dll.AsList().ToList();
            nodeCount = nodeList.Count - 1;
        }

        public override bool IsReady() => true;


        private void DoShiftNumbers()
        {

            for (var i = 0; i < nodeList.Count; i++)
            {
                var n = nodeList[i];

                var moveforward = ((n.data % nodeCount) + nodeCount) % nodeCount;
                if (moveforward == 0)
                {
                    continue;
                }

                var target = dll.Forward(n, (int)moveforward);
                dll.Remove(n);
                dll.AddAfter(target, n);
                //    base.WriteLine(dll.Print(n));
            }
        }



        private long ExtractResult()
        {
            var start = dll.head;
            int offset = 0;
            while (start.data != 0)
            {
                start = start.next;
                offset++;
            }

            long val1 = dll.Forward(start, 1000).data;
            long val2 = dll.Forward(start, 2000).data;
            long val3 = dll.Forward(start, 3000).data;

            return val1 + val2 + val3;
        }


    }

}