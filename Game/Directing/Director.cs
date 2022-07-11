using System;
using Raylib_cs;
using Cellular_Automata.Services;

using Cellular_Automata.Game.Grid;
namespace Cellular_Automata.Directing
{
    public class Director
    {
        private VideoService videoService;
        private bool isRunning = false;
        public Director(VideoService videoService)
        {
            this.videoService = videoService;
        }
        public void StartGame(Grid grid)
        {
            isRunning = true;
            videoService.OpenWindow();
            while (!Raylib.WindowShouldClose())
            {
                if (isRunning)
                {
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