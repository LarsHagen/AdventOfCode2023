namespace Day2
{
    public class Game
    {

        public int Id;
        public List<(int red, int green, int blue)> Sets;
        public (int red, int green, int blue) MinimumRequired = new(0, 0, 0);

        public Game(string input)
        {
            string[] split = input.Split(":");
            Id = int.Parse(split[0].Replace("Game","").Trim());

            var sets = split[1].Split(";");

            Sets = new();

            foreach (var set in sets)
            {
                var colors = set.Split(',');

                int red = 0;
                int green = 0;
                int blue = 0;

                foreach(var color in colors)
                {
                    if (color.Contains("red"))
                    {
                        red += int.Parse(color.Replace("red", ""));
                        continue;
                    }

                    if (color.Contains("green"))
                    {
                        green += int.Parse(color.Replace("green", ""));
                        continue;
                    }

                    if (color.Contains("blue"))
                    {
                        blue += int.Parse(color.Replace("blue", ""));
                        continue;
                    }
                }

                MinimumRequired.red = Math.Max(MinimumRequired.red, red);
                MinimumRequired.green = Math.Max(MinimumRequired.green, green);
                MinimumRequired.blue = Math.Max(MinimumRequired.blue, blue);

                Sets.Add((red, green, blue));
            }
        }
    }
}
