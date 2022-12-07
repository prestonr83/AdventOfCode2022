var input = System.IO.File.ReadAllText(@".\input.txt");
var inputList = input.ToList();


int GetMarker(List<char> input, int range)
{
    var start = 0;
    var markerPosition = 0;
    while (true)
    {
        var segment = input.GetRange(start, range);
        var duplicates = segment.GroupBy(x => x).Where(g => g.Count() > 1);
        if (duplicates.Count() == 0)
        {
            markerPosition = start + range;
            break;
        }
        start++;
    }
    return markerPosition;
}

var day1 = GetMarker(inputList, 4);
var day2 = GetMarker(inputList, 14);

Console.WriteLine($"start-of-packet marker is at position: {day1}");
Console.WriteLine($"start-of-packet message is at position: {day2}");
