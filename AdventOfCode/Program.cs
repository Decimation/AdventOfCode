using Kantan.Text;

namespace AdventOfCode;

public static class Program
{

	public static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		Console.WriteLine(Day2.Eval(Day2.parse(File.ReadAllLines(@"..\..\..\day2"))));
		;
	}

}

public class Day2
{

	public const int MAX_RED   = 12;
	public const int MAX_GREEN = 13;
	public const int MAX_BLUE  = 14;

	public record Game(int Id, GameSubset[] subsets);

	public record GameSubset(int r, int g, int b);

	public static Game[] parse(string[] lines)
	{
		var games = new List<Game>();

		foreach (string line in lines) {

			var spl     = line.Split(':');
			var id      = spl[0].Split(' ')[1];
			var subs    = spl[1].Split(';');
			var subsets = new List<GameSubset>();

			foreach (var sub in subs) {

				var rgb = sub.Split(',');
				int r   = 0, g = 0, b = 0;

				foreach (string s in rgb) {
					var ss    = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
					var color = ss[1];
					var qty   = int.Parse(ss[0]);

					if (color == "red") {
						r = qty;
					}

					if (color == "green") {
						g = qty;
					}

					if (color == "blue") {
						b = qty;
					}
				}

				var gss = new GameSubset(r, g, b);
				subsets.Add(gss);
				Console.WriteLine(gss);
			}

			var game = new Game(int.Parse(id), subsets.ToArray());
			Console.WriteLine(game);
			games.Add(game);

			// var game = new Game(int.Parse(id), new[] { });
		}

		return games.ToArray();
	}

	public static int Eval(Game[] games)
	{
		var pg = new List<Game>();

		foreach (var game in games) {
			bool isp = true;

			foreach (GameSubset subset in game.subsets) {
				var (r, g, b) = subset;

				if (r > MAX_RED || g > MAX_GREEN || b > MAX_BLUE) {
					isp = false;
					break;
				}
			}

			if (isp) {
				pg.Add(game);
			}
		}

		return pg.Sum(x => x.Id); //
	}

}