using Day18;

var lines = File.ReadAllLines("input.txt");

List<DigInstruction> digInstructions = new();
List<DigInstruction> digInstructionsPart2 = new();
foreach (var instruction in lines)
{
    digInstructions.Add(new DigInstruction(instruction));
    digInstructionsPart2.Add(new DigInstructionPart2(instruction));
}

var holes = DigTrench(digInstructions);
Console.WriteLine("Part 1: " + FloodFill(holes));
Console.WriteLine("Part 2: " + Shoelace(digInstructionsPart2));

int FloodFill(HashSet<(int x, int y)> holes)
{
    int mapMinX = int.MaxValue;
    int mapMaxX = int.MinValue;
    int mapMinY = int.MaxValue;
    int mapMaxY = int.MinValue;
    foreach (var hole in holes)
    {
        mapMinX = Math.Min(mapMinX, hole.x);
        mapMaxX = Math.Max(mapMaxX, hole.x);
        mapMinY = Math.Min(mapMinY, hole.y);
        mapMaxY = Math.Max(mapMaxY, hole.y);
    }
    
    Console.WriteLine(mapMaxX - mapMinX);
    Console.WriteLine(mapMaxY - mapMinY);
    
    //Assume the center of the map is within the hole (luckily that assumption is correct)
    var centerX = (mapMaxX + mapMinX) / 2;
    var centerY = (mapMaxY + mapMinY) / 2;
    
    Queue<(int x, int y)> toDig = new();
    toDig.Enqueue((centerX,centerY));

    while (toDig.Count > 0)
    {
        //Console.WriteLine(holes.Count);
        var current = toDig.Dequeue();
    
        if (holes.Contains(current))
            continue;
    
        holes.Add((current.x, current.y));
    
        (int x, int y) up = (current.x, current.y - 1);
        (int x, int y) down = (current.x, current.y + 1);
        (int x, int y) left = (current.x - 1, current.y);
        (int x, int y) right = (current.x + 1, current.y);
    
        if (!holes.Contains(up))
            toDig.Enqueue(up);
        if (!holes.Contains(down))
            toDig.Enqueue(down);
        if (!holes.Contains(left))
            toDig.Enqueue(left);
        if (!holes.Contains(right))
            toDig.Enqueue(right);
    }

    return holes.Count;
}

HashSet<(int x, int y)> DigTrench(List<DigInstruction> list)
{
    HashSet<(int x, int y)> holes = new();
    (int x, int y) currentPos = (0, 0);

    foreach (var digInstruction in list)
    {
        for (int i = 0; i < digInstruction.Distance; i++)
        {
            holes.Add((currentPos.x, currentPos.y));

            currentPos.x += (int)digInstruction.Direction.x;
            currentPos.y += (int)digInstruction.Direction.y;
        }
    }

    return holes;
}

long Shoelace(List<DigInstruction> list)
{
    List<(long x, long y)> verticies = new();

    long x = 0;
    long y = 0;

    long trenchSize = 0;
    
    foreach (var digInstruction in list)
    {
        verticies.Add((x, y));
        x += digInstruction.Direction.x * digInstruction.Distance;
        y += digInstruction.Direction.y * digInstruction.Distance;

        trenchSize += digInstruction.Distance;
    }
    verticies.Add((0,0));

    long a = 0;
    long b = 0;
    
    for (int i = 0; i < verticies.Count - 1; i++)
    {
        a += verticies[i].x * verticies[i + 1].y;
        b += verticies[i].y * verticies[i + 1].x;
    }
    
    var shoelaceArea = Math.Abs(a - b) / 2;

    //The shoelace algorithm assumes the sides are edges without thickness, this means that only half of the trench is
    //included in the area, so we need to add half the trench size to get total area... but I also needed to add +1 to
    //get the correct answer because of rounding errors I guess?
    return shoelaceArea + trenchSize / 2 + 1;
}