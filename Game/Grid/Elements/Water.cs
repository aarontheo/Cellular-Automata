using System;
using Raylib_cs;

namespace Cellular_Automata.Game.Grid.Elements
{
    public class Water : Liquid
    {
        public Water()
        {
            this.color = Color.BLUE;
        }
    }
}
