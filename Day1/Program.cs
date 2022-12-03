var input = System.IO.File.ReadAllText(@"C:\GIT\AdventOfCode2022\AdventOfCode2022\Day1\input.txt");

var arrays = input.Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries);

int[] summedCalories = new int[arrays.Length];
int index = 0;

foreach (var array in arrays)
{
    summedCalories[index] = (array.Split(Environment.NewLine).Select(s => Convert.ToInt32(s)).ToList().Sum());
    index++;
}

// Part 1
Console.WriteLine($"Max Calories: {summedCalories.Max()}");

// Part 2
Console.WriteLine($"Top 3 Total Max Calories: {summedCalories.OrderByDescending(x => x).Take(3).Sum()}");