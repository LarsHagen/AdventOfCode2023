
var lines = File.ReadAllLines("input.txt");

int sum = 0;

foreach (var line in lines)
{
    char first = line.First(c => c >= '0' && c <= '9');
    char second = line.Last(c => c >= '0' && c <= '9');

    int number = int.Parse("" + first + second);
    sum += number;
}

Console.WriteLine("Part 1: " + sum);

sum = 0;

List<string> numbersAsStrings = new() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
List<char> numbersAsChars = new() { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

foreach (var line in lines)
{
    List<int> numbers = new();

    for (int i = 0; i < line.Length; i++)
    {
        var c = line[i];

        if (numbersAsChars.Contains(c))
        {
            numbers.Add(int.Parse(c.ToString()));
            continue;
        }

        var subString = line.Substring(i);

        for (int j = 0; j < numbersAsStrings.Count; j++)
        {
            string numberAsString = numbersAsStrings[j];
            if (subString.StartsWith(numberAsString))
            {
                numbers.Add(j + 1);
                break;
            }
        }
    }

    string number = "" + numbers.First() + numbers.Last();
    sum += int.Parse(number);
}

Console.WriteLine("Part 2: " + sum);
