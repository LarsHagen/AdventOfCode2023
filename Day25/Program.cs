using Day25;

var nodes = File.ReadAllLines("input.txt").Select(s => new Node(s)).ToList();
nodes.ForEach(n => n.Connect());
nodes = Node.AllNodes.Values.ToList();

foreach (var node in nodes)
{
    node.CalculatePaths();
}

var random = new Random();

Dictionary<Connection, int> connectionUsages = new();
for (int i = 0; i < 10000; i++)
{
    var start = nodes[random.Next(0, nodes.Count - 1)];
    var end = nodes[random.Next(0, nodes.Count - 1)];
    
    if (start == end)
        continue;
    
    start.CalculatePaths();

    var current = end;
    var previous = current.Previous;

    while (previous != null)
    {
        var connection = new Connection(current, previous);

        if (!connectionUsages.ContainsKey(connection))
            connectionUsages.Add(connection, 1);
        else
            connectionUsages[connection]++;

        current = previous;
        previous = current.Previous;
    }
}

var orderedConnections = connectionUsages.OrderByDescending(c => c.Value).ToList();

/*foreach (var connectionUsage in orderedConnections)
{
    Console.WriteLine(connectionUsage.Key.a.Name + "/" + connectionUsage.Key.b.Name + ": " + connectionUsage.Value);
}*/

for (int i = 0; i < 3; i++)
{
    orderedConnections[i].Key.a.RemoveConnection(orderedConnections[i].Key.b);
}

int groupA = nodes[0].CalculatePaths();
int groupB = nodes.Count - groupA;

//Console.WriteLine("Group A: " + groupA);
//Console.WriteLine("Group B: " + groupB);
Console.WriteLine("Part 1: " + groupA * groupB);
    
