using Raylib_cs;

namespace Cellular_Automata.Game.Grid
{
    /// <summary>
    /// A Grid is a collection of objects stored in a 2d array 
    /// </summary>
    public class Grid
    {
        public int width { get; }
        public int height { get; }
        private Element[,] current;
        private Element[,] buffer;
        public int cellSize { get; }
        public Grid(int width, int height, int cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            current = new Element[width, height];
            buffer = new Element[width, height];
        }
        public void Update()
        {
            for (int x = 0;x < width;x++)
            {
                for (int y = 0; y < height;y++)
                {
                    var cell = current[x, y];
                    if (cell != null)
                    {
                        cell.Update(this,x,y);
                    }
                }
            }
            current = buffer;
        }
        public void Draw()
        {
            for (int x = 0;x < width;x++)
            {
                for (int y = 0; y < height;y++)
                {
                    var cell = buffer[x, y];
                    if (cell != null)
                    {
                        cell.Draw(this,x,y);
                    }
                }
            }
        }
        private int WrapInt(int a,int max)
        {
            return ((a % max) + max) % max;
        }
    }
}