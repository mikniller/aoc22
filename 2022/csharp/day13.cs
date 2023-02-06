
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Aoc.Common;
namespace Aoc._2022
{
    internal class Day13 : SolveDay2022
    {

        public List<JArray?> input;

        public override string SolvePart1()
        {
            int sum = 0;

            for (int i = 0; i < input.Count(); i += 2)
            {
                int val = findval(input[i], input[i + 1]);
                if (val > 0)
                {
                    sum += i / 2 + 1;
                }
            }
            return sum + "";
        }

        public override string SolvePart2()
        {

            var d1 = new JArray(2);
            var d2 = new JArray(6);

            input.Add(d1);
            input.Add(d2);

            input.Sort((a, b) => findval(b, a));

            int sum = (input.IndexOf(d1) + 1) * (input.IndexOf(d2) + 1);

            return sum + "";
        }

        public override void Setup(bool isPart1)
        {

            input = _lines.
                Where(l => string.IsNullOrWhiteSpace(l) == false).
                Select(v => JsonConvert.DeserializeObject<JArray>(v)).
                ToList();
        }

        public override bool IsReady() => true;

        internal int findval(object ar1, object ar2)
        {

            bool isNum1 = (ar1 is JArray) == false;
            bool isNum2 = (ar2 is JArray) == false;

            if (isNum1 && isNum2)
            {
                int val1 = Int32.Parse(ar1.ToString());
                int val2 = Int32.Parse(ar2.ToString());
                int res = 0;

                if (val1 < val2) res = 1;
                if (val1 > val2) res = -1;

                GetWriter().WriteLine($"compare {val1} {val2} => {res}");
                return res;

            }

            if (isNum1)
                ar1 = new JArray(ar1);

            if (isNum2)
                ar2 = new JArray(ar2);


            int l1 = ((JArray)ar1).Count();
            int l2 = ((JArray)ar2).Count();
            int val = 0;
            for (int i = 0; i < Math.Max(l1, l2); i++)
            {
                if (i >= l1)
                    return 1;
                if (i >= l2)
                    return -1;
                val = findval(((JArray)ar1)[i], ((JArray)ar2)[i]);
                if (val != 0)
                    return val;
            }

            return val;

        }

    }

}