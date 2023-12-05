
using Day5;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

var lines = File.ReadAllLines("input.txt");

var seedsString = lines[0].Replace("seeds: ", "");
var seeds = seedsString.Split(" ").Select(seed => long.Parse(seed)).ToList();


List<Map> maps = new();
List<string> mapStrings = new();
for (int i = 3; i < lines.Length; i++)
{
    var line = lines[i];
    if (string.IsNullOrEmpty(line))
    {
        maps.Add(new Map(mapStrings));
        mapStrings.Clear();
        i++;
        continue;
    }

    mapStrings.Add(line);
}
maps.Add(new Map(mapStrings));

long lowestLocation = long.MaxValue;

foreach (var seed in seeds)
{
    var number = seed;

    foreach (var map in maps)
    {
        number = map.GetDestination(number);
    }

    if (number < lowestLocation)
        lowestLocation = number;
}

Console.WriteLine("Part 1: " + lowestLocation);

//Part 2 is just bruteforce. Not pretty..

lowestLocation = long.MaxValue;

for (int i=0; i < seeds.Count; i += 2)
{
    Console.WriteLine(i + "/" + seeds.Count);
    for (long seed = seeds[i]; seed < seeds[i] + seeds[i + 1]; seed++)
    {
        var number = seed;

        foreach (var map in maps)
        {
            number = map.GetDestination(number);
        }

        if (number < lowestLocation)
            lowestLocation = number;
    }
}

Console.WriteLine("Part 2: " + lowestLocation);