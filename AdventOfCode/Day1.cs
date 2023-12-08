// Deci AdventOfCode Day1.cs
// $File.CreatedYear-$File.CreatedMonth-7 @ 18:6

namespace AdventOfCode;

public class Day1
{

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