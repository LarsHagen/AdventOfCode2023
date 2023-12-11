var lines = File.ReadAllLines("input.txt");
int mapSizeX = lines[0].Length;
int mapSizeY = lines.Length;

int[,] map = new int[mapSizeX,mapSizeY];

int galaxyCount= 0;

Dictionary<int, (int x, int y)> galaxyPositions = new();

List<int> emptyRows = new();
List<int> emotyCols = new();

for (int y = 0; y < mapSizeY; y++)
{
    bool emptyRow = true;
    var line = lines[y];
    for (int x = 0; x < mapSizeX; x++)
    {
        if (line[x] == '#')
        {
            galaxyCount++;
            map[x, y] = galaxyCount;
            emptyRow = false;
            galaxyPositions.Add(galaxyCount, new(x, y));
        }
    }

    if (emptyRow)
        emptyRows.Add(y);
}

for (int x = 0; x < mapSizeX; x++)
{
    bool emptyCol = true;
    for (int y = 0; y < mapSizeY; y++)
    {
        if (map[x, y] != 0)
            emptyCol = false;
    }

    if (emptyCol)
        emotyCols.Add(x);
}

List<GalaxyDist> galaxyDists = new();

for (int i = 1; i <= galaxyCount; i++)
{
    for (int j = i + 1; j <= galaxyCount; j++)
    {
        var galaxyA = galaxyPositions[i];
        var galaxyB = galaxyPositions[j];

        var distY = Math.Abs(galaxyA.y - galaxyB.y);
        var distX = Math.Abs(galaxyA.x - galaxyB.x);

        int numEmptyCrossX = 0;
        for (int x = Math.Min(galaxyA.x, galaxyB.x); x < Math.Max(galaxyA.x, galaxyB.x); x++)
        {
            if (emotyCols.Contains(x))
                numEmptyCrossX++;
        }

        int numEmptyCrossY = 0;
        for (int y = Math.Min(galaxyA.y, galaxyB.y); y < Math.Max(galaxyA.y, galaxyB.y); y++)
        {
            if (emptyRows.Contains(y))
                numEmptyCrossY++;
        }

        galaxyDists.Add(new()
        {
            IndexA = i,
            IndexB = j,
            Distance = distX + distY,
            numEmptySpaceCross = numEmptyCrossX + numEmptyCrossY
        });
    }
}

int sum = 0;

foreach(var galaxyDist in galaxyDists)
{
    sum += galaxyDist.Distance + galaxyDist.numEmptySpaceCross;
}
Console.WriteLine("Part 1: " + sum);

long longSum = 0;
long iterations = 1000000;
foreach (var galaxyDist in galaxyDists)
{
    longSum += (long)galaxyDist.Distance + (long)galaxyDist.numEmptySpaceCross * (iterations - 1);
}
Console.WriteLine("Part 2: " + longSum);

struct GalaxyDist
{
    public int IndexA;
    public int IndexB;
    public int Distance;
    public int numEmptySpaceCross;
}