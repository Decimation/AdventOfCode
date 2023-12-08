using Kantan.Text;

namespace AdventOfCode;

public static class Program
{

	public static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		Day1.Part2();
	}

	public class Day1
	{

		private static readonly string[] NumNames = new[]
			{ "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

		public static void Part1()
		{
			var lines = File.ReadAllLines(@"..\..\..\day1");
			int sum   = Sum(lines);

			Console.WriteLine(sum);
		}

		private static int Sum(string[] lines)
		{
			var sum = 0;

			foreach (var line in lines) {
				var    rg            = line.Where(char.IsNumber).ToArray();
				char   lastOrDefault = rg.LastOrDefault();
				string ns            = new(rg.First() + (lastOrDefault == default ? "" : lastOrDefault.ToString()));
				sum += int.Parse(ns);
				Console.WriteLine($"{line} {ns}");

			}

			return sum;
		}

		public static void Part2()
		{
			var lines = File.ReadAllLines(@"..\..\..\day1");
			var s     = CalculateTotalSum(lines);
			Console.WriteLine(s);
		}

		public static void Part2b()
		{
			// var lines = File.ReadAllLines(@"..\..\..\day1");
			/*var lines = """
			            two1nine
			            eightwothree
			            abcone2threexyz
			            xtwone3four
			            4nineeightseven2
			            zoneight234
			            7pqrstsixteen
			            """.Split("\r\n");*/
			var lines = File.ReadAllLines(@"..\..\..\day1");

			var numNames2 = (1..9);
			var sum       = 0;

			foreach (string line in lines) {
				var dict = new List<KeyValuePair<string, int>>();

				for (int i = 0; i < NumNames.Length; i++) {
					string? name = NumNames[i];
					var     of   = line.IndexOfAll(name);

					foreach (int i1 in of) {
						if (i1 != -1) {
							// dict[of]   = name;
							dict.Add(new(name, i1));

						}

					}
				}

				for (int i = 1; i <= 9; i++) {
					string? name = i.ToString();
					var     of   = line.IndexOfAll(name);

					foreach (int i1 in of) {
						if (i1 != -1) {
							// dict[of]   = name;
							dict.Add(new(name, i1));

						}

					}
				}

				var max = dict.MaxBy(x => x.Value);
				var min = dict.MinBy(x => x.Value);

				var key = I(min.Key).ToString();

				if (min.Value != max.Value) {
					key += I(max.Key);
				}

				sum += int.Parse($"{key}");
				Console.WriteLine($"{line} {max} {min} {key}");

			}

			Console.WriteLine(sum);

		}

		private static int I(string k)
		{
			string[] numNames;
			int      d = Array.IndexOf(NumNames, k);

			if (d == -1) {
				d = int.Parse(k);
			}
			else {
				d += 1;
			}

			return d;
		}

		static int CalculateTotalSum(string[] lines)
		{
			var numberMap = new Dictionary<string, string>
			{
				{ "zero", "0" }, { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" },
				{ "five", "5" }, { "six", "6" }, { "seven", "7" }, { "eight", "8" }, { "nine", "9" }
			};

			int totalSum = 0;

			foreach (var line in lines) {
				string firstDigit = FindDigit(line, numberMap, true);
				string lastDigit  = FindDigit(line, numberMap, false);

				if (!string.IsNullOrEmpty(firstDigit) && !string.IsNullOrEmpty(lastDigit)) {
					totalSum += int.Parse(firstDigit + lastDigit);
				}
			}

			return totalSum;
		}

		static string FindDigit(string line, Dictionary<string, string> numberMap, bool searchFromStart)
		{
			string current = "";

			if (searchFromStart) {
				foreach (char c in line) {
					if (char.IsDigit(c)) {
						return c.ToString();
					}

					current += c;

					foreach (var entry in numberMap) {
						if (current.EndsWith(entry.Key)) {
							return entry.Value;
						}
					}
				}
			}
			else {
				for (int i = line.Length - 1; i >= 0; i--) {
					if (char.IsDigit(line[i])) {
						return line[i].ToString();
					}

					current = line[i] + current;

					foreach (var entry in numberMap) {
						if (current.StartsWith(entry.Key)) {
							return entry.Value;
						}
					}
				}
			}

			return "";
		}

	}

}