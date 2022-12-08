var input = File.ReadLines(@".\input.txt").ToArray();

var rows = input.Count();
var visibleTrees = (input[0].Count() * 2) + (rows * 2) - 4;

for (int row = 1; row < rows - 1; row++)
{
    for (int col = 1; col < input[row].Count() - 1; col++)
    {
        var rowVisible = checkRow(input[row], col);
        if (rowVisible)
        {
            visibleTrees++;
            continue;
        }
        var colVisible = checkColumn(input, row, col);
        if (colVisible)
        {
            visibleTrees++;
        }
    }
}


Console.WriteLine($"The number of visible trees is: {visibleTrees}");


var topScore = 0;
for (int row = 1; row < rows - 1; row++)
{
    for (int col = 1; col < input[row].Count() - 1; col++)
    {
        var rowScore = getRowScore(input[row], col);
        var colScore = getColScore(input, row, col);

        var totalScore = rowScore * colScore;
        if (totalScore > topScore)
        {
            topScore = totalScore;
        }
    }
}


Console.WriteLine($"The highest scenic score is: {topScore}");



bool checkColumn(string[] array, int rIndex, int cIndex)
{
    var tree = char.GetNumericValue(array[rIndex][cIndex]);
    var topVisible = !array.Take(rIndex).ToList().Any(t => char.GetNumericValue(t[cIndex]) >= tree);
    var bottomVisible = !array.TakeLast(array.Count() - rIndex - 1).ToList().Any(t => char.GetNumericValue(t[cIndex]) >= tree);
    if (topVisible || bottomVisible)
    {
        return true;
    }
    return false;
}


int getColScore(string[] array, int rIndex, int cIndex)
{
    var score = 0;
    var topTrees = array.Take(rIndex).ToList();
    topTrees.Reverse();
    var bottomTrees = array.TakeLast(array.Count() - rIndex - 1);

    var tScore = 0;
    foreach (var tree in topTrees)
    {
        if (char.GetNumericValue(tree[cIndex]) >= char.GetNumericValue(array[rIndex][cIndex]))
        {
            tScore++;
            break;
        }
        tScore++;
    }
    var bScore = 0;
    foreach (var tree in bottomTrees)
    {
        if (char.GetNumericValue(tree[cIndex]) >= char.GetNumericValue(array[rIndex][cIndex]))
        {
            bScore++;
            break;
        }
        bScore++;
    }

    score = tScore * bScore;
    return score;
}


int getRowScore(string row, int index)
{
    var score = 0;
    var leftTrees = row.Take(index).ToList();
    leftTrees.Reverse();
    var rightTrees = row.TakeLast(row.Count() - index - 1).ToList();

    var lScore = 0;
    foreach (var tree in leftTrees)
    {
        if (char.GetNumericValue(tree) >= char.GetNumericValue(row[index]))
        {
            lScore++;
            break;
        }
        lScore++;
    }
    var rScore = 0;
    foreach (var tree in rightTrees)
    {
        if (char.GetNumericValue(tree) >= char.GetNumericValue(row[index]))
        {
            rScore++;
            break;
        }
        rScore++;
    }

    score = lScore * rScore;
    return score;
}



bool checkRow(string row, int index)
{
    var tree = char.GetNumericValue(row[index]);
    var leftVisible = !row.Take(index).ToList().Any(t => char.GetNumericValue(t) >= tree);
    var rightVisible = !row.TakeLast(row.Count() - index - 1).ToList().Any(t => char.GetNumericValue(t) >= tree);
    if (leftVisible || rightVisible)
    {
        return true;
    }
    return false;
}