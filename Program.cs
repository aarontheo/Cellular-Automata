using Cellular_Automata.Directing;
using Cellular_Automata.Services;
using Cellular_Automata.Game.Grid;
using Raylib_cs;

int width = 400;
int height = 400;
int cellSize = 10;
int FPS = 1;
Color bgColor = Color.BLACK;
string title = "Sand";

VideoService videoService = new VideoService(width, height, bgColor, title, FPS);
Director director = new Director(videoService);
Grid grid = new Grid(height / cellSize, width / cellSize, cellSize);
director.StartGame(grid);