using Day16;

var lines = File.ReadAllLines("input.txt");

int mapSizeX = lines[0].Length;
int mapSizeY = lines.Length;

char[,] map = new char[mapSizeX,mapSizeY];

for (int y = 0; y < mapSizeY; y++)
{
    for (int x = 0; x < mapSizeX; x++)
    {
        map[x, y] = lines[y][x];
    }
}

Console.WriteLine("Part 1: " + GetEnergized(new Beam() { Position = (0,0), Direction = (1,0)}, map, mapSizeX, mapSizeY));


int mostEnergized = 0;
for (int y = 0; y < mapSizeY; y++)
{
    var energizedLeft = GetEnergized(new Beam() { Position = (0, y), Direction = (1, 0) }, map, mapSizeX, mapSizeY);
    var energizedRight = GetEnergized(new Beam() { Position = (mapSizeX - 1, y), Direction = (-1, 0) }, map, mapSizeX, mapSizeY);

    if (energizedLeft > mostEnergized)
        mostEnergized = energizedLeft;
    if (energizedRight > mostEnergized)
        mostEnergized = energizedRight;
}
for (int x = 0; x < mapSizeX; x++)
{
    var energizedUp = GetEnergized(new Beam() { Position = (x, 0), Direction = (0, 1) }, map, mapSizeX, mapSizeY);
    var energizedDown = GetEnergized(new Beam() { Position = (x, mapSizeY - 1), Direction = (0, -1) }, map, mapSizeX, mapSizeY);

    if (energizedUp > mostEnergized)
        mostEnergized = energizedUp;
    if (energizedDown > mostEnergized)
        mostEnergized = energizedDown;
}

Console.WriteLine("Part 2: " + mostEnergized);

static int GetEnergized(Beam startBeam, char[,] map, int mapSizeX, int mapSizeY)
{
    Dictionary<(int x, int y), List<Beam>> cachedBeams = new();

    Queue<Beam> beams = new();
    beams.Enqueue(startBeam);

    while (beams.Count > 0)
    {
        var newBeams = GetNewBeams(beams.Dequeue(), map, mapSizeX, mapSizeY, ref cachedBeams);
        foreach (var newBeam in newBeams)
        {
            beams.Enqueue(newBeam);
        }
    }

    int sum = 0;

    for (int y = 0; y < mapSizeY; y++)
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            if (cachedBeams.TryGetValue(new(x, y), out var beamsAtPosition))
                if (beamsAtPosition.Count > 0)
                    sum++;
        }
    }
    return sum;
}

static List<Beam> GetNewBeams(Beam beam, char[,] map, int mapSizeX, int mapSizeY, ref Dictionary<(int x, int y), List<Beam>> cachedBeams)
{
    var list = new List<Beam>();

    if (beam.Position.x < 0 || beam.Position.x > mapSizeX - 1)
        return list;
    if (beam.Position.y < 0 || beam.Position.y > mapSizeY - 1)
        return list;

    if (!cachedBeams.ContainsKey(beam.Position))
        cachedBeams.Add(beam.Position, new());

    if (cachedBeams[beam.Position].Any(b => b.Direction == beam.Direction))
        return list;

    cachedBeams[beam.Position].Add(beam);

    var mapInstruction = map[beam.Position.x, beam.Position.y];

    if (mapInstruction == '|')
    {
        if (beam.Direction.x != 0)
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x, beam.Position.y + 1),
                Direction = new(0, 1)
            });
            list.Add(new Beam()
            {
                Position = new(beam.Position.x, beam.Position.y - 1),
                Direction = new(0, -1)
            });
            return list;
        }
    }

    if (mapInstruction == '-')
    {
        if (beam.Direction.y != 0)
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x + 1, beam.Position.y),
                Direction = new(1, 0)
            });
            list.Add(new Beam()
            {
                Position = new(beam.Position.x - 1, beam.Position.y),
                Direction = new(-1, 0)
            });
            return list;
        }
    }

    if (mapInstruction == '/')
    {
        if (beam.Direction.x == 1)
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x, beam.Position.y - 1),
                Direction = new(0, -1)
            });
        }
        else if (beam.Direction.x == -1)
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x, beam.Position.y + 1),
                Direction = new(0, 1)
            });
        }
        else if (beam.Direction.y == 1)
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x - 1, beam.Position.y),
                Direction = new(-1, 0)
            });
        }
        else
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x + 1, beam.Position.y),
                Direction = new(1, 0)
            });
        }
        return list;
    }

    if (mapInstruction == '\\')
    {
        if (beam.Direction.x == 1)
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x, beam.Position.y + 1),
                Direction = new(0, 1)
            });
        }
        else if (beam.Direction.x == -1)
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x, beam.Position.y - 1),
                Direction = new(0, -1)
            });
        }
        else if (beam.Direction.y == 1)
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x + 1, beam.Position.y),
                Direction = new(1, 0)
            });
        }
        else
        {
            list.Add(new Beam()
            {
                Position = new(beam.Position.x - 1, beam.Position.y),
                Direction = new(-1, 0)
            });
        }
        return list;
    }

    list.Add(new Beam()
    {
        Position = new(beam.Position.x + beam.Direction.x, beam.Position.y + beam.Direction.y),
        Direction = beam.Direction
    });

    return list;
}