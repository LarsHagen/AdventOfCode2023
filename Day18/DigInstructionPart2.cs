namespace Day18;

public class DigInstructionPart2 : DigInstruction
{
    public DigInstructionPart2(string input) : base(input)
    {
        var distanceHex = "0x" + Color.Substring(1, 5);
        var directionHex = "0x" + Color.Substring(6, 1);
        
        Distance = Convert.ToInt32(distanceHex, 16);
        var direction = Convert.ToInt32(directionHex, 16);
        
        Direction = direction switch
        {
            0 => (1, 0),
            1 => (0, 1),
            2 => (-1, 0),
            3 => (0, -1),
            _ => throw new Exception()
        };
    }
}