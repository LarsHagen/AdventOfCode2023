var lines = File.ReadAllLines("input.txt");

int sum = 0;

Dictionary<(int x, int y), List<int>> gears = new();

for (int y = 0; y < lines.Length; y++)
{
    var line = lines[y];

    for (int x = 0; x < line.Length; x++)
    {
        var c = line[x];

        if (c < '0' || c > '9')
            continue;

        //get length of number
        int length = 0;
        for (length = 1; length < line.Length - x; length++)
        {
            if (line[x + length] < '0' || line[x + length] > '9')
                break;
        }

        int number = int.Parse(line.Substring(x, length));

        //See if any neighbor is symbol
        var hit = AnyNeighborIsSymbol(x, y, length);

        if (hit.hit)
        {
            sum += number;

            //build gears
            if (lines[hit.y][hit.x] == '*')
            {
                (int x, int y) location = (hit.x, hit.y);
                if (!gears.ContainsKey(location))
                    gears.Add(location, new List<int>());

                gears[location].Add(number);
            }

        }

        x += length;
    }
}

Console.WriteLine("Part 1: " + sum);

sum = 0;

foreach(var partNumbers in gears.Values)
{
    if (partNumbers.Count != 2)
        continue;

    var gearRatio = partNumbers[0] * partNumbers[1];

    sum += gearRatio;
}

Console.WriteLine("Part 2: " + sum);


(bool hit, int x, int y) AnyNeighborIsSymbol(int startX, int startY, int numberLength)
{
    for (int x = startX - 1; x < startX + numberLength + 1; x++)
    {
        if (x < 0)
            continue;
        if (x >= lines[0].Length)
            continue;

        for (int y = startY - 1; y <= startY + 1; y++)
        {
            if (y < 0)
                continue;
            if (y >= lines.Length)
                continue;


            var c = lines[y][x];

            if (CharIsSymbol(c))
                return (true,x,y);
        }
    }

    return (false,0,0);
}

bool CharIsSymbol(char c)
{
    if (c >= '0' && c <= '9')
        return false;

    if (c == '.')
        return false;

    return true;
}