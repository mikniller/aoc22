internal class Day11
{
    internal static (ulong, ulong) Solve() {
            return (Solve(3,20),Solve(1,10000));
    }


    internal static ulong Solve(ulong div,int rounds )
    {
        List<monkey> monkeys = new List<monkey>();
        var lines = util.ReadFile("day11.txt").Where(l => String.IsNullOrWhiteSpace(l)==false).ToList();
        monkey.div=div;    
        ulong divisorSum = 1;
        for(int i=0;i<lines.Count();i++)
        {
           if(lines[i][0]=='M') {
                var items = lines[++i].Split(':')[1].Split(',').Select(p => int.Parse(p)).ToList();
                var opStr = lines[++i].Split(':')[1].Trim();
                var test = ulong.Parse(lines[++i].Split(' ').Last());
                divisorSum *=test;
                var recieverTrue = Int32.Parse(lines[++i].Split(' ').Last());
                var recieverFalse = Int32.Parse(lines[++i].Split(' ').Last());
                monkeys.Add(new monkey(items,opStr,test,recieverTrue,recieverFalse));
           }
        }

        for(int i=0;i<rounds;i++) {
            monkeys.ForEach(m => m.next(monkeys,divisorSum));
        }

        monkeys = monkeys.OrderByDescending(m=>m.count).ToList();
        var sum = (ulong)(monkeys[0].count)*(ulong)(monkeys[1].count);
        
        return (sum);
    }



    internal class monkey {
        public static ulong div = 1;
        public int count = 0;
        List<ulong > items = new List<ulong >();
        Func<ulong ,ulong ,ulong > op;
        int rt;
        int rf;
        ulong td;
        ulong oval;
        
       

        internal monkey(List<int> ims, string opStr,ulong testDivisor, int rTrue, int rFalse) {
            this.items = ims.Select(i =>(ulong)i).ToList();
            this.td=testDivisor;
            this.rt=rTrue;
            this.rf=rFalse;
            var args = opStr.Split('=')[1].Trim().Split(' ');
            
            bool hasoVal = ulong.TryParse(args[2],out oval);

            if(args[1].Trim()=="*")                  
                op = !hasoVal ? (wl,v) => (wl*wl)/div : (wl,v) => (wl*v)/div;
            else
                op = !hasoVal? (wl,v) => (wl+wl)/div : (wl,v) => (wl+v)/div;
        }


       internal void next( List<monkey> monkeys, ulong divsum) {
            count += items.Count();
            for(int i=0;i<items.Count();i++) {
                var res = op(items[i],oval) % divsum;
                int rec = (res % td)==0 ? rt : rf;
                monkeys[rec].items.Add(res);
            }

            items.Clear();    
        }
    }
}