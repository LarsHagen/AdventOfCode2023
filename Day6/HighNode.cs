namespace Day6
{
    internal class HighNode
    {
        public bool Better;
        public long Index;
        public long StepSize;

        public HighNode Left;
        public HighNode Right;

        public static long bestFit = long.MinValue;

        public HighNode(long index, long stepSize, long totalTime, long record)
        {
            Better = Helper.IsBetter(index, totalTime, record);
            Index = index;
            StepSize = stepSize;

            if (Better && index > bestFit)
                bestFit = index;

            if (stepSize > 1 && !Better)
            {
                var childStepSize = stepSize / 2;
                Left = new HighNode(index - childStepSize, childStepSize, totalTime, record);
                Right = new HighNode(index, childStepSize, totalTime, record);
            }
        }
    }
}
