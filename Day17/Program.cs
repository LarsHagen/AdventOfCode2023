using Day17;

Console.WriteLine("Warning! Solution is not very well optimized. Each answer takes several minutes to compute");

var lines = File.ReadAllLines("input.txt");
var mapSizeX = lines[0].Length;
var mapSizeY = lines.Length;

int[,] map = new int[mapSizeX, mapSizeY];
Dictionary<(int x, int y), List<Node>> nodes = new();

for (int y = 0; y < mapSizeY; y++)
{
    for (int x = 0; x < mapSizeX; x++)
    {
        map[x, y] = int.Parse("" + lines[y][x]);
        nodes.Add((x, y), new());
    }
}

Node start = new Node()
{
    X = 0,
    Y = 0
};
nodes[(0, 0)].Add(start);


Queue<Node> queue = new Queue<Node>();
queue.Enqueue(start);

while (queue.Count > 0)
{
    var current = queue.Dequeue();

    var validMoves = current.GetValidMoves();

    foreach(var move in validMoves)
    {
        var posX = current.X + move.x;
        var posY = current.Y + move.y;

        if (posX < 0 || posY < 0 || posX >= mapSizeX || posY >= mapSizeY)
            continue;

        var stepsX = move.x != 0 ? current.StepsLineX + move.x : move.x;
        var stepsY = move.y != 0 ? current.StepsLineY + move.y : move.y;
        var heatLoss = current.HeatLoss + map[posX, posY];

        var nodeAtPositionWithSameInput = nodes[(posX, posY)].Where(n => n.StepsLineX == stepsX && n.StepsLineY == stepsY).FirstOrDefault();
        if (nodeAtPositionWithSameInput != null)
        {
            if (nodeAtPositionWithSameInput.HeatLoss <= heatLoss)
                continue;

            nodeAtPositionWithSameInput.Previous = current;
            nodeAtPositionWithSameInput.HeatLoss = heatLoss;

            if (!queue.Contains(nodeAtPositionWithSameInput))
                queue.Enqueue(nodeAtPositionWithSameInput);
        }
        else
        {
            Node newNode = new Node() 
            {
                X = posX, 
                Y = posY,
                StepsLineX = stepsX,
                StepsLineY = stepsY,
                Previous = current,
                HeatLoss = heatLoss
            };
            queue.Enqueue(newNode);
            nodes[(posX, posY)].Add(newNode);
        }
    }
}

var leastHeatloss = nodes[(mapSizeX - 1, mapSizeY - 1)].Min(n => n.HeatLoss);
Console.WriteLine("Part 1: " + leastHeatloss);

foreach (var node in nodes.Values)
    node.Clear();


queue = new Queue<Node>();
queue.Enqueue(start);

while (queue.Count > 0)
{
    var current = queue.Dequeue();

    var validMoves = current.GetValidMovesUltraCrucible();

    foreach (var move in validMoves)
    {
        var posX = current.X + move.x;
        var posY = current.Y + move.y;

        if (posX < 0 || posY < 0 || posX >= mapSizeX || posY >= mapSizeY)
            continue;

        var stepsX = move.x != 0 ? current.StepsLineX + move.x : move.x;
        var stepsY = move.y != 0 ? current.StepsLineY + move.y : move.y;
        var heatLoss = current.HeatLoss + map[posX, posY];

        var nodeAtPositionWithSameInput = nodes[(posX, posY)].Where(n => n.StepsLineX == stepsX && n.StepsLineY == stepsY).FirstOrDefault();
        if (nodeAtPositionWithSameInput != null)
        {
            if (nodeAtPositionWithSameInput.HeatLoss <= heatLoss)
                continue;

            nodeAtPositionWithSameInput.Previous = current;
            nodeAtPositionWithSameInput.HeatLoss = heatLoss;

            if (!queue.Contains(nodeAtPositionWithSameInput))
                queue.Enqueue(nodeAtPositionWithSameInput);
        }
        else
        {
            Node newNode = new Node()
            {
                X = posX,
                Y = posY,
                StepsLineX = stepsX,
                StepsLineY = stepsY,
                Previous = current,
                HeatLoss = heatLoss
            };
            queue.Enqueue(newNode);
            nodes[(posX, posY)].Add(newNode);
        }
    }
}

leastHeatloss = nodes[(mapSizeX - 1, mapSizeY - 1)].Where(n => Math.Abs(n.StepsLineX) >= 4 || Math.Abs(n.StepsLineY) >= 4).Min(n => n.HeatLoss);
Console.WriteLine("Part 2: " + leastHeatloss);