namespace Day19;

public class MachinePart
{
    public Dictionary<char,int> Values = new();

    public MachinePart(string input)
    {
        input = input.Replace("{","").Replace("}","");
        var split = input.Split(",");
        foreach (var substring in split)
        {
            var split2 = substring.Split("=");
            Values.Add(split2[0][0], int.Parse(split2[1]));
        }
    }
}