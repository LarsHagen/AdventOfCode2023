namespace Day22;

public class Brick
{
    public List<(int x, int y, int z)> Blocks = new();
    public char Name = '#';
    public HashSet<Brick> SupportedBy = new();
    public HashSet<Brick> Supports = new();
    
    public Brick(string input, Dictionary<(int x, int y, int z), Brick> map)
    {
        var ends = input.Split('~');
        var endA = ends[0].Split(',').Select(i => int.Parse(i)).ToArray();
        var endB = ends[1].Split(',').Select(i => int.Parse(i)).ToArray();
        
        var minX = Math.Min(endA[0], endB[0]);
        var maxX = Math.Max(endA[0], endB[0]);
        var minY = Math.Min(endA[1], endB[1]);
        var maxY = Math.Max(endA[1], endB[1]);
        var minZ = Math.Min(endA[2], endB[2]);
        var maxZ = Math.Max(endA[2], endB[2]);
        
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int z = minZ; z <= maxZ; z++)
                {
                    Blocks.Add((x, y, z));
                }
            }
        }
        
        foreach (var block in Blocks)
        {
            map.Add(block, this);
        }
    }

    public void CalculateSupports(Dictionary<(int x, int y, int z), Brick> map)
    {
        foreach (var block in Blocks)
        {
            (int x, int y, int z) oneLower = (block.x, block.y, block.z - 1);
            if (map.TryGetValue(oneLower, out var brick))
            {
                if (brick == this)
                    continue;
                AddIsSupportedBy(brick);
            }
        }
    }

    public bool Fall(Dictionary<(int x, int y, int z), Brick> map)
    {
        if (Blocks.Any(b => b.z == 1))
            return false;
        
        List<(int x, int y, int z)> newPositions = new();
        foreach (var block in Blocks)
        {
            (int x, int y, int z) newPosition = (block.x, block.y, block.z - 1);
            newPositions.Add(newPosition);
            
            if (map.TryGetValue(newPosition, out var other))
            {
                if (other != this)
                    return false;
            }
        }
        
        foreach (var block in Blocks)
        {
            map.Remove(block);
        }

        Blocks = newPositions;
        foreach (var block in Blocks)
        {
            map.Add(block, this);
        }

        return true;
    }
    
    public void AddIsSupportedBy(Brick brick)
    {
        SupportedBy.Add(brick);
        brick.Supports.Add(this);
    }

    public int CalculateChainReaction()
    {
        HashSet<Brick> falling = new();
        Queue<Brick> toCheck = new();
        toCheck.Enqueue(this);
        falling.Add(this);
        
        while (toCheck.Count > 0)
        {
            var current = toCheck.Dequeue();
            foreach (var brick in current.Supports)
            {
                if (brick.SupportedBy.Any(b => !falling.Contains(b)))
                    continue;

                falling.Add(brick);
                if (!toCheck.Contains(brick))
                    toCheck.Enqueue(brick);
            }
        }

        return falling.Count - 1;
    }
}