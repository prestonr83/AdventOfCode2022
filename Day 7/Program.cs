var input = System.IO.File.ReadLines(@".\input.txt");


var root = new DirectoryInfo
{
    name = "/"
};


DirectoryInfo lastItem = root;
foreach (var line in input)
{
    if (line.StartsWith("$ cd .."))
    {
        if (lastItem.parent == "/")
        {
            lastItem = root;
            continue;
        }
        var path = lastItem.parent.Split("/").Where(x => !string.IsNullOrEmpty(x)).ToArray();
        lastItem = root.child[path[0]];
        foreach (var name in path.Skip(1))
        {
            lastItem = lastItem.child[name];
        }
        continue;
    }
    if (line.StartsWith("$ cd"))
    {
        var currentDir = line.Replace("$ cd ", "");
        if (currentDir == "/")
        {
            continue;
        }
        var parent = lastItem.name;
        if (lastItem.parent is not null)
        {
            parent = $"{lastItem.parent}/{lastItem.name}";
        }
        lastItem.child.Add(currentDir, new DirectoryInfo
        {
            name = currentDir,
            parent = parent
        });
        lastItem = lastItem.child[currentDir];
        continue;
    }
    if (line.StartsWith("$ ls"))
    {
        continue;
    }
    if (line.StartsWith("dir"))
    {
        continue;
    }
    if (char.IsDigit(line[0]))
    {
        lastItem.contents.Add(
            new FileInfo
            {
                type = "file",
                name = line.Split(" ")[1],
                size = Int32.Parse(line.Split(" ")[0])
            });
        lastItem.totalSize += Int32.Parse(line.Split(" ")[0]);
        if (lastItem.parent == "/")
        {
            root.totalSize += Int32.Parse(line.Split(" ")[0]);
            continue;
        }
        if (lastItem.parent is null)
        {
            continue;
        }
        var path = lastItem.parent.Split("/").Where(x => !string.IsNullOrEmpty(x)).ToArray();
        var parent = root.child[path[0]];
        foreach (var name in path.Skip(1))
        {
            parent = parent.child[name];
        }
        do
        {
            parent.totalSize += Int32.Parse(line.Split(" ")[0]);
            if (parent.parent is not null && parent.parent != "/")
            {
                path = parent.parent.Split("/").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                parent = root.child[path[0]];
                foreach (var name in path.Skip(1))
                {
                    parent = parent.child[name];
                }
                //parent = root.child[parent.parent.Trim('/')];
            }
            else
            {
                parent = root;
                parent.totalSize += Int32.Parse(line.Split(" ")[0]);
            }
        } while (parent.parent is not null);
    }

}

var directorySum = parseDirectorySize(root, 100000);

Console.WriteLine($"The sum of the directories is: {directorySum}");


var freespace = 70000000 - root.totalSize;
var minimumDeleteSize = 30000000 - freespace;
var deleteSize = parseDirectoryDeleteSize(root, minimumDeleteSize);
Console.WriteLine($"The size of directory to delete is: {deleteSize}");

int parseDirectoryDeleteSize(DirectoryInfo directories, int minimumDeleteSize)
{
    var last = 0;
    if (directories.totalSize >= minimumDeleteSize)
    {
        last = directories.totalSize;
    }
    if (directories.child.Count() > 0)
    {
        foreach (var directory in directories.child)
        {
            var subDirLast = parseDirectoryDeleteSize(directory.Value, minimumDeleteSize);
            if (subDirLast > 0 && subDirLast < last)
            {
                last = subDirLast;
            }
        }
    }
    return last;
}

int parseDirectorySize(DirectoryInfo directories, int sizeLimit)
{
    var total = 0;
    if (directories.totalSize <= sizeLimit)
    {
        total += directories.totalSize;
    }
    if (directories.child.Count() > 0)
    {
        foreach (var directory in directories.child)
        {
            total += parseDirectorySize(directory.Value, sizeLimit);
        }
    }
    return total;
}


public class DirectoryInfo
{
    public string? name;
    public string? parent;
    public int totalSize = 0;
    public List<FileInfo>? contents = new List<FileInfo>();
    public Dictionary<string, DirectoryInfo> child = new Dictionary<string, DirectoryInfo>();
}


public class FileInfo
{
    public string? type { get; set; }
    public string? name { get; set; }
    public int? size { get; set; }

}








