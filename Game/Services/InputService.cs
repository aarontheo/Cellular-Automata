using System;
using Raylib_cs;
using Cellular_Automata.Game.Grid;
using Cellular_Automata.Game.Grid.Elements;

namespace Cellular_Automata.Services
{
    public class InputService
    {
        //the keys array has the 4 directional keys, in order WASD, or up/left/down/right
        private KeyboardKey[] keys = new KeyboardKey[4];
        public InputService(KeyboardKey left,KeyboardKey right,KeyboardKey up,KeyboardKey down)
        {
            keys[0] = up;
            keys[1] = left;
            keys[2] = down;
            keys[3] = right;
        }
        public InputService()
        {
            keys = null;
        }
        public Point GetMousePos(Grid grid)
        {
            return new Point(Raylib.GetMouseX()/grid.cellSize,Raylib.GetMouseY()/grid.cellSize);
        }
        public Point GetMouseDelta(Grid grid)
        {

            return new Point(Raylib.GetMouseDelta()) * grid.cellSize;
        }
        public int GetMouseWheelMove()
        {
            return (int)Raylib.GetMouseWheelMove();
        }
        // public sbyte GetMouseDirection()
        // {
        //     return ((sbyte)GetMouseWheelMove());
        // }
        public Point GetDirection()
        {
            int dx = 0;
            int dy = 0;
            if (Raylib.IsKeyDown(keys[1]))
            {
                dx -= 1;
            }
            if (Raylib.IsKeyDown(keys[3]))
            {
                dx += 1;
            }
            //re-enable this to allow up and down motion
            if (Raylib.IsKeyDown(keys[0]))
            {
                dy -= 1;
            }
            if (Raylib.IsKeyDown(keys[2]))
            {
                dy += 1;
            }
            return new Point(dx, dy);
        }
        /// <summary>
        /// Get Direction Exclusive. 
        /// Returns a point representing the direction intended, 
        /// but can only point in a cardinal direction.
        /// </summary>
        /// <returns></returns>
        public Point GetDirectionEx()
        {
            int dx = 0;
            int dy = 0;
            if (Raylib.IsKeyDown(keys[1]))
            {
                dx -= 1;
                dy = 0;
            }
            if (Raylib.IsKeyDown(keys[3]))
            {
                dx += 1;
                dy = 0;
            }
            //re-enable this to allow up and down motion
            if (Raylib.IsKeyDown(keys[0]))
            {
                dy -= 1;
                dx = 0;
            }
            if (Raylib.IsKeyDown(keys[2]))
            {
                dy += 1;
                dx = 0;
            }
            return new Point(dx, dy);
        }
    }
}
