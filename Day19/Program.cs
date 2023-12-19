using Day19;

var lines = File.ReadAllLines("input.txt");

List<MachinePart> machineParts = new();
List<Workflow> workflows = new();

Workflow initialWorkflow = null;

foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
        continue;
    
    if (line.StartsWith("{"))
        machineParts.Add(new MachinePart(line));
    else
    {
        workflows.Add(new Workflow(line));
        if (line.StartsWith("in"))
            initialWorkflow = workflows.Last();
    }
}

workflows.Add(new Workflow("R{R}"));
workflows.Add(new Workflow("A{A}"));

foreach(var workflow in workflows)
    workflow.CreateRules();

foreach(var machinePart in machineParts)
{
    Workflow currentWorkflow = initialWorkflow;

    while (currentWorkflow != null)
    {
        var rule = currentWorkflow.Execute(machinePart);
        currentWorkflow = rule.Workflow;
    }
}

long sum = 0;

foreach (var machinePart in AcceptRule.AcceptedParts)
{
    sum += machinePart.Values.Values.Sum();
    Console.WriteLine(machinePart.Values['x'] + " " + machinePart.Values['m'] + " " + machinePart.Values['a'] + " " +
                      machinePart.Values['s']);
}

Console.WriteLine("Part 1: " + sum);


