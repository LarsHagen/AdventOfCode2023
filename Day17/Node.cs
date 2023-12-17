using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    internal class Node
    {
        public Node Previous;
        public int HeatLoss;
        public int StepsLineX;
        public int StepsLineY;
        public int X;
        public int Y;

        public List<(int x, int y)> GetValidMoves()
        {
            List<(int x, int y)> result = new();

            if (StepsLineX == 0)
            {
                result.Add((1, 0));
                result.Add((-1, 0));
            }
            else if (StepsLineX > 0 && StepsLineX < 3)
            {
                result.Add((1, 0));
            }
            else if (StepsLineX < 0 && StepsLineX > -3)
            {
                result.Add((-1, 0));
            }

            if (StepsLineY == 0)
            {
                result.Add((0, 1));
                result.Add((0, -1));
            }
            else if (StepsLineY > 0 && StepsLineY < 3)
            {
                result.Add((0, 1));
            }
            else if (StepsLineY < 0 && StepsLineY > -3)
            {
                result.Add((0, -1));
            }

            return result;
        }

        public List<(int x, int y)> GetValidMovesUltraCrucible()
        {
            List<(int x, int y)> result = new();

            if (StepsLineX == 0 && StepsLineY == 0)
            {
                result.Add((1, 0));
                result.Add((-1, 0));
                result.Add((0, 1));
                result.Add((0, -1));

                return result;
            }


            if (StepsLineX > 0)
            {
                if (StepsLineX >= 4)
                {
                    result.Add((0, 1));
                    result.Add((0, -1));
                }

                if (StepsLineX < 10)
                {
                    result.Add((1, 0));
                }

                return result;
            }

            if (StepsLineX < 0)
            {
                if (StepsLineX <= -4)
                {
                    result.Add((0, 1));
                    result.Add((0, -1));
                }

                if (StepsLineX > -10)
                {
                    result.Add((-1, 0));
                }

                return result;
            }

            if (StepsLineY > 0)
            {
                if (StepsLineY >= 4)
                {
                    result.Add((1, 0));
                    result.Add((-1, 0));
                }

                if (StepsLineY < 10)
                {
                    result.Add((0, 1));
                }

                return result;
            }

            if (StepsLineY < 0)
            {
                if (StepsLineY <= -4)
                {
                    result.Add((1, 0));
                    result.Add((-1, 0));
                }

                if (StepsLineY > -10)
                {
                    result.Add((0, -1));
                }

                return result;
            }


            return null;
        }
    }
}
