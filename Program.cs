using Cellular_Automata.Game.Directing;
using Cellular_Automata.Services;
using Cellular_Automata.Game.Grid;
using Raylib_cs;

int width = 800;
int height = 600;
int cellSize = 2;
int FPS = 60;
Color bgColor = Color.BLACK;
string title = "Sand";
VideoService videoService = new VideoService(width, height, bgColor, title, FPS);
Director director = new Director(videoService);
Grid grid = new Grid(width, height, cellSize);
//for (int i = 0; i < 30;i++)
// {
//     grid.AddCell(30+i, 80,new Wall());
// }
director.StartGame(grid);
// Point a = new Point(0, 0);
// Point b = new Point(0, 0);
// Console.WriteLine(a >= b);