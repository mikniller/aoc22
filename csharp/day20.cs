
using DLL;

internal class Day20
{

     static DoubleLinkedList<long> dll = new DoubleLinkedList<long>();
     static int nodeCount = 0;
     static List<Node<long>> nodeList;
    internal static (long, long) Solve()
    {
         // optimized and fucked up, doesn't work anymore...
         long r1 = util.Measure(Setup,true,P1,1);
         long r2 = util.Measure(Setup,false,P2,1);
         Console.WriteLine($"{r1} - {r2}");
         return (r1,r2);


    }

    private static  void Setup(bool isP1) {
        long mul = isP1 ? 1   : 811589153;
        List<long> codes = util.ReadFile("day20_sample.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).Select(l => long.Parse(l) * mul).ToList();
        dll.from_list(codes);
        
        nodeList = dll.AsList().ToList();
        nodeCount = nodeList.Count - 1;
    }

    private static long P1() {
        DoShiftNumbers();

        return ExtractResult();            
    }

    private static long P2() {
        
        for(int i=0;i<10;i++)
            DoShiftNumbers();

        return ExtractResult();            
    }

    private static void DoShiftNumbers() {

        for (var i = 0; i < nodeList.Count; i++) 
        {
            var n = nodeList[i];

            var moveforward = ((n.data % nodeCount) + nodeCount) % nodeCount;
            if (moveforward == 0) {
                continue;
            }

            var target = dll.forward(n,(int)moveforward);
            dll.MoveAfter(target,n);
        }
    }

    

    private static long ExtractResult() {
          var start = dll.head;
        int offset = 0;
        while(start.data!=0) {
            start = start.next;
            offset++;
        }

        var pos1 = ((1000+offset) % nodeCount);
        var pos2 = ((2000+offset) % nodeCount);
        var pos3 = ((3000+offset) % nodeCount);

      
        Console.WriteLine($"{offset} 1000 {pos1}   2000 {pos2}  3000 {pos3}");

        long val1 = dll.forward(dll.head, pos1).data;
        long val2 = dll.forward(dll.head,pos2).data;
        long val3 = dll.forward(dll.head,pos3).data;
        
        Console.WriteLine($"{offset} 1000 {val1}   2000 {val2}  3000 {val3}");
        return val1+val2+val3;
    }

}

