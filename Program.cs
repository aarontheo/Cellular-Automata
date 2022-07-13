using Cellular_Automata.Directing;
using Cellular_Automata.Services;
using Cellular_Automata.Game.Grid;
using Cellular_Automata.Game.Grid.Elements;
using Raylib_cs;

int width = 400;
int height = 400;
int cellSize = 10;
int FPS = 20;
Color bgColor = Color.BLACK;
string title = "Sand";

VideoService videoService = new VideoService(width, height, bgColor, title, FPS);
Director director = new Director(videoService);
Grid grid = new Grid(height, width, cellSize);
grid.cells[10, 10] = new Sand();
director.StartGame(grid);
// Point a = new Point(0, 0);
// Point b = new Point(0, 0);
// Console.WriteLine(a >= b);