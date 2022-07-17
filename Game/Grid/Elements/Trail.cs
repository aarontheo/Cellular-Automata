using System;
using Raylib_cs;

namespace Cellular_Automata.Game.Grid.Elements
{
    public class Trail : DynamicSolid, ISpecial
    {
        public byte life { get; set; }
        public Trail(Color color)
        {
            this.color = color;
        }
        public Trail()
        {
            this.color = Color.SKYBLUE;
        }
        public override void Update(Grid grid, int x,int y)
        {
            if (life == 0)
            {
                grid.RemoveCell(x, y);
            }
            color.a = ((byte)(life + 50));
            life--;
        }
    }
}
