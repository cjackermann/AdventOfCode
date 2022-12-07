string[] input = File.ReadAllLines("input.txt");

Folder folderStructure = GetFolderStructure(input);

PartOne(folderStructure);
PartTwo(folderStructure);

static void PartOne(Folder folderStructure)
{
    var bigFolders = folderStructure.GetFolders().Where(d => d.GetSize <= 100000);
    Console.WriteLine(bigFolders.Sum(d => d.GetSize));
}

static void PartTwo(Folder folderStructure)
{
    var overallSize = folderStructure.GetSize;
    var leftSpace = 70000000 - overallSize;
    var neededSpace = 30000000 - leftSpace;

    var folders = folderStructure.GetFolders();
    var folderToDelete = folders.OrderBy(d => d.GetSize).FirstOrDefault(d => d.GetSize >= neededSpace);
    Console.WriteLine(folderToDelete.GetSize);
}

static Folder GetFolderStructure(string[] input)
{
    var tree = new Folder("/", null, new List<Folder>(), new List<Text>());
    Folder currentFolder = tree;
    foreach (var line in input.Skip(1))
    {
        if (line[0] == '$')
        {
            var blocks = line.Split(' ');
            if (blocks[1] == "cd")
            {
                if (blocks[2] == "..")
                {
                    currentFolder = currentFolder.Parent;
                }
                else
                {
                    currentFolder = currentFolder.SubFolders.FirstOrDefault(d => d.Name == blocks[2]);
                }
            }
        }
        else
        {
            var parts = line.Split(' ');
            if (parts[0] == "dir")
            {
                currentFolder.SubFolders.Add(new Folder(parts[1], currentFolder, new List<Folder>(), new List<Text>()));
            }
            else
            {
                currentFolder.Texts.Add(new Text(parts[1], int.Parse(parts[0])));
            }
        }
    }

    return tree;
}

public class Folder
{
    public Folder(string name, Folder parent, List<Folder> subFolders, List<Text> texts)
    {
        Name = name;
        Parent = parent;
        SubFolders = subFolders;
        Texts = texts;
    }

    public string Name { get; }

    public Folder Parent { get; }

    public List<Folder> SubFolders { get; }

    public List<Text> Texts { get; }

    public int GetSize => Texts.Sum(d => d.Size) + SubFolders.Sum(d => d.GetSize);

    public IEnumerable<Folder> GetFolders()
    {
        yield return this;

        foreach (var entry in SubFolders.SelectMany(c => c.GetFolders()))
        {
            yield return entry;
        }
    }
}

public record Text(string Name, int Size);