using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts.Model
{
    class Coordinate
    {
        public Coordinate(int fromX, int fromY, int toX, int toY)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
        }

        public int fromX { get; set; }
        public int fromY { get; set; }
        public int toX { get; set; }
        public int toY { get; set; }
    }
}
