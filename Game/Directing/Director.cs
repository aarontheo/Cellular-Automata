using System;
using Raylib_cs;
using Cellular_Automata.Services;
using Cellular_Automata.Game.Grid.Elements;

using Cellular_Automata.Game.Grid;
namespace Cellular_Automata.Directing
{
    public class Director
    {
        private VideoService videoService;
        private InputService inputService;
        private bool isRunning = false;
        public Director(VideoService videoService)
        {
            this.videoService = videoService;
            this.inputService = new InputService();
        }
        public void StartGame(Grid grid)
        {
            isRunning = true;
            videoService.OpenWindow();
            while (!Raylib.WindowShouldClose())
            {
                if (isRunning)
                {
                    if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
                    {
                        inputService.AddSandAtCursor(grid, new Sand());
                    }
                    grid.Update();
                    videoService.ClearBuffer();
                    grid.Draw();
                    videoService.FlushBuffer();
                }
            }
            videoService.CloseWindow();
        }
    }
}