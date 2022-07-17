using System;

namespace Cellular_Automata.Game.Grid.Elements
{
    public abstract class DynamicSolid : Solid
    {
        public override void Update(Grid grid, int x, int y)
        {
            //try to move down, then to a random direction to the right or left if that is obstructed
            if (!grid.isSolid(x, y + 1))
            {
                grid.MakeMove(x, y, x, y + 1);
                // grid.setCell(x, y + 1, this);
                // grid.setCell(x, y, null);
            }
            else
            {
                if (rng.Next(2) == 0 & !grid.isSolid(x - 1, y + 1) & !grid.isSolid(x - 1, y))
                {
                    grid.MakeMove(x, y, x - 1, y + 1);
                    // grid.setCell(x - 1, y + 1, this);
                    // grid.setCell(x, y, null);
                }
                else if (!grid.isSolid(x + 1, y + 1) & !grid.isSolid(x + 1, y))
                {
                    grid.MakeMove(x, y, x + 1, y + 1);
                    // grid.setCell(x + 1, y + 1, this);
                    // grid.setCell(x, y, null);
                }
            }
        }
    }
}
