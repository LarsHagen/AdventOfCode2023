namespace Day18;

public class DigInstruction
{
    public (long x, long y) Direction;
    public long Distance;
    public string Color;

    public DigInstruction(string input)
    {
        var split = input.Split(" ");

        Direction = split[0] switch
        {
            "U" => (0, -1),
            "D" => (0, 1),
            "R" => (1, 0),
            "L" => (-1, 0),
            _ => throw new Exception()
        };
        Distance = int.Parse(split[1]);
        Color = split[2].Replace("(", "").Replace(")", "");
    }
}