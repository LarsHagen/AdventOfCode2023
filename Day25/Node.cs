namespace Day25;

public class Node
{
    public string Name;
    public static Dictionary<string, Node> AllNodes = new();
    public List<Node> Connections = new();

    private string[] connectionsRaw;
    
    //Pathfinding
    public Node Previous;
    public int Distance;
    
    public Node(string input)
    {
        var options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
        Name = input.Split(":",options)[0];
        connectionsRaw = input.Split(":")[1].Split(" ", options);
        
        AllNodes.Add(Name, this);
    }

    public Node() {}
    
    public void Connect()
    {
        foreach (var connection in connectionsRaw)
        {
            if (!AllNodes.ContainsKey(connection))
            {
                AllNodes.Add(connection, new Node()
                {
                    Name = connection
                });
            }
            AddConnection(AllNodes[connection]);
        }
    }
    
    public void AddConnection(Node node)
    {
        Connections.Add(node);
        node.Connections.Add(this);
    }

    public void RemoveConnection(Node node)
    {
        Connections.Remove(node);
        node.Connections.Remove(this);
    }

    public static void ClearPreviousPathfinding()
    {
        foreach (var node in AllNodes.Values)
        {
            node.Previous = null;
            node.Distance = int.MaxValue;
        }
    }
    
    public int CalculatePaths()
    {
        HashSet<Node> reachedNodes = new();
        ClearPreviousPathfinding();
        Queue<Node> queue = new();
        queue.Enqueue(this);
        Distance = 0;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            reachedNodes.Add(current);

            foreach (var connection in current.Connections)
            {
                if (Distance + 1 < connection.Distance)
                {
                    connection.Previous = current;
                    connection.Distance = Distance + 1;
                    queue.Enqueue(connection);
                }
            }
        }
        return reachedNodes.Count;
    }
}