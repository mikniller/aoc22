

public class Monkey {
    public string Name;

    public string LeftNode;
    public string RightNode;

    public long Val;

    public char Operand;

    public bool IsHuman = false;

    public bool IsRoot = false;

    public bool Seen = false;

    public string Print() {

        return $"{Name} {Val} {LeftNode} {Operand} {RightNode}";

    }


}

internal class Day21
{
    static Dictionary<string,Monkey> Monkeys = new Dictionary<string, Monkey>(); 
    static Dictionary<string,long> Shouts = new Dictionary<string, long>(); 

     internal static (long, long) Solve()
    {
        List<string> lines = util.ReadFile("day21.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();
        foreach(var l in lines) {
            Monkey m = new Monkey();
            var parts = l.Split(' ');
            m.Name = parts[0].Replace(":","");
            m.Val = parts.Length==2 ? Int32.Parse(parts[1].Trim()) : 0;
            m.LeftNode = parts.Length>2 ? parts[1].Trim() : "";
            m.RightNode = parts.Length>2 ? parts[3].Trim() : "";
            m.Operand  = parts.Length>2 ? parts[2].Trim()[0] : ' ';
            m.IsHuman = m.Name=="humn";
            m.IsRoot = m.Name=="root";
            Monkeys.Add(m.Name,m);
        }
        return (P1(OrderMonkeys()),0);
    }
      
        public static List<Monkey> OrderMonkeys() {
        
        Queue<Monkey> q = new Queue<Monkey>();
        List<Monkey> inOrder = new List<Monkey>();

        Action<string> tryMonkey = (name)  => {
           if(name=="")  
            return;
           var mon = Monkeys[name];
                if(!mon.Seen) {
                   
                    q.Enqueue(mon);
                    mon.Seen = true;
                }
        };

        q.Enqueue(Monkeys["root"]);
        while(q.Any()) {
            var m = q.Dequeue();
            inOrder.Add(m);
            tryMonkey(m.LeftNode);
            tryMonkey(m.RightNode);
        }
        inOrder.Reverse();

        // foreach(var i in inOrder) {
        //     Console.WriteLine(i.Print());
        // }

        return inOrder;
     }

     static long P1(List<Monkey> monkeys) {
            // calculate all.
            foreach (var monkey in monkeys) {
               if(monkey.Operand==' ') 
                continue; // nothing to calc.
                var lm = Monkeys[monkey.LeftNode];
                var rm = Monkeys[monkey.RightNode];

                
                monkey.Val = monkey.Operand switch { 
                    '*' => lm.Val*rm.Val, 
                    '+' => lm.Val+rm.Val, 
                    '-' => lm.Val-rm.Val, 
                    '/' => lm.Val/rm.Val,
                    _ => monkey.Val 
                };
                }
            

            return Monkeys["root"].Val;
     }

}