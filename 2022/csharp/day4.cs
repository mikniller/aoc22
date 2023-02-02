using Aoc.Common;
namespace Aac._2022 {
	internal class Day4: SolveDay
	{
		public Day4(int year) : base(year) {}
	
		public override string SolvePart1() {
			int sum = 0;
			
			var pairs = _lines.
			Select(c => new { f = c.Split(',') }).
			Select(c1 => new { fl = Int32.Parse(c1.f[0].Split('-')[0]), fh = Int32.Parse(c1.f[0].Split('-')[1]), ll = Int32.Parse(c1.f[1].Split('-')[0]), lh = Int32.Parse(c1.f[1].Split('-')[1]) }).
			Select(r => new { r1 = Enumerable.Range(r.fl, r.fh - r.fl + 1), r2 = Enumerable.Range(r.ll, r.lh - r.ll + 1) });

			foreach (var p in pairs)
			{
				sum += p.r1.Except(p.r2).Any() ? 0 : 1;
				sum += p.r2.Except(p.r1).Any() ? 0 : 1;
			}

			return sum+"";
		}

		public override string SolvePart2() {
			int sum = 0;
			var pairs = _lines.
				Select(c => new { f = c.Split(',') }).
				Select(c1 => new { fl = Int32.Parse(c1.f[0].Split('-')[0]), fh = Int32.Parse(c1.f[0].Split('-')[1]), ll = Int32.Parse(c1.f[1].Split('-')[0]), lh = Int32.Parse(c1.f[1].Split('-')[1]) }).
				Select(r => new { r1 = Enumerable.Range(r.fl, r.fh - r.fl + 1), r2 = Enumerable.Range(r.ll, r.lh - r.ll + 1) });

			foreach (var p in pairs)
				sum += p.r1.Intersect(p.r2).Any() ? 1 : 0;
			return sum+"";
		}

		public override void Setup(bool isPart1) {
		}
	}

}