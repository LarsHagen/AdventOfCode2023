

using Day13;

var lines = File.ReadAllLines("input.txt");

List<Map> maps = new();
List<string> linesForNextMap = new();

foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        CreateMap(linesForNextMap);

        continue;
    }
    
    linesForNextMap.Add(line);
}

CreateMap(linesForNextMap);

int sum = 0;

foreach (var map in maps)
{
    var value = map.MirrorIndex;
    if (map.Horizontal)
        value *= 100;
    
    sum += value;
}

Console.WriteLine("Part 1: " + sum);


void CreateMap(List<string> list)
{
    var map = new Map(list);
    maps.Add(map);
    list.Clear();

    Console.WriteLine("Horizontal? " + map.Horizontal + "... Mirror index: " + map.MirrorIndex);
}


