using Aoc.Common;
namespace Aoc._2022
{

    internal class Day19 : SolveDay2022
    {
        List<Blueprint> bluePrints = new List<Blueprint>();



        public override string SolvePart1()
        {
            int res = 0;
            foreach (var b in bluePrints)
            {
                var r = Search(b, 24);
                res += b.Number * r;
            }
            return res + "";
        }

        public override string SolvePart2()
        {
            int res = Search(bluePrints[0], 32);
            res *= Search(bluePrints[1], 32);
            res *= Search(bluePrints[2], 32);

            return res + "";
        }

        public override void Setup(bool isPart1)
        {
            bluePrints = new List<Blueprint>();
            foreach (var bpVals in _linesWithoutBlank.Select(l => Util.AllIntsInLine(l, true).ToList()))
            {
                Blueprint bp = new Blueprint(bpVals[0], bpVals[1], bpVals[2], bpVals[3], bpVals[4], bpVals[5], bpVals[6]);
                bluePrints.Add(bp);
            }
        }

        public override bool IsReady() => true;

        public static int Search(Blueprint b, int depth)
        {
            Queue<MiningState> MiningQueue = new Queue<MiningState>();
            HashSet<string> seenStates = new HashSet<string>();

            MiningQueue.Enqueue(MiningState.InitialState(depth));
            int res = 0;
            while (MiningQueue.Any())
            {

                var workState = MiningQueue.Dequeue();

                res = Math.Max(res, workState.OpenenedGeodes);

                if (workState.Depth == 0)
                    continue;

                workState.AdjustLevels(b);

                // is it same result at same minute earlier ? Then stop this branch
                if (seenStates.Contains(workState.GetKey()))
                    continue;
                seenStates.Add(workState.GetKey());

                // just keep mining
                MiningQueue.Enqueue(workState.Copy(true));

                // one more orerobot
                if (workState.Ore >= b.OreRobot)
                {
                    var s1 = workState.Copy(true);
                    s1.Ore -= b.OreRobot; // building an ore robot
                    s1.OreRobots++;
                    MiningQueue.Enqueue(s1);

                }

                if (workState.Ore >= b.ClayRobot)
                {
                    var s1 = workState.Copy(true);
                    s1.Ore -= b.ClayRobot; // building a clay robot
                    s1.ClayRobots++;
                    MiningQueue.Enqueue(s1);
                }

                if (workState.Ore >= b.GeodeRobot.ore && workState.Obsidian >= b.GeodeRobot.obsidian)
                {
                    var s1 = workState.Copy(true);
                    s1.Ore -= b.GeodeRobot.ore; // building a geoderobot
                    s1.Obsidian -= b.GeodeRobot.obsidian;
                    s1.GeodeRobots++;
                    MiningQueue.Enqueue(s1);
                }

                if (workState.Ore >= b.ObsidianRobot.ore && workState.Clay >= b.ObsidianRobot.clay)
                {
                    var s1 = workState.Copy(true);
                    s1.Ore -= b.ObsidianRobot.ore; // building a obsd robot
                    s1.Clay -= b.ObsidianRobot.clay;
                    s1.ObsidianRobots++;
                    MiningQueue.Enqueue(s1);
                }


            }

            return res;
        }

    }

    class Blueprint
    {
        public int HighestRobotOreVal;
        public int Number;
        public int OreRobot;
        public int ClayRobot;
        public (int ore, int clay) ObsidianRobot;
        public (int ore, int obsidian) GeodeRobot;



        public Blueprint(int num, int or, int cr, int obOre, int obCl, int geOre, int geObs)
        {
            Number = num;
            OreRobot = or;
            ClayRobot = cr;
            ObsidianRobot = (obOre, obCl);
            GeodeRobot = (geOre, geObs);
            HighestRobotOreVal = Math.Max(Math.Max(Math.Max(OreRobot, ClayRobot), ObsidianRobot.ore), GeodeRobot.ore);
        }
    }

    class MiningState
    {

        public int Ore;
        public int Clay;
        public int Obsidian;
        public int OpenenedGeodes;
        public int OreRobots;
        public int ClayRobots;
        public int ObsidianRobots;
        public int GeodeRobots;
        public int Depth;

        public MiningState Copy(bool increaseResources)
        {
            var state = new MiningState()
            {
                Ore = this.Ore,
                Clay = this.Clay,
                Obsidian = this.Obsidian,
                OpenenedGeodes = this.OpenenedGeodes,
                OreRobots = this.OreRobots,
                ClayRobots = this.ClayRobots,
                ObsidianRobots = this.ObsidianRobots,
                GeodeRobots = this.GeodeRobots,
                Depth = this.Depth,
            };

            if (increaseResources)
            {
                state.Ore += this.OreRobots;
                state.Clay += this.ClayRobots;
                state.Obsidian += this.ObsidianRobots;
                state.OpenenedGeodes += this.GeodeRobots;
                state.Depth = this.Depth - 1;
            }
            return state;

        }

        public void AdjustLevels(Blueprint b)
        {

            int nd = Depth - 1;
            if (nd < 0)
                return;


            OreRobots = Math.Min(OreRobots, b.HighestRobotOreVal);
            ClayRobots = Math.Min(ClayRobots, b.ObsidianRobot.clay);
            ObsidianRobots = Math.Min(ObsidianRobots, b.GeodeRobot.obsidian);


            int maxRemainingVal = (Depth * b.HighestRobotOreVal) - (OreRobots * nd);
            Ore = Math.Min(maxRemainingVal, Ore);

            maxRemainingVal = (Depth * b.GeodeRobot.obsidian) - (ObsidianRobots * nd);
            Obsidian = Math.Min(maxRemainingVal, Obsidian);

            maxRemainingVal = (Depth * b.ObsidianRobot.clay) - (ClayRobots * nd);
            Clay = Math.Min(maxRemainingVal, Clay);
        }


        public string GetKey()
        {
            return $"{Ore}_{Clay}_{Obsidian}_{OpenenedGeodes}_{OreRobots}_{ClayRobots}_{ObsidianRobots}_{GeodeRobots}_{Depth}";
        }


        public static MiningState InitialState(int depth)
        {
            return new MiningState()
            {

                OreRobots = 1,
                Ore = 0,
                Clay = 0,
                Obsidian = 0,
                ClayRobots = 0,
                Depth = depth,
                GeodeRobots = 0,
                ObsidianRobots = 0,
                OpenenedGeodes = 0
            };
        }

    }





}