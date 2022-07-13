using System;

namespace Cellular_Automata.Game.Grid
{
    public class Move
    {
        public Point from { get; }
        public Point to { get; }
        
        public Move(Point from, Point to)
        {
            this.from = from;
            this.to = to;
        }
        public Move(int x, int y, int xto, int yto)
        {
            this.from = new Point(x, y);
            this.to = new Point(xto, yto);
        }
        public static bool operator <(Move a, Move b)
        {
            return a.to < b.to;
        }
        public static bool operator >(Move a, Move b)
        {
            return a.to < b.to;
        }
    }
}
