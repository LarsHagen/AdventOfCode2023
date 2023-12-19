namespace Day19;

public class Workflow
{
    public static Dictionary<string, Workflow> Workflows = new Dictionary<string, Workflow>();

    public List<Rule> Rules = new List<Rule>();
    
    private string[] _rulesInput;
    
    public Workflow(string input)
    {
        var name = input.Split("{")[0];
        _rulesInput = input.Split("{")[1].Replace("}","").Split(",");
        Workflows.Add(name, this);
    }

    public Rule Execute(MachinePart machinePart)
    {
        foreach (var rule in Rules)
        {
            if (rule.RuleApplies(machinePart))
                return rule;
        }

        throw new Exception();
    }
    
    public void CreateRules()
    {
        foreach (var ruleInput in _rulesInput)
        {
            if (ruleInput == "A")
            {
                Rules.Add(new AcceptRule());
                continue;
            }
            
            if (ruleInput == "R")
            {
                Rules.Add(new RejectRule());
                continue;
            }
            
            if (ruleInput.Contains(">"))
            {
                var splitA = ruleInput.Split(">");
                var splitB = splitA[1].Split(":");

                var parameterName = splitA[0][0];
                var value = int.Parse(splitB[0]);
                var workflow = Workflows[splitB[1]];
                
                
                Rules.Add(new MoreThanRule()
                {
                    machinePartParameter = parameterName,
                    compare = value,
                    Workflow = workflow
                });
                continue;
            }
            
            if (ruleInput.Contains("<"))
            {
                var splitA = ruleInput.Split("<");
                var splitB = splitA[1].Split(":");

                var parameterName = splitA[0][0];
                var value = int.Parse(splitB[0]);
                var workflow = Workflows[splitB[1]];
                
                
                Rules.Add(new LessThanRule()
                {
                    machinePartParameter = parameterName,
                    compare = value,
                    Workflow = workflow
                });
                continue;
            }
            
            Rules.Add(new AlwaysTrue()
            {
                Workflow = Workflows[ruleInput]
            });
        }
    }
}