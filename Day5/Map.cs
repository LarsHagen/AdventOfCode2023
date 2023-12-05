
namespace Day5
{
    internal class Map
    {
        public List<Mapping> Mappings;

        public long GetDestination(long source)
        {
            var mapping = Mappings.FirstOrDefault(v => source >= v.SourceRangeStart && source < v.SourceRangeStart + v.RangeLength);

            if (mapping == null)
                return source;

            return mapping.GetDestination(source);
        }


        public Map(List<string> lines)
        {
            Mappings = new();

            foreach (var line in lines)
            {
                var split = line.Split(' ');
                Mappings.Add(new Mapping()
                {
                    DestinationRangeStart = long.Parse(split[0]),
                    SourceRangeStart = long.Parse(split[1]),
                    RangeLength = long.Parse(split[2]),
                });
            }
        }
    }

    internal class Mapping
    {
        public long DestinationRangeStart;
        public long SourceRangeStart;
        public long RangeLength;

        public long GetDestination(long source)
        {
            var diff = source - SourceRangeStart;
            return DestinationRangeStart + diff;
        }

        public bool ContainSource(long source)
        {
            if (source < SourceRangeStart)
                return false;

            if (source > SourceRangeStart + RangeLength - 1)
                return false;

            return true;
        }
    }
}
