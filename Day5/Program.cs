using System.Collections.Generic;
using System.Text.RegularExpressions;

var input = System.IO.File.ReadAllText(@"C:\GIT\AdventOfCode2022\AdventOfCode2022\Day5\input.txt");

var inputSplit = input.Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries);

var cargo = inputSplit[0].Split(Environment.NewLine);
var crateCount = cargo.Last().Trim().Last();
var instructions = inputSplit[1].Split(Environment.NewLine);
var cargoMap = new List<List<string>>();
for (int i = 0; i < Int32.Parse(crateCount.ToString()); i++)
{
    cargoMap.Add(new List<string>());
}

foreach (var crate in cargo.SkipLast(1).Reverse())
{
    var cnt = 0;
    for (var i = 0; i < crate.Length; i += 4)
    {
        cargoMap[cnt].Add(crate.Substring(i, Math.Min(4, crate.Length - i)).Replace("[", "").Replace("]", "").Trim());
        cargoMap[cnt].RemoveAll(s => s == "");
        cnt++;
    }
}


var crateTops = "";
foreach (var crate in CrateMover9000.execute(cargoMap, instructions))
{
    crateTops += crate.Last();
}

Console.WriteLine($"The top of each create is: {crateTops}");

// Part 2

crateTops = "";
foreach (var crate in CrateMover9001.execute(cargoMap, instructions))
{
    crateTops += crate.Last();
}

Console.WriteLine($"The top of each create is: {crateTops}");




class CrateMover9000
{
    public static List<List<string>> execute(List<List<string>> cargo, string[] instructions)
    {
        var copy = cargo.Select(x => x.ToList()).ToList();
        Regex regex = new Regex(@"move (?<count>\d+) from (?<source>\d+) to (?<destination>\d+)");
        foreach (var instruction in instructions)
        {
            Match match = regex.Match(instruction);
            var count = Int32.Parse(match.Groups["count"].Value.ToString());
            var source = Int32.Parse(match.Groups["source"].Value.ToString()) - 1;
            var destination = Int32.Parse(match.Groups["destination"].Value.ToString()) - 1;

            for (int i = 0; i < count; i++)
            {
                copy[destination].Add(copy[source].Last());
                copy[source].RemoveAt(copy[source].Count - 1);
            }
        }

        return copy;
    }
}
class CrateMover9001
{
    public static List<List<string>> execute(List<List<string>> cargo, string[] instructions)
    {
        var copy = cargo.Select(x => x.ToList()).ToList();
        Regex regex = new Regex(@"move (?<count>\d+) from (?<source>\d+) to (?<destination>\d+)");
        foreach (var instruction in instructions)
        {
            Match match = regex.Match(instruction);
            var count = Int32.Parse(match.Groups["count"].Value.ToString());
            var source = Int32.Parse(match.Groups["source"].Value.ToString()) - 1;
            var destination = Int32.Parse(match.Groups["destination"].Value.ToString()) - 1;
            copy[destination].AddRange(copy[source].TakeLast(count));
            copy[source].Reverse();
            copy[source].RemoveRange(0, count);
            copy[source].Reverse();
        }
        return copy;
    }

}