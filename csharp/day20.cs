
using DLL;

internal class Day20
{

    static DoubleLinkedList<long> dll;
    static int nodeCount = 0;
    static List<Node<long>> nodeList;
    internal static (long, long) Solve()
    {
        // works again
        long r1 = util.Measure(Setup, true, P1, 1);
        long r2 = util.Measure(Setup, false, P2, 1);
        return (r1, r2);


    }

    private static void Setup(bool isP1)
    {
        long mul = isP1 ? 1 : 811589153;
        List<long> codes = util.ReadFile("day20.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).Select(l => long.Parse(l) * mul).ToList();
        dll = new DoubleLinkedList<long>(codes);
        nodeList = dll.AsList().ToList();
        nodeCount = nodeList.Count - 1;
    }

    private static long P1()
    {
        DoShiftNumbers();

        return ExtractResult();
    }

    private static long P2()
    {

        for (int i = 0; i < 10; i++)
            DoShiftNumbers();

        return ExtractResult();
    }

    private static void DoShiftNumbers()
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
            //    Console.WriteLine(dll.Print(n));
        }
    }



    private static long ExtractResult()
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

