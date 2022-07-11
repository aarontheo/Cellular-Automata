using Raylib_cs;

namespace Cellular_Automata.Game.Grid
{
    public abstract class Element
    {
        public Color color = new Color(255, 255, 255, 255);
        public abstract void Update(Grid grid, int x, int y);
        public virtual void Draw(Grid grid,int x,int y)
        {
            int size = grid.cellSize;
            Raylib.DrawRectangle(x * size, y * size, size, size, color);
        }
    }
}
