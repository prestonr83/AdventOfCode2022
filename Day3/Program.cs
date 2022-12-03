using System.Numerics;
using System.Text.RegularExpressions;

var input = System.IO.File.ReadLines(@"C:\GIT\AdventOfCode2022\AdventOfCode2022\Day3\input.txt");


// Part 1
var prioritySum = 0;
foreach (var line in input)
{
    var compartment1 = line.Substring(0, line.Length / 2).ToArray();
    var compartment2 = line.Substring(compartment1.Length, line.Length / 2).ToArray();

    var matches = compartment1.Where(c => compartment2.Contains(c)).Distinct().ToArray();
    foreach (var match in matches)
    {
        prioritySum += ((int)match % 32);
        if (match == char.ToUpper(match))
        {
            prioritySum += 26;
        }
    }
}

Console.WriteLine($"The sum of priorities is: {prioritySum}");

// Part 2
var groupSum = 0;
var skip = 0;
for (int i = 0; i < input.Count(); i = i + 3)
{
    var group = input.Skip(skip).Take(3).ToArray();
    var matches = group[0].Where(g => group[1].Contains(g) && group[2].Contains(g)).Distinct().ToArray();
    skip = skip + 3;
    foreach (var match in matches)
    {
        groupSum += ((int)match % 32);
        if (match == char.ToUpper(match))
        {
            groupSum += 26;
        }
    }
}

Console.WriteLine($"The sum of group priorities is: {groupSum}");