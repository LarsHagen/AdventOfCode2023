using Day23;

//Please note that example input has been modified. It has been extended up and down with one line to better match
//the input. That also means that output values are off by two for example input
var lines = File.ReadAllLines("input.txt");
var mapSizeX = lines[0].Length;
var mapSizeY = lines.Length;
char[,] map = new char[mapSizeX, mapSizeY];

for (int y = 0; y < mapSizeY; y++)
{
    for (int x = 0; x < mapSizeX; x++)
    {
        map[x,y] = lines[y][x];
    }
}

map[1, 1] = 'v';
map[mapSizeX-2, mapSizeY-2] = 'v';

Dictionary<(int x, int y), Node> nodes = new();
Node start = new Node()
{
    Position = (1,0)
};
Node end = new Node()
{
    Position = (mapSizeX-2,mapSizeY-1)
};

nodes.Add(start.Position, start);
nodes.Add(end.Position, end);

Queue<Node> queue = new();
queue.Enqueue(start);

while (queue.Count > 0)
{
    var current = queue.Dequeue();
    if (current == end)
        continue;
    
    var currentPos = current.Position;
    
    List<(int x, int y)> outDirections = new();
    if (currentPos.x < mapSizeX - 1 && map[currentPos.x + 1, currentPos.y] == '>')
        outDirections.Add((currentPos.x + 1, currentPos.y));
    if (currentPos.x > 0 && map[currentPos.x - 1, currentPos.y] == '<')
        outDirections.Add((currentPos.x-1, currentPos.y));
    if (currentPos.y < mapSizeY - 1 && map[currentPos.x, currentPos.y + 1] == 'v')
        outDirections.Add((currentPos.x, currentPos.y+1));
    if (currentPos.y > 0 && map[currentPos.x, currentPos.y - 1] == '^')
        outDirections.Add((currentPos.x, currentPos.y-1));

    foreach (var outDirection in outDirections)
    {
        int distance = 0;
        currentPos = outDirection;
        map[currentPos.x, currentPos.y] = '-';

        distance ++;
        
        while (true)
        {
            var x = currentPos.x;
            var y = currentPos.y;
            map[x, y] = '-';
            
            distance ++;
            
            if (map[x + 1, y] == '.')
            {
                currentPos = (x + 1, y);
                continue;
            }

            if (map[x - 1, y] == '.')
            {
                currentPos = (x - 1, y);
                continue;
            }

            if (map[x, y+1] == '.')
            {
                currentPos = (x, y+1);
                continue;
            }
            
            if (map[x, y-1] == '.')
            {
                currentPos = (x, y-1);
                continue;
            }
            
            if (map[x + 1, y] == '>')
            {
                var newNode = AddNode(x + 2, y, current, distance + 1);
                if (!queue.Contains(newNode))
                    queue.Enqueue(newNode);
                break;
            }

            if (map[x - 1, y] == '<')
            {
                var newNode = AddNode(x - 2, y, current, distance + 1);
                if (!queue.Contains(newNode))
                    queue.Enqueue(newNode);
                break;
            }

            if (map[x, y+1] == 'v')
            {
                var newNode = AddNode(x, y + 2, current, distance + 1);
                if (!queue.Contains(newNode))
                    queue.Enqueue(newNode);
                break;
            }
            
            if (map[x, y-1] == '^')
            {
                var newNode = AddNode(x, y - 2, current, distance + 1);
                if (!queue.Contains(newNode))
                    queue.Enqueue(newNode);
                break;
            }

            throw new Exception("Should never reach here!");
        }
    }
}

Node AddNode(int x, int y, Node inNode, int distance)
{
    if (nodes.TryGetValue((x, y), out var node))
    {
        node.AddInNode(inNode, distance);
    }
    else
    {
        node = new Node()
        {
            Position = (x, y)
        };
        node.AddInNode(inNode, distance);
        nodes.Add(node.Position, node);
    }

    return node;
}

queue.Clear();
queue.Enqueue(end);

while (queue.Count > 0)
{
    var current = queue.Dequeue();
    foreach (var connectionIn in current.ConnectIn) 
    {
        var distToEnd = current.LongestDistanceToEndFromHere + connectionIn.Distance;
        if (distToEnd > connectionIn.To.LongestDistanceToEndFromHere)
        {
            connectionIn.To.LongestDistanceToEndFromHere = distToEnd;
            connectionIn.To.LongPathOut = current;
            queue.Enqueue(connectionIn.To);
        }
    }
}

Console.WriteLine("Part 1: " + start.LongestDistanceToEndFromHere);
Console.WriteLine("Part 2 is not very optimized, so it takes a minute or two to run.");

//Make sure all nodes connect both ways
foreach (var node in nodes.Values)
{
    foreach (var connection in node.ConnectOut)
    {
        if (connection.To.ConnectOut.Any(c => c.To == node))
            continue;
        connection.To.ConnectOut.Add(new Connection()
        {
            Distance = connection.Distance,
            To = node
        });
    }
}

List<List<Node>> paths = new();
List<Node> initialPath = new();
initialPath.Add(start);
GetPathsRecursive(start, end, ref paths, initialPath);

void GetPathsRecursive(Node currentNode, Node endNode, ref List<List<Node>> allPaths, List<Node> currentPath)
{
    if (currentNode == endNode)
    {
        allPaths.Add(currentPath);
        return;
    }
    
    foreach (var connection in currentNode.ConnectOut)
    {
        if (currentPath.Contains(connection.To))
            continue;
        var newPath = new List<Node>(currentPath);
        newPath.Add(connection.To);
        GetPathsRecursive(connection.To, endNode, ref allPaths, newPath);
    }
}

int longestPath = 0;
foreach (var path in paths)
{
    int length = 0;
    for (int i = 0; i < path.Count-1; i++)
    {
        length += path[i].ConnectOut.First(c => c.To == path[i+1]).Distance;
    }
    longestPath = Math.Max(longestPath, length);
}
Console.WriteLine("Part 2: " + longestPath);