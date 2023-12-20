using Day20;

var lines = File.ReadAllLines("input.txt");

List<Module> modules = new();
//modules.Add(new EndModule("output"));
var rxModule = new EndModule("rx");
modules.Add(rxModule);
Module start = null;

foreach (var line in lines)
{
    if (line.Contains("broadcaster"))
    {
        start = new Broadcaster(line);
        modules.Add(start);
    }
    else if (line.Contains("%"))
        modules.Add(new FlipFlop(line));
    else
        modules.Add(new Conjuction(line));
}

foreach(var module in modules)
    module.WireOutputs();

int lowSignals = 0;
int highSignals = 0;

int i = 0;
int part2Answer = 0;

//It seems like rx has one input node, it's a conjunction node so it will return 0 if all input is high
//This conjunction node has 4 inputs, so it will return 0 if all 4 inputs are high
//My assumption is that each of these nodes run on a cycle like example 1, if we find the number of button
//presses it takes for each individual input to be high, we can find the least common multiple of these
var rxInput = rxModule.Inputs.First();
Dictionary<string, int> highInput = new();
rxInput.OnReceiveSignal += (sender, signal) =>
{
    if (signal == 0)
        return;

    if (highInput.ContainsKey(sender.Name))
        return;

    highInput.Add(sender.Name, i);
};

//10000 button presses seems to be more than enough to find the cycle of each input to the conjunction node
for (i = 0; i < 10000; i++)
{
    Queue<(Module module, Module sender, int signal)> signalQueue = new();
    signalQueue.Enqueue((start, null, 0));
    lowSignals++;

    while (signalQueue.Count > 0)
    {
        var current = signalQueue.Dequeue();
        var module = current.module;
        var signal = current.signal;
        var response = module.ReceiveSignal(current.sender, signal);
        if (response.HasValue)
        {
            foreach (var output in module.Outputs)
            {
                if (response.Value == 0)
                    lowSignals++;
                else
                    highSignals++;

                signalQueue.Enqueue((output, module, response.Value));
            }
        }
    }
    
    if (i == 999)
        Console.WriteLine("Part 1: " + (lowSignals * highSignals));
}

var lcm = MathNet.Numerics.Euclid.LeastCommonMultiple(highInput.Values.Select(v => (long) v + 1).ToList());
Console.WriteLine("Part 2: " + lcm);