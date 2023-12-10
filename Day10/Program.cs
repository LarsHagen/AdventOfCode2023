using Day10;

//var lines = File.ReadAllLines("example.txt");
//var startReplace = 'F';

//var lines = File.ReadAllLines("example2.txt");
//var startReplace = 'F';

//var lines = File.ReadAllLines("example3.txt");
//var startReplace = '7';

var lines = File.ReadAllLines("input.txt");
var startReplace = 'L';


Dictionary<(int x, int y), Node> nodes = new();
Node start = new();

Dictionary<(int x, int y), int> loopMap = new();

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        //loopMap.Add((x, y), 0);

        Node n = new Node()
        {
            X = x,
            Y = y,
            C = lines[y][x]
        };

        nodes.Add(new(x, y), n);

        if (n.C == 'S')
            start = n;
    }
}

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        Node connection;

        var c = nodes[(x, y)].C;
        if (c == '.')
            continue;

        if (c == 'S')
            c = startReplace;

        if (c == '|')
        {
            if (nodes.TryGetValue((x, y - 1), out connection))
                nodes[(x, y)].Connections.Add(connection);
            if (nodes.TryGetValue((x, y + 1), out connection))
                nodes[(x, y)].Connections.Add(connection);
        }

        if (c == '-')
        {
            if (nodes.TryGetValue((x - 1, y), out connection))
                nodes[(x, y)].Connections.Add(connection);
            if (nodes.TryGetValue((x + 1, y), out connection))
                nodes[(x, y)].Connections.Add(connection);
        }

        if (c == 'L')
        {
            if (nodes.TryGetValue((x, y - 1), out connection))
                nodes[(x, y)].Connections.Add(connection);
            if (nodes.TryGetValue((x + 1, y), out connection))
                nodes[(x, y)].Connections.Add(connection);
        }

        if (c == 'J')
        {
            if (nodes.TryGetValue((x, y - 1), out connection))
                nodes[(x, y)].Connections.Add(connection);
            if (nodes.TryGetValue((x - 1, y), out connection))
                nodes[(x, y)].Connections.Add(connection);
        }

        if (c == '7')
        {
            if (nodes.TryGetValue((x - 1, y), out connection))
                nodes[(x, y)].Connections.Add(connection);
            if (nodes.TryGetValue((x, y + 1), out connection))
                nodes[(x, y)].Connections.Add(connection);
        }

        if (c == 'F')
        {
            if (nodes.TryGetValue((x + 1, y), out connection))
                nodes[(x, y)].Connections.Add(connection);
            if (nodes.TryGetValue((x, y + 1), out connection))
                nodes[(x, y)].Connections.Add(connection);
        }
    }
}

int count = 0;
Node previous = start;
Node current = start.Connections[0];
start.Connected = true;


while (current != start)
{
    current.Connected = true;
    var next = current.Connections.First(c => c != previous);
    previous = current;
    current = next;

    count++;
}

Console.WriteLine("Part 1: " + (count + 1) / 2);

int loopMapSizeX = lines[0].Length * 3;
int loopMapSizeY = lines.Length * 3;

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[0].Length; x++)
    {
        var c = nodes[(x, y)].C;
        
        if (c == 'S')
            c = startReplace;
        
        if (!nodes[(x, y)].Connected)
            c = '.';

        int loopMapY = y * 3;
        int loopMapX = x * 3;

        var kernel = GetPart2Kernel(c);

        for (int ky = 0; ky < 3; ky++)
        {
            for (int kx = 0; kx < 3; kx++)
            {
                loopMap.Add((loopMapX + kx, loopMapY + ky), kernel[ky, kx]);
            }
        }
    }
}

//0 = unburned
//1 = loop
//2 = burning
//3 = burned

for (int y = 0; y < loopMapSizeY; y++)
{
    if (loopMap[(0, y)] == 0)
        loopMap[(0, y)] = 2;

    if (loopMap[(loopMapSizeX - 1, y)] == 0)
        loopMap[(loopMapSizeX - 1, y)] = 2;
}

for (int x = 0; x < loopMapSizeX; x++)
{
    if (loopMap[(x, 0)] == 0)
        loopMap[(x, 0)] = 2;

    if (loopMap[(x, loopMapSizeY - 1)] == 0)
        loopMap[(x, loopMapSizeY - 1)] = 2;
}

bool stillBurning = true;

while (stillBurning)
{
    stillBurning = false;
    for (int y = 0; y < loopMapSizeY; y++)
    {
        for (int x = 0; x < loopMapSizeX; x++)
        {
            if (loopMap[(x, y)] != 2)
                continue;

            if (LightNeighbours(x, y))
                stillBurning = true;
        }
    }
}

int unburned = 0;

for (int y = 0; y < loopMapSizeY; y += 3)
{
    for (int x = 0; x < loopMapSizeX; x += 3)
    {
        if (loopMap[(x + 1, y + 1)] == 0)
        {
            loopMap[(x + 1, y + 1)] = 9;
            unburned++;
        }
    }
}

Console.WriteLine("Part 2: " + unburned);

bool LightNeighbours(int x, int y)
{
    int neighbour = 0;
    bool newFire = false;

    for (int ky = y-1; ky < y + 2; ky++)
    {
        for (int kx = x-1; kx < x + 2; kx++)
        {
            if (loopMap.TryGetValue((kx,ky), out neighbour) && neighbour == 0)
            {
                newFire = true;
                loopMap[(kx,ky)] = 2;
            }
        }
    }

    return newFire;
}

int[,] GetPart2Kernel(char c)
{
    if (c == '.')
    {
        return new int[,]
        {
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
    }

    if (c == '|')
    {
        return new int[,]
        {
            {0,1,0},
            {0,1,0},
            {0,1,0}
        };
    }

    if (c == '-')
    {
        return new int[,]
        {
            {0,0,0},
            {1,1,1},
            {0,0,0}
        };
    }

    if (c == 'L')
    {
        return new int[,]
        {
            {0,1,0},
            {0,1,1},
            {0,0,0}
        };
    }

    if (c == 'J')
    {
        return new int[,]
        {
            {0,1,0},
            {1,1,0},
            {0,0,0}
        };
    }

    if (c == '7')
    {
        return new int[,]
        {
            {0,0,0},
            {1,1,0},
            {0,1,0}
        };
    }

    if (c == 'F')
    {
        return new int[,]
        {
            {0,0,0},
            {0,1,1},
            {0,1,0}
        };
    }

    throw new ArgumentOutOfRangeException();
}