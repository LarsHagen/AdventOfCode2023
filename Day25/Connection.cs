namespace Day25;

public struct Connection
{
    public Node a;
    public Node b;
    
    public Connection(Node a, Node b)
    {
        if (a.Name.CompareTo(b.Name) < 0)
        {
            this.a = a;
            this.b = b;
        }
        else
        {
            this.a = b;
            this.b = a;
        }
    }

    /*public override bool Equals(object? obj)
    {
        if (obj is Connection other)
        {
            return (a == other.a && b == other.b) || (a == other.b && b == other.a);
        }
        
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(a, b);
    }*/
}