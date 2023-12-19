namespace Day19;

public abstract class Rule
{
    public char machinePartParameter;
    public int compare;
    public Workflow Workflow;

    public abstract bool RuleApplies(MachinePart machinePart);
}

public class LessThanRule : Rule
{
    public override bool RuleApplies(MachinePart machinePart)
    {
        return machinePart.Values[machinePartParameter] < compare;
    }
}

public class MoreThanRule : Rule
{
    public override bool RuleApplies(MachinePart machinePart)
    {
        return machinePart.Values[machinePartParameter] > compare;
    }
}

public class AcceptRule : Rule
{
    public static List<MachinePart> AcceptedParts = new List<MachinePart>();
    
    public override bool RuleApplies(MachinePart machinePart)
    {
        AcceptedParts.Add(machinePart);
        return true;
    }
}

public class RejectRule : Rule
{
    public static List<MachinePart> RejectedParts = new List<MachinePart>();
    
    public override bool RuleApplies(MachinePart machinePart)
    {
        RejectedParts.Add(machinePart);
        return true;
    }
}

public class AlwaysTrue : Rule
{
    public override bool RuleApplies(MachinePart machinePart)
    {
        return true;
    }
}