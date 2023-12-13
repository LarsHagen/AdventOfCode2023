namespace Day13;

public class Map
{
    public int MirrorIndex;
    public bool Horizontal;

    public int MapSizeX;
    public int MapSizeY;
    
    public bool[,] MapData;
    
    public Map(List<string> input)
    {
        MapSizeX = input[0].Length;
        MapSizeY = input.Count;
        
        MapData = new bool[MapSizeX,MapSizeY];
        
        for (int y = 0; y < MapSizeY; y++)
        {
            for (int x = 0; x < MapSizeX; x++)
            {
                MapData[x,y] = input[y][x] == '#';
            }
        }

        var horizontalMirror = FindHorizontalMirror();
        if (horizontalMirror != -1)
        {
            MirrorIndex = horizontalMirror;
            Horizontal = true;
            return;
        }
        
        var verticalMirror = FindVerticalMirror();
        if (verticalMirror != -1)
        {
            MirrorIndex = verticalMirror;
            Horizontal = false;
            return;
        }

        throw new ArgumentOutOfRangeException("No mirror found");
    }

    private int FindVerticalMirror()
    {
        for (int x = 0; x < MapSizeX - 1; x++)
        {
            if (IsVerticalMirror(x))
                return x + 1; //Remember +1 for one-indexed rows and columns
        }

        return -1;
    }

    private int FindHorizontalMirror()
    {
        for (int y = 0; y < MapSizeY - 1; y++)
        {
            if (IsHorizontalMirror(y))
                return y + 1;
        }

        return -1;
    }
    
    private bool IsVerticalMirror(int x)
    {
        int offset = 0;
        while (true)
        {
            int minX = x - offset;
            int maxX = x + offset + 1;

            if (minX < 0 || maxX >= MapSizeX)
                return true; //No issues found
                
            for (int y = 0; y < MapSizeY; y++)
            {
                var left = MapData[minX, y];
                var right = MapData[maxX, y];

                if (left != right)
                    return false;
            }
            
            offset++;
        }
    }
    
    private bool IsHorizontalMirror(int y)
    {
        int offset = 0;
        while (true)
        {
            int minY = y - offset;
            int maxY = y + offset + 1;

            if (minY < 0 || maxY >= MapSizeY)
                return true; //No issues found
                
            for (int x = 0; x < MapSizeX; x++)
            {
                var up = MapData[x, minY];
                var down = MapData[x, maxY];

                if (up != down)
                    return false;
            }

            offset++;
        }
    }
}