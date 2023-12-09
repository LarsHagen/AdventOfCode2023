
using Day9;

var lines = File.ReadAllLines("input.txt");

int sumPart1 = 0;
int sumPart2 = 0;

foreach (var line in lines)
{
    Reading reading = new Reading(line);
    sumPart1 += reading.Extrapolate();
    sumPart2 += reading.ExtrapolateBack();
}

Console.WriteLine("Part 1: " + sumPart1);
Console.WriteLine("Part 2: " + sumPart2);
