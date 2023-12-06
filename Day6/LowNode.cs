namespace Day6
{
    internal class LowNode
    {
        public bool Better;
        public long Index;
        public long StepSize;

        public LowNode Left;
        public LowNode Right;

        public static long bestFit = long.MaxValue;

        public LowNode(long index, long stepSize, long totalTime, long record)
        {
            Better = Helper.IsBetter(index, totalTime, record);
            Index = index;
            StepSize = stepSize;

            if (Better && index < bestFit)
                bestFit = index;

            if (stepSize > 1 && !Better)
            {
                var childStepSize = stepSize / 2;
                Left = new LowNode(index, childStepSize, totalTime, record);
                Right = new LowNode(index + childStepSize, childStepSize, totalTime, record);
            }
        }
    }
}
