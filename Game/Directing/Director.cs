using System;
using Raylib_cs;
using Cellular_Automata.Services;
using Cellular_Automata.Game.Grid.Elements;
using System.ComponentModel;

namespace Cellular_Automata.Game.Directing
{
    public class Director
    {
        private VideoService videoService;
        private InputService inputService;
        private bool isRunning = false;
        public int CursorSize = 1;
        public int maxCursorSize;
        private Color cursorColor = new Color(150,150,150,100);
        public int selectedElement;
        public Element[] elementTypes = { new Sand(), new Water(), new Wall(), null };
        public string[] elementNames = { "SAND", "WATER", "WALL", "ERASE"};
        public Director(VideoService videoService)
        {
            this.videoService = videoService;
            this.inputService = new InputService();
            this.maxCursorSize = videoService.width / 8;
        }
        public void StartGame(Grid.Grid grid)
        {
            videoService.OpenWindow();
            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
                {
                    grid.AddCells(inputService.GetMousePos(grid), CursorSize, elementTypes[selectedElement]);
                }
                if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT))
                {
                    grid.RemoveCells(inputService.GetMousePos(grid),CursorSize);
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    isRunning = !isRunning;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_W))
                {
                    grid.wraparound = !grid.wraparound;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT_BRACKET) & selectedElement > 0){
                    selectedElement--;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT_BRACKET) & selectedElement < elementTypes.Length-1){
                    selectedElement++;
                }
                //Console.WriteLine(inputService.GetMouseWheelMove());
                CursorSize = Math.Max(1,CursorSize + inputService.GetMouseWheelMove()/2);
                CursorSize = Math.Min(maxCursorSize, CursorSize);
                //grid.AddCell(40, 40, new Sand());
                if (isRunning)
                {
                    grid.Update();
                }
                videoService.ClearBuffer();
                grid.Draw();
                Raylib.DrawRectangleLines(Raylib.GetMouseX() - (CursorSize*grid.cellSize)/2, Raylib.GetMouseY() - (CursorSize*grid.cellSize)/2,CursorSize*grid.cellSize,CursorSize*grid.cellSize,cursorColor);
                Raylib.DrawText($"Cursor Size: {CursorSize}", 10, 10, 10, Color.WHITE);
                Raylib.DrawText($"Selected Element: {elementNames[selectedElement]}", 10, 30, 10, Color.WHITE);
                Raylib.DrawText($"Edge Wrapping: {grid.wraparound}", 10, 40, 10, Color.WHITE);
                if (!isRunning)
                {
                    Raylib.DrawText("PAUSED", videoService.width/2-30, 10, 10, Color.WHITE);
                }

                videoService.FlushBuffer();
            }
            videoService.CloseWindow();
        }
    }
}