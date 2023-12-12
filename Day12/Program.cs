using Day12;

var lines = File.ReadAllLines("input.txt");

ulong sum = 0;

foreach (var line in lines)
{
    var record = new ConditionRecord(line);
    sum += record.PossibleConditions;
}

Console.WriteLine("Part 1: " + sum);

sum = 0;

foreach (var line in lines)
{
    var condition = line.Split(" ")[0];
    var groups = line.Split(" ")[1];

    var unfolded = condition + "?" +
                   condition + "?" +
                   condition + "?" +
                   condition + "?" +
                   condition + " " +
                   groups + "," +
                   groups + "," +
                   groups + "," +
                   groups + "," +
                   groups;
    
    
    var record = new ConditionRecord(unfolded);
    sum += (ulong)record.PossibleConditions;
}

Console.WriteLine("Part 2: " + sum);