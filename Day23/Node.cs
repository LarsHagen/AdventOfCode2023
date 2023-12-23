namespace Day23;

public class Node
{
    public List<Connection> ConnectOut = new();
    public List<Connection> ConnectIn = new();
    
    public (int x, int y) Position;
    
    public int LongestDistanceToEndFromHere = 0;
    public Node LongPathOut = null;

    public void AddInNode(Node inNode, int distance)
    {
        ConnectIn.Add(new Connection()
        {
            To = inNode,
            Distance = distance
        });
        inNode.ConnectOut.Add(new Connection()
        {
            To = this,
            Distance = distance
        });
    }
}

public class Connection
{
    public Node To;
    public int Distance;
}