namespace Day13;

public class Map
{
    public int MirrorIndexClear;
    public bool HorizontalClear;
    
    public int MirrorIndexSmudge;
    public bool HorizontalSmudge;

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

        var horizontalMirror = FindHorizontalMirrorClear(0);
        if (horizontalMirror != -1)
        {
            MirrorIndexClear = horizontalMirror;
            HorizontalClear = true;
        }
        
        var verticalMirror = FindVerticalMirrorClear(0);
        if (verticalMirror != -1)
        {
            MirrorIndexClear = verticalMirror;
            HorizontalClear = false;
        }
        
        var horizontalMirrorSmudge = FindHorizontalMirrorClear(1);
        if (horizontalMirrorSmudge != -1)
        {
            MirrorIndexSmudge = horizontalMirrorSmudge;
            HorizontalSmudge = true;
        }
        
        var verticalMirrorSmudge = FindVerticalMirrorClear(1);
        if (verticalMirrorSmudge != -1)
        {
            MirrorIndexSmudge = verticalMirrorSmudge;
            HorizontalSmudge = false;
        }
    }

    private int FindVerticalMirrorClear(int exactDifference)
    {
        for (int x = 0; x < MapSizeX - 1; x++)
        {
            if (IsVerticalMirror(x) == exactDifference)
                return x + 1; //Remember +1 for one-indexed rows and columns
        }

        return -1;
    }

    private int FindHorizontalMirrorClear(int exactDifference)
    {
        for (int y = 0; y < MapSizeY - 1; y++)
        {
            if (IsHorizontalMirror(y) == exactDifference)
                return y + 1;
        }

        return -1;
    }
    
    private int IsVerticalMirror(int x)
    {
        int difference = 0;
        int offset = 0;
        
        while (true)
        {
            int minX = x - offset;
            int maxX = x + offset + 1;

            if (minX < 0 || maxX >= MapSizeX)
                return difference; 
                
            for (int y = 0; y < MapSizeY; y++)
            {
                var left = MapData[minX, y];
                var right = MapData[maxX, y];

                if (left != right)
                    difference++;
            }
            
            offset++;
        }
    }
    
    private int IsHorizontalMirror(int y)
    {
        int difference = 0;
        int offset = 0;
        
        while (true)
        {
            int minY = y - offset;
            int maxY = y + offset + 1;

            if (minY < 0 || maxY >= MapSizeY)
                return difference;
                
            for (int x = 0; x < MapSizeX; x++)
            {
                var up = MapData[x, minY];
                var down = MapData[x, maxY];

                if (up != down)
                    difference++;
            }

            offset++;
        }
    }
}