using Day6;

var lines = File.ReadAllLines("example.txt");

var times = lines[0].Replace("Time:", "").Trim().Split(" ").Where(s => !string.IsNullOrEmpty(s)).Select(s => int.Parse(s)).ToList();
var distances = lines[1].Replace("Distance:", "").Trim().Split(" ").Where(s => !string.IsNullOrEmpty(s)).Select(s => int.Parse(s)).ToList();

List<int> numWaysToWin = new();

for (int i = 0; i < times.Count; i++)
{
    int sum = 0;

    var time = times[i];
    var record = distances[i];

    for (int t = 0; t <= time; t++)
    {
        var remaning = time - t;
        var speed = t;

        var distance = remaning * speed;

        if (distance > record)
            sum++;
    }


    numWaysToWin.Add(sum);
}


int score = numWaysToWin.Aggregate(1, (a, b) => a * b);

Console.WriteLine("Part 1: " + score);

var part2Time = long.Parse(lines[0].Replace("Time:", "").Replace(" ", ""));
var part2Record = long.Parse(lines[1].Replace("Distance:", "").Replace(" ", ""));

var steps = 100;
var stepSize = part2Time / 100;

//Build binary trees to find lowest amount of time and highest amount of time
LowNode low = new LowNode(0, part2Time, part2Time, part2Record);
HighNode high = new HighNode(part2Time - 1, part2Time, part2Time, part2Record);

//Rounding errors mean we might now have found the best just yet. Test one lower and higher
long l = LowNode.bestFit;
if (Helper.IsBetter(l - 1, part2Time, part2Record))
    l--;

long h = HighNode.bestFit;
if (Helper.IsBetter(h + 1, part2Time, part2Record))
    h++;

Console.WriteLine("Part 2: " + (h - l + 1));