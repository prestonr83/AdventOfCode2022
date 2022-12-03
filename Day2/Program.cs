var input = System.IO.File.ReadLines(@"C:\GIT\AdventOfCode2022\AdventOfCode2022\Day2\input.txt");

// Part 1

var totalScore = 0;
foreach (var line in input)
{
    var shapes = line.Split(' ');
    var opponent = shapes[0];
    var you = shapes[1];
    totalScore += Part1Outcome(opponent, you) + (int)Enum.Parse<shapeValue>(you);
}

Console.WriteLine($"My Total Score is: {totalScore}");


// Part 2
var outcomeScore = new Dictionary<string, int>(){
    {"X", 0},
    {"Y", 3},
    {"Z", 6}
};

var totalCorrectScore = 0;
foreach (var line in input)
{
    var shapes = line.Split(' ');
    var opponent = shapes[0];
    var expectedOutcome = shapes[1];
    totalCorrectScore += outcomeScore[expectedOutcome] + Part2Outcome(opponent, expectedOutcome);
}

Console.WriteLine($"My Total Score is: {totalCorrectScore}");

// Functions / Enums

int Part1Outcome(string opponent, string you)
{
    switch (opponent)
    {
        case "A":
            switch (you)
            {
                case "X":
                    return 3;
                case "Y":
                    return 6;
                case "Z":
                    return 0;
            }
            break;
        case "B":
            switch (you)
            {
                case "X":
                    return 0;
                case "Y":
                    return 3;
                case "Z":
                    return 6;
            }
            break;
        case "C":
            switch (you)
            {
                case "X":
                    return 6;
                case "Y":
                    return 0;
                case "Z":
                    return 3;
            }
            break;
    }
    return 0;
}

int Part2Outcome(string opponent, string expectedOutcome)
{
    switch (opponent)
    {
        case "A":
            switch (expectedOutcome)
            {
                case "X":
                    return 3;
                case "Y":
                    return 1;
                case "Z":
                    return 2;
            }
            break;
        case "B":
            switch (expectedOutcome)
            {
                case "X":
                    return 1;
                case "Y":
                    return 2;
                case "Z":
                    return 3;
            }
            break;
        case "C":
            switch (expectedOutcome)
            {
                case "X":
                    return 2;
                case "Y":
                    return 3;
                case "Z":
                    return 1;
            }
            break;
    }
    return 0;
}

enum shapeValue
{
    X = 1,
    Y,
    Z
}