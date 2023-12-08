using Day8;

Dictionary<string, Node> nodes = new();

var lines = File.ReadAllLines("input.txt");

var instructions = lines[0];

for (int i = 2; i < lines.Length; i++)
{
    var nodeName = lines[i].Split("=")[0].Trim();
    var leftRight = lines[i].Split("=")[1].Trim().Replace("(","").Replace(")","").Split(",");
    var left = leftRight[0].Trim();
    var right = leftRight[1].Trim();
    
    nodes.Add(nodeName, new Node()
    {
        Name = nodeName,
        Left = left,
        Right = right
    });
}

Node node = nodes["AAA"];
Node goal = nodes["ZZZ"];

int instructionIndex = 0;

int steps = 0;

while (node != goal)
{
    var instruction = instructions[instructionIndex];
    instructionIndex++;
    if (instructionIndex == instructions.Length)
        instructionIndex= 0;
    
    node = instruction == 'L' ? nodes[node.Left] : nodes[node.Right];
    steps++;
}

Console.WriteLine("Part 1: " + steps);



List<Node> startNodesPart2 = new();
foreach(Node n in nodes.Values)
{
    if (n.Name.EndsWith("A"))
        startNodesPart2.Add(n);
}

Dictionary<Node, int> stepsToEnd = new();

foreach (Node n in startNodesPart2)
{
    var current = n;
    steps = 0;
    while (!current.Name.EndsWith("Z"))
    {
        var instruction = instructions[instructionIndex];
        instructionIndex++;
        if (instructionIndex == instructions.Length)
            instructionIndex= 0;
    
        current = instruction == 'L' ? nodes[current.Left] : nodes[current.Right];
        steps++;
    }
    
    stepsToEnd.Add(n, steps);
}


List<long> stepsAsLong = stepsToEnd.Values.Select(step => (long) step).ToList();

var lcm = MathNet.Numerics.Euclid.LeastCommonMultiple(stepsAsLong);

Console.WriteLine("Part 2: " + lcm);