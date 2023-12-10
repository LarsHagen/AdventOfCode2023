using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    internal class Node
    {
        public List<Node> Connections = new();

        public int X;
        public int Y;
        public char C;
        public bool Connected = false;
    }
}
