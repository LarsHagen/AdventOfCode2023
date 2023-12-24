namespace Day24;

public class Line
{
    public (double x, double y, double z) P1;
    public (double x, double y, double z) P2;
    public double Speed;
    public (double x, double y, double z) Direction;
    public (double x, double y, double z) NormalizedDirection;
    
    public Line(string line)
    {
        var parts = line.Split("@");
        var first = parts[0].Split(",").Select(s => double.Parse(s)).ToArray();
        var second = parts[1].Split(",").Select(s => double.Parse(s)).ToArray();

        P1 = (first[0], first[1], first[2]);
        Direction = (second[0], second[1], second[2]);
        P2 = (P1.x + Direction.x, P1.y + Direction.y, P1.z + Direction.z);
        
        Speed = Math.Sqrt(Math.Pow(Direction.x, 2) + Math.Pow(Direction.y, 2) + Math.Pow(Direction.z, 2));
        NormalizedDirection = (Direction.x / Speed, Direction.y / Speed, Direction.z / Speed);
    }
}