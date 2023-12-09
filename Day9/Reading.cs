
namespace Day9
{
    internal class Reading
    {
        public List<List<int>> sequences = new();

        public Reading(string input)
        {
            var initialSequence = input.Split(" ").Select(v => int.Parse(v)).ToList();
            sequences.Add(initialSequence);

            GenerateLowerRecursive(initialSequence);
        }

        private void GenerateLowerRecursive(List<int> current)
        {
            bool done = true;

            List<int> lower = new List<int>();

            for (int i = 0; i < current.Count - 1; i++)
            {
                var difference = current[i + 1] - current[i];

                if (difference != 0)
                    done = false;

                lower.Add(difference);
            }

            sequences.Add(lower);

            if (!done)
                GenerateLowerRecursive(lower);
        }

        public int Extrapolate()
        {
            sequences.Last().Add(0);

            for (int i = sequences.Count - 2; i >= 0; i--)
            {
                sequences[i].Add(sequences[i].Last() + sequences[i + 1].Last());
            }

            return sequences[0].Last();
        }

        public int ExtrapolateBack()
        {
            sequences.Last().Insert(0, 0);


            for (int i = sequences.Count - 2; i >= 0; i--)
            {
                sequences[i].Insert(0, sequences[i].First() - sequences[i + 1].First());
            }

            return sequences[0].First();
        }
    }
}
