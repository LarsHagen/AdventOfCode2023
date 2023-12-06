using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    internal class Helper
    {
        //Is holding button for "time" better than the record?
        public static bool IsBetter(long time, long totalTime, long record)
        {
            var speed = time;
            var remaning = totalTime - time;

            var distance = remaning * speed;

            bool better = distance > record;
            return better;
        }
    }
}
