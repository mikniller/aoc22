

class Snafu {
    
    public static int BASE_NUMBER = 5;    

    public List<int> SnafuValues = new List<int>();

    public String SnafuString;

    public Snafu(string s) {
        SnafuString =s;
        foreach(var c in s) {
            if(c=='-') 
                SnafuValues.Add(-1);
            else if(c=='=')         
                SnafuValues.Add(-2);       
            else SnafuValues.Add(c-'0');
        }
        SnafuValues.Reverse();
    }

    long? _asLong = null;
    
    public long ToLong {
        get {
            if(_asLong==null) {
                long res = 0;
                for(int i=0;i<SnafuValues.Count();i++)     {
                    res += (long)Math.Pow(BASE_NUMBER,i)*SnafuValues[i];
                }
                _asLong =res;
            }
            return _asLong.Value;
        }
    }

    public static string ToSnafu(long value) 
    {
        string snafuStr = "";
        while(value!=0) { 
            long rem = value % BASE_NUMBER;
            if(rem==3) {
                snafuStr += '='; value+=BASE_NUMBER;
            } 
            else if(rem==4) {
                snafuStr += '-'; value+=5;
            }
            else {
                snafuStr += ""+rem;
            }
            value/=BASE_NUMBER;
        }
        var reversed = snafuStr.ToCharArray().Reverse();

        return string.Join("",reversed);
    }

    public string print() {
        return $"{SnafuString.PadLeft(10)} = {ToLong}";
    }

}

internal class Day25
{
    public static List<Snafu> Snafus; 

    internal static (string, string) Solve()
    {
        string s1 = util.Measure<string>(Setup,true,P1,1);
        string s2 = util.Measure<string>(Setup,true,P2,1);
   
        return (s1,s2);
    }


    private static void Setup(bool IsP1) {
        List<string> lines = util.ReadFile("day25.txt").Where(l => String.IsNullOrWhiteSpace(l) == false).ToList();
        Snafus = lines.Select(l => new Snafu(l)).ToList();
    }

    private static string P1() {
        var sum = Snafus.Sum(s => s.ToLong);
        return Snafu.ToSnafu(sum);
    }

     private static string P2() {
        return "Done!";
    }


}