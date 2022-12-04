using System.Runtime.Intrinsics.X86;

var input = System.IO.File.ReadLines(@"C:\GIT\AdventOfCode2022\AdventOfCode2022\Day4\input.txt");

// Part 1
var completlyOverlappingPairs = 0;
var allOverlappingPairs = 0;
foreach (var line in input)
{
    var inputs = line.Split(',');
    var input1 = inputs[0].Split('-');
    var input2 = inputs[1].Split('-');
    var array1 = new int[Int32.Parse(input1[1]) - Int32.Parse(input1[0]) + 1];
    var array2 = new int[Int32.Parse(input2[1]) - Int32.Parse(input2[0]) + 1];

    var start = Int32.Parse(input1[0]);
    for (int i = 0; i < array1.Length; i++)
    {
        array1[i] = start;
        start++;
    }

    start = Int32.Parse(input2[0]);
    for (int i = 0; i < array2.Length; i++)
    {
        array2[i] = start;
        start++;
    }

    var overlapsCompletely = (new bool[2] { array1.Intersect(array2).Count() == array1.Count(), array2.Intersect(array1).Count() == array2.Count() }).Contains(true);
    var overlapsAtAll = (new bool[2] { array1.Intersect(array2).Count() > 0, array2.Intersect(array1).Count() > 0 }).Contains(true);
    if (overlapsCompletely)
    {
        completlyOverlappingPairs++;
    }
    if (overlapsAtAll)
    {
        allOverlappingPairs++;
    }
}

Console.WriteLine($"The number of completely overlapping pairs is: {completlyOverlappingPairs}");

Console.WriteLine($"The number of all overlapping pairs is: {allOverlappingPairs}");