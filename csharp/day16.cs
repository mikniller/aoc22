
using System.Text;



class Graph {

    struct Node
    {
        public Node(int ver, int wei, HashSet<int> s ) {
            vertex=ver;
            weight=wei;
            seen = s;
        }
        // current vertex number and cost of the current path
        public int vertex; 
        public int weight;

        // set of nodes visited so far in the current path
        public HashSet<int> seen;
    };


    public List<List<Edge>> adjList;
    public int size;

    public Graph( List<(int from, int to, int weight)> edges,  int s, bool bothdir) {
        
        adjList =new List<List<Edge>>();
        size=s;
        
        for(int i=0;i<size;i++)
            adjList.Add(new List<Edge>());

        foreach(var e in edges)
        {
        
            adjList[e.from].Add(new Edge(e.from, e.to, e.weight));
            if(bothdir) 
                adjList[e.to].Add(new Edge(e.to,e.from,e.weight));
        }
    }

     int findMaxCost(List<Edge> edges, int src, int k, int maxLength)
     {
         Queue<Node> q = new Queue<Node>();
         HashSet<int> vertices = new HashSet<int>();
         vertices.Add(src);
         q.Enqueue(new Node(src,0,vertices));

         // stores maximum cost of a path from the source
         int maxCost =Int32.MaxValue;
    
         // loop till queue is empty
         while (q.Any())   {
            // dequeue front node
            Node node = q.Dequeue();
 
            int v = node.vertex;
            int cost = node.weight;
            vertices = new HashSet<int>(node.seen);

 
//         // if the destination is reached and BFS depth is equal to `m`,
//         // update the minimum cost calculated so far
            if (cost > k) 
                   maxCost = Math.Max(maxCost, cost);

            foreach(Edge edge in this.adjList[v])
            {
                // check for a cycle
                if (!vertices.Contains(edge.to))
                {
                    // add current node to the path
                    HashSet<int> s = new HashSet<int>(vertices);
                    s.Add(edge.to);
 
                    // push every vertex (discovered or undiscovered) into
                    // the queue with a cost equal to the
                    // parent's cost plus the current edge's weight


                    q.Enqueue(new Node(edge.from, cost + edge.weight, s));
                }
            }
     }
     return maxCost;
     }





     public void Print(Func<int,string> translate=null) {
             if(translate==null) translate = (i)=> ""+i;
             StringBuilder b = new StringBuilder();
             for(int i=0;i<size;i++)   {
                b.AppendLine($"{translate(i)} => {String.Join("   ", adjList[i].Select(x=>translate(x.to)+" "+x.weight ))}");
             }
             Console.WriteLine(b);
       }

}



internal class ParsedNode {
    
    public int vertex=0;
    
    public string name;
    public int flow;
    public List<string> neighbors = new List<string>();

    

    public void print() {
        Console.WriteLine($"{name}  flow={flow}, nodes = {string.Join(",",neighbors)}");
    }
}


internal class Edge {
    public int from;
    public int to;
    public int weight;

    public Edge(int f, int t, int w) {
        from=f;
        to=t;
        weight=w;
    }

    public void print() {
        Console.WriteLine($"{from} => {to}  weight = {weight}");
    }
}

internal class Day16 {
public static Dictionary<int,ParsedNode> parsedNodes = new Dictionary<int,ParsedNode>();
    
    internal static (int, int) Solve()
    {
        var lines = util.ReadFile("day16_sample.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();
        int i=0;
        foreach (var line in lines)
        {
           ParsedNode n =  new ParsedNode();

           string l = line.Replace("valve ","valves ");
           n.name =l.Substring(6,2).Trim();
           n.flow = Int32.Parse(l.Split('=')[1].Split(';')[0]); 
           n.neighbors = l.Split("valves")[1].Split(',').ToList().Select(t => t.Trim()).ToList(); 
           n.vertex = i++; 
           parsedNodes[n.vertex]=n;

        }

        foreach(var n in parsedNodes)
            n.Value.print();

        int cnt=0;
        List<(int from, int to, int weight)> temp = new List<(int from, int to, int weight)>();
        foreach(var n in parsedNodes) {
            foreach(var sn in n.Value.neighbors)  {
                var to = parsedNodes.Values.FirstOrDefault(v => v.name==sn);
                temp.Add( (n.Value.vertex,to.vertex,to.flow));
            }  
                cnt++;

        }


        Graph g = new Graph(temp,cnt,false);

        g.Print( (i) => { return i+" "+parsedNodes[i].name+"("+parsedNodes[i].flow+")"; } );



    return (0,0);
    }


}



// Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
// Valve BB has flow rate=13; tunnels lead to valves CC, AA
// Valve CC has flow rate=2; tunnels lead to valves DD, BB
// Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
// Valve EE has flow rate=3; tunnels lead to valves FF, DD
// Valve FF has flow rate=0; tunnels lead to valves EE, GG
// Valve GG has flow rate=0; tunnels lead to valves FF, HH
// Valve HH has flow rate=22; tunnel leads to valve GG
// Valve II has flow rate=0; tunnels lead to valves AA, JJ
// Valve JJ has flow rate=21; tunnel leads to valve II