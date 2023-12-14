using Day14;

var map = new Map("input.txt");

List<char[,]> previousMaps = new();
int cycles = 1000000000;
for (int i = 0; i < cycles; i++)
{
    CopyMapToPreviousMaps(map);
    map.TiltNorth();
    
    if (i == 0)
        Console.WriteLine("Part 1: " + map.NorthBeamLoad());
    
    map.TiltWest();
    map.TiltSouth();
    map.TiltEast();

    var repeatIndex = MapExistsInPreviousMaps(map);
    
    if (repeatIndex != -1)
    {
        int loopSize = previousMaps.Count - repeatIndex;
        int remaningCycles = cycles - i;
        int loopIndex = repeatIndex + (remaningCycles % loopSize) - 1;
        
        Console.WriteLine("Part 2: " + Map.NorthBeamLoad(previousMaps[loopIndex], map.MapSizeX, map.MapSizeY));
        
        break;
    }
}

void CopyMapToPreviousMaps(Map map)
{
    char[,] mapData = new char[map.MapSizeX, map.MapSizeY];
    for (int y = 0; y < map.MapSizeY; y++)
    {
        for (int x = 0; x < map.MapSizeX; x++)
        {
            mapData[x, y] = map.MapData[x, y];
        }
    }
    previousMaps.Add(mapData);
}

int MapExistsInPreviousMaps(Map map)
{
    for (var index = 0; index < previousMaps.Count; index++)
    {
        var previousMap = previousMaps[index];
        if (MapEqual(map, previousMap))
            return index;
    }

    return -1;
}

bool MapEqual(Map map, char[,] mapPrevious)
{
    for (int y = 0; y < map.MapSizeY; y++)
    {
        for (int x = 0; x < map.MapSizeX; x++)
        {
            if (map.MapData[x, y] != mapPrevious[x, y])
                return false;
        }
    }

    return true;
}