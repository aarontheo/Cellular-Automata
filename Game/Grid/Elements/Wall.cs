using System;
using Raylib_cs;
namespace Cellular_Automata.Game.Grid.Elements
{
    public class Wall : StaticSolid
    {
        public Wall()
        {
            this.color = Color.GRAY;
            
        }
    }
}
