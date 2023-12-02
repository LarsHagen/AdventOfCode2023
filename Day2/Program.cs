using Day2;

var input = File.ReadAllLines("input.txt");

List<Game> games = new();

foreach(var line in input)
{
    games.Add(new Game(line));
}


int maxRed = 12;
int maxGreen = 13;
int maxBlue = 14;

int idSum = 0;

foreach(var game in games)
{
    if (game.Sets.Any(s => s.red > maxRed || s.green > maxGreen || s.blue > maxBlue))
        continue;

    idSum += game.Id;
}

Console.WriteLine("Part 1: " + idSum);

int sum = 0;

foreach (var game in games)
{
    var power = game.MinimumRequired.red * game.MinimumRequired.green * game.MinimumRequired.blue;

    sum += power;
}

Console.WriteLine("Part 2: " + sum);
