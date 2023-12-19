namespace Day19;

public abstract class Rule
{
    public char MachinePartParameter;
    public int Compare;
    public Workflow Workflow;

    public abstract bool RuleApplies(MachinePart machinePart);
    
    public abstract (MachinePartGroup apply, MachinePartGroup notApply) RuleApplies(MachinePartGroup machinePartGroup);
}

public class LessThanRule : Rule
{
    public override bool RuleApplies(MachinePart machinePart)
    {
        return machinePart.Values[MachinePartParameter] < Compare;
    }

    public override (MachinePartGroup apply, MachinePartGroup notApply) RuleApplies(MachinePartGroup machinePartGroup)
    {
        var apply = new MachinePartGroup(machinePartGroup);
        var notApply = new MachinePartGroup(machinePartGroup);
        
        apply.UpperBounds[MachinePartParameter] = Compare - 1;
        notApply.LowerBounds[MachinePartParameter] = Compare;
        
        return (apply, notApply);
    }
}

public class MoreThanRule : Rule
{
    public override bool RuleApplies(MachinePart machinePart)
    {
        return machinePart.Values[MachinePartParameter] > Compare;
    }
    
    public override (MachinePartGroup apply, MachinePartGroup notApply) RuleApplies(MachinePartGroup machinePartGroup)
    {
        var apply = new MachinePartGroup(machinePartGroup);
        var notApply = new MachinePartGroup(machinePartGroup);
        
        apply.LowerBounds[MachinePartParameter] = Compare + 1;
        notApply.UpperBounds[MachinePartParameter] = Compare;
        
        return (apply, notApply);
    }
}

public class AcceptRule : Rule
{
    public static List<MachinePart> AcceptedParts = new List<MachinePart>();
    public static List<MachinePartGroup> AcceptedGroups = new List<MachinePartGroup>();
    
    public override bool RuleApplies(MachinePart machinePart)
    {
        AcceptedParts.Add(machinePart);
        return true;
    }
    
    public override (MachinePartGroup apply, MachinePartGroup notApply) RuleApplies(MachinePartGroup machinePartGroup)
    {
        AcceptedGroups.Add(machinePartGroup);
        return (null, null);
    }
}

public class RejectRule : Rule
{
    public static List<MachinePart> RejectedParts = new List<MachinePart>();
    public static List<MachinePartGroup> RejectedGroups = new List<MachinePartGroup>();
    
    public override bool RuleApplies(MachinePart machinePart)
    {
        RejectedParts.Add(machinePart);
        return true;
    }
    
    public override (MachinePartGroup apply, MachinePartGroup notApply) RuleApplies(MachinePartGroup machinePartGroup)
    {
        RejectedGroups.Add(machinePartGroup);
        return (null, null);
    }
}

public class AlwaysTrue : Rule
{
    public override bool RuleApplies(MachinePart machinePart)
    {
        return true;
    }
    
    public override (MachinePartGroup apply, MachinePartGroup notApply) RuleApplies(MachinePartGroup machinePartGroup)
    {
        return (machinePartGroup, null);
    }
}