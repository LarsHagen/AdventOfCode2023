using Day22;

var lines = File.ReadAllLines("input.txt");
string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
List<Brick> bricks = new();
Dictionary<(int x, int y, int z), Brick> map = new();

for (int i = 0; i < lines.Length; i++)
{
    var newBrick = new Brick(lines[i], map);
    //newBrick.Name = abc[i];
    bricks.Add(newBrick);
}

//PrintMap();
bool SomethingMoved = true;
while (SomethingMoved)
{
    SomethingMoved = false;
    foreach (var brick in bricks)
    {
        if (brick.Fall(map))
            SomethingMoved = true;
    }
    //PrintMap();
}

foreach (var brick in bricks)
{
    brick.CalculateSupports(map);
}

HashSet<Brick> bricksThatCanBeRemoved = new();

foreach(var brick in bricks)
{
    if (brick.Supports.Count == 0)
        bricksThatCanBeRemoved.Add(brick);
    
    if (brick.SupportedBy.Count > 1)
    {
        foreach (var supportBrick in brick.SupportedBy)
        {
            //Each brick is a candidate, just check if removing it will make any other brick fall
            bool supportingAnyBrickThatWillFall = supportBrick.Supports.Any(b => b.SupportedBy.Count == 1);
            if (supportingAnyBrickThatWillFall)
                continue;
            
            bricksThatCanBeRemoved.Add(supportBrick);
        }
    }
}

Console.WriteLine("Part 1: " + bricksThatCanBeRemoved.Count);

int chainReactionSum = bricks.Sum(brick => brick.CalculateChainReaction());
Console.WriteLine("Part 2: " + chainReactionSum);

void PrintMap() //Only works for example input
{
    Console.Clear();
    Console.SetCursorPosition(0,0);
    Console.WriteLine("MAP:");
    foreach (var brick in bricks)
    {
        foreach (var block in brick.Blocks)
        {
            Console.SetCursorPosition(block.x, 10 - block.z);
            Console.Write(brick.Name);
            
            Console.SetCursorPosition(block.y + 10, 10 - block.z);
            Console.Write(brick.Name);
        }
    }
    Console.SetCursorPosition(0,10 + 1);
    Thread.Sleep(1000);
}