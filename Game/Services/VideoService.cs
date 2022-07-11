using System;
using Raylib_cs;

namespace Cellular_Automata.Services
{
    public class VideoService
    {
        private int framerate;
        public int width { get; }
        public int height { get; }
        private string title;
        public Color BGColor { get; set; }
        public VideoService(int width,int height,Color BGColor,string title,int framerate = 30)
        {
            this.width = width;
            this.height = height;
            this.BGColor = BGColor;
            this.title = title;
            this.framerate = framerate;
        }
        public void OpenWindow()
        {
            Raylib.InitWindow(width, height, title);
            Raylib.SetTargetFPS(framerate);
        }
        public void CloseWindow()
        {
            Raylib.CloseWindow();
        }
        public void ClearBuffer()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(BGColor);
        }
        public void FlushBuffer()
        {
            Raylib.EndDrawing();
        }
    }
}
