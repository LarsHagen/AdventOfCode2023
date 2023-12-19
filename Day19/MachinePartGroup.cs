namespace Day19;

public class MachinePartGroup
{
    /*public int LowerX;
    public int UpperX;
    public int LowerM;
    public int UpperM;
    public int LowerA;
    public int UpperA;
    public int LowerS;
    public int UpperS;*/
    
    public Dictionary<char, int> LowerBounds = new();
    public Dictionary<char, int> UpperBounds = new();

    public MachinePartGroup()
    {
        LowerBounds.Add('x', 1);
        LowerBounds.Add('m', 1);
        LowerBounds.Add('a', 1);
        LowerBounds.Add('s', 1);
        
        UpperBounds.Add('x', 4000);
        UpperBounds.Add('m', 4000);
        UpperBounds.Add('a', 4000);
        UpperBounds.Add('s', 4000);
    }

    public MachinePartGroup(MachinePartGroup copy)
    {
        LowerBounds = new(copy.LowerBounds);
        UpperBounds = new(copy.UpperBounds);
    }

    public long Count()
    {
        return (long) (UpperBounds['x'] - LowerBounds['x'] + 1) *
               (long) (UpperBounds['m'] - LowerBounds['m'] + 1) *
               (long) (UpperBounds['a'] - LowerBounds['a'] + 1) *
               (long) (UpperBounds['s'] - LowerBounds['s'] + 1);
    }
}