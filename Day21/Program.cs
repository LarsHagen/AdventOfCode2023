var lines = File.ReadAllLines("input.txt");
//int steps = 6; //6 for example
int steps = 64; //64 for input

var mapSizeX = lines[0].Length;
Console.WriteLine(mapSizeX);
var mapSizeY = lines.Length;
char[,] map = new char[mapSizeX, mapSizeY];
(int x, int y) start = (0, 0);

for (int y = 0; y < mapSizeY; y++)
{
    for (int x = 0; x < mapSizeX; x++)
    {
        map[x, y] = lines[y][x];

        if (lines[y][x] == 'S')
        {
            start = (x, y);
            map[x, y] = '.';
        }
    }
}

HashSet<(int x, int y)> stepPositions = new();
stepPositions.Add(start);

for (int i = 0; i < steps; i++)
{
    HashSet<(int x, int y)> newPositions = new();
    for (int y = 0; y < mapSizeY; y++)
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            if (!stepPositions.Contains((x,y)))
                continue;
            
            if (x < mapSizeX - 1 && map[x+1, y] == '.')
                newPositions.Add((x + 1, y));
            
            if (x > 0 && map[x-1, y] == '.')
                newPositions.Add((x - 1, y));
            
            if (y < mapSizeY - 1 && map[x, y + 1] == '.')
                newPositions.Add((x, y + 1));
            
            if (y > 0 && map[x, y - 1] == '.')
                newPositions.Add((x, y - 1));
        }
    }

    stepPositions = newPositions;
    //PrintMap(stepPositions);
}

Console.WriteLine("Part 1: " + stepPositions.Count);

stepPositions = new();
stepPositions.Add(start);

List<(int x, int y)> directions = new();
directions.Add((1, 0));
directions.Add((-1, 0));
directions.Add((0, 1));
directions.Add((0, -1));

//List<float> aValues = new();
//List<float> bValues = new();
//List<float> cValues = new();

List<int> s = new();
s.Add(0);
for (int i = 0; i < 400; i++)
{
    HashSet<(int x, int y)> newPositions = new();

    foreach (var position in stepPositions)
    {
        foreach (var direction in directions)
        {
            (int x, int y) newPosition = (position.x + direction.x, position.y + direction.y);
            var mapPosition = WrapPosition(newPosition.x, newPosition.y);
            if (map[mapPosition.x, mapPosition.y] == '.')
                newPositions.Add(newPosition);
        }
    }
    
    /*for (int y = 0; y < mapSizeY; y++)
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            if (!stepPositions.Contains((x,y)))
                continue;
            
            if (x < mapSizeX - 1 && map[x+1, y] == '.')
                newPositions.Add((x + 1, y));
            
            if (x > 0 && map[x-1, y] == '.')
                newPositions.Add((x - 1, y));
            
            if (y < mapSizeY - 1 && map[x, y + 1] == '.')
                newPositions.Add((x, y + 1));
            
            if (y > 0 && map[x, y - 1] == '.')
                newPositions.Add((x, y - 1));
        }
    }*/

    stepPositions = newPositions;
    //Console.WriteLine(i + "," + stepPositions.Count);
    s.Add(stepPositions.Count);
    /*if (i > 3)
    {
        var p1 = s[0];
        var p2 = s[s.Count /2];
        var p3 = s[^1];
        
        var param = CalculateQuadraticParameters(0, p1, s.Count / 2, p2, s.Count - 1, p3);
        //Console.WriteLine(param.a + "-" + param.b + "-" + param.c);
        
        aValues.Add(param.a);
        bValues.Add(param.b);
        cValues.Add(param.c);
        
        var a = aValues.Average();
        var b = bValues.Average();
        var c = cValues.Average();

        long x = 26501365;
        //long x = 499;
        long estimatedGardenPlots = (long)(a * Math.Pow(x, 2) + b * x + 4);
        Console.WriteLine(estimatedGardenPlots);
    }*/
}

//int x1 = mapSizeX;
//int x2 = mapSizeX * 2;
//int x3 = mapSizeX * 3;
int x1 = 65;
int x2 = 65 + mapSizeX;
int x3 = 65 + mapSizeX * 2;

var p1 = s[x1];
var p2 = s[x2];
var p3 = s[x3];
  
var quadraticParams = CalculateQuadraticParameters(x1, p1, x2, p2, x3, p3);

var a = quadraticParams.a;
var b = quadraticParams.b;
var c = quadraticParams.c;

long point = 26501365;
double estimatedGardenPlots = a * Math.Pow(point, 2) + b * point + c;
Console.WriteLine("Part 2: " + estimatedGardenPlots);


void PrintMap(HashSet<(int x, int y)> stepPositions)
{
    for (int y = 0; y < mapSizeY; y++)
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            Console.SetCursorPosition(x,y);
            
            if (stepPositions.Contains((x,y)))
                Console.Write("O");
            else if ((x,y)==start)
                Console.Write("S");
            else
                Console.Write(map[x, y]);
            
        }
    }
    Thread.Sleep(500);
}

//Convert any world position to a position in the map by wrapping numbers
(int x, int y) WrapPosition(int x, int y)
{
    var mapX = MathMod(x, mapSizeX);
    var mapY = MathMod(y, mapSizeY);
    return (mapX, mapY);
}

static int MathMod(int a, int b) {
    return (Math.Abs(a * b) + a) % b;
}

(double a, double b, double c) CalculateQuadraticParameters(double x1, double y1, double x2, double y2, double x3, double y3)
{
    //Adapted from here: https://chris35wills.github.io/parabola_python/
    var denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
    var A = (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
    var B = (x3 * x3 * (y1 - y2) + x2 * x2 * (y3 - y1) + x1 * x1 * (y2 - y3)) / denom;
    var C = (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;

    return (A, B, C);
}