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

int sumClear = 0;
int sumSmudge = 0;

foreach (var map in maps)
{
    var valueClear = map.MirrorIndexClear;
    if (map.HorizontalClear)
        valueClear *= 100;
    
    sumClear += valueClear;
    
    var valueSmudge = map.MirrorIndexSmudge;
    if (map.HorizontalSmudge)
        valueSmudge *= 100;
    
    sumSmudge += valueSmudge;
}

Console.WriteLine("Part 1: " + sumClear);
Console.WriteLine("Part 2: " + sumSmudge);


void CreateMap(List<string> list)
{
    var map = new Map(list);
    maps.Add(map);
    list.Clear();
}


