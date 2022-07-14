using Raylib_cs;

namespace Cellular_Automata.Game.Grid.Elements
{
    public abstract class Element
{
    protected static Random rng = new Random(8);
        public Color color;
        public abstract void Update(Grid grid, int x, int y);
    public virtual void Draw(Grid grid, int x, int y)
    {
        int size = grid.cellSize;
        Raylib.DrawRectangle(x * size, y * size, size, size, color);
    }
}
}
