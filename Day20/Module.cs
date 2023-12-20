namespace Day20;

public abstract class Module
{
    public static Dictionary<string, Module> Modules = new();

    public string Name;
    public string[] OutputNames;
    public List<Module> Inputs = new();
    public List<Module> Outputs = new();
    
    public Action<Module, int> OnReceiveSignal;

    public void ProcessInputString(string input)
    {
        var split = input.Split("->");
        Name = split[0].Trim();
        OutputNames = split[1].Split(",").Select(n => n.Trim()).ToArray();
        Modules.Add(Name, this);
    }
    
    public void WireOutputs()
    {
        foreach (var outputName in OutputNames)
        {
            var output = Modules[outputName];
            WireOutput(output);
        }
    }
    
    public void WireOutput(Module input)
    {
        Outputs.Add(input);
        input.Inputs.Add(this);
    }

    public abstract int? ReceiveSignal(Module sender, int signal);
}

public class FlipFlop : Module
{
    bool state = false;
    
    public FlipFlop(string input)
    {
        ProcessInputString(input.Replace("%",""));
    }

    public override int? ReceiveSignal(Module sender, int signal)
    {
        OnReceiveSignal?.Invoke(sender, signal);
        if (signal == 1)
            return null;
        
        state = !state;
        return state ? 1 : 0;
    }
}

public class Conjuction : Module
{
    Dictionary<Module, int> memory = new();
    
    public Conjuction(string input)
    {
        ProcessInputString(input.Replace("&",""));
    }
    
    public override int? ReceiveSignal(Module sender, int signal)
    {
        OnReceiveSignal?.Invoke(sender, signal);
        if (memory.Count == 0)
            Init();
        
        memory[sender] = signal;

        if (memory.Values.Any(pulse => pulse == 0))
            return 1;
        return 0;
    }

    private void Init()
    {
        foreach (var module in Inputs)
        {
            memory.Add(module, 0);
        }
    }
}

public class Broadcaster : Module
{
    public Broadcaster(string input)
    {
        ProcessInputString(input);
    }

    public override int? ReceiveSignal(Module sender, int signal)
    {
        OnReceiveSignal?.Invoke(sender, signal);
        return signal;
    }
}

public class EndModule : Module
{
    public EndModule(string moduleName)
    {
        Name = moduleName;
        OutputNames = new string[0];
        Modules.Add(Name, this);
    }

    public override int? ReceiveSignal(Module sender, int signal)
    {
        OnReceiveSignal?.Invoke(sender, signal);
        return null;
    }
}