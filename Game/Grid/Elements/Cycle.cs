using System;
using Raylib_cs;

namespace Cellular_Automata.Game.Grid.Elements
{
    public class Cycle : DynamicSolid
    {

        public Cycle(Color color)
        {
            this.color = color;

        }
        public override void Update(Grid grid, int x, int y)
        {

        }
    }
}
