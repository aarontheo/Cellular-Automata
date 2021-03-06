using System;
using System.Numerics;

namespace Cellular_Automata.Game.Grid
{
    /// <summary>
    /// A Point is essentially a Vect that deals in ints rather than floats.
    /// </summary>
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Point(Vector2 vect)
        {
            this.x = (int)vect.X;
            this.y = (int)vect.Y;
        }
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }
        public static Point operator -(Point a,Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }
        public static Point operator *(Point a, int b)
        {
            return new Point(a.x * b, a.y * b);
        }
        public static bool operator ==(Point a, Point b)
        {
            if (a.x == b.x & a.y == b.y)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
        public static bool operator <(Point a, Point b)
        {
            return a.x < b.x & a.y <= b.y;
        }
        public static bool operator <=(Point a, Point b)
        {
            return a < b || a == b;
        }
        public static bool operator >(Point a, Point b)
        {
            return a.x > b.x & a.y >= b.y;
        }
        public static bool operator >=(Point a, Point b)
        {
            return a > b || a == b;
        }
    }
}
