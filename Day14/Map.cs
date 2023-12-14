namespace Day14;

public class Map
{
    public int MapSizeX;
    public int MapSizeY;
    public char[,] MapData;

    public Map(string filename)
    {
        var lines = File.ReadAllLines(filename);
        
        MapSizeY = lines.Length;
        MapSizeX = lines[0].Length;

        MapData = new char[MapSizeX, MapSizeY];
        
        for (var y = 0; y < MapSizeY; y++)
        {
            var line = lines[y];

            for (int x = 0; x < MapSizeX; x++)
            {
                MapData[x, y] = line[x];
            }
        }
    }

    public void TiltNorth()
    {
        for (var y = 1; y < MapSizeY; y++)
        {
            for (int x = 0; x < MapSizeX; x++)
            {
                if (MapData[x, y] != 'O') 
                    continue;
                
                int rollToY = y - 1;
                
                for (; rollToY >= 0; rollToY--)
                {
                    if (MapData[x, rollToY] != '.')
                        break;
                }

                rollToY++;
                MapData[x, y] = '.';
                MapData[x, rollToY] = 'O';
            }
        }
    }
    
    public void TiltSouth()
    {
        for (var y = MapSizeY - 2; y >= 0; y--)
        {
            for (int x = 0; x < MapSizeX; x++)
            {
                if (MapData[x, y] != 'O') 
                    continue;
                
                int rollToY = y + 1;
                
                for (;rollToY < MapSizeY; rollToY++)
                {
                    if (MapData[x, rollToY] != '.')
                        break;
                }

                rollToY--;
                MapData[x, y] = '.';
                MapData[x, rollToY] = 'O';
            }
        }
    }
    
    public void TiltEast()
    {
        for (var x = MapSizeX - 2; x >= 0; x--)
        {
            for (int y = 0; y < MapSizeY; y++)
            {
                if (MapData[x, y] != 'O') 
                    continue;
                
                int rollToX = x + 1;
                
                for (;rollToX < MapSizeX; rollToX++)
                {
                    if (MapData[rollToX, y] != '.')
                        break;
                }

                rollToX--;
                MapData[x, y] = '.';
                MapData[rollToX, y] = 'O';
            }
        }
    }
    
    public void TiltWest()
    {
        for (var x = 1; x < MapSizeX; x++)
        {
            for (int y = 0; y < MapSizeY; y++)
            {
                if (MapData[x, y] != 'O') 
                    continue;
                
                int rollToX = x - 1;
                
                for (; rollToX >= 0; rollToX--)
                {
                    if (MapData[rollToX, y] != '.')
                        break;
                }

                rollToX++;
                MapData[x, y] = '.';
                MapData[rollToX, y] = 'O';
            }
        }
    }

    public int NorthBeamLoad()
    {
        return NorthBeamLoad(MapData, MapSizeX, MapSizeY);
    }

    public static int NorthBeamLoad(char[,] map, int sizeX, int sizeY)
    {
        int load = 0;
        for (var y = 0; y < sizeY; y++)
        {
            int dist = sizeY - y;
            for (int x = 0; x < sizeX; x++)
            {
                if (map[x, y] != 'O') 
                    continue;
                
                load += dist;
            }
        }

        return load;
    }
}