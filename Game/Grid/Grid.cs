using Raylib_cs;
using Cellular_Automata.Game.Grid.Elements;

namespace Cellular_Automata.Game.Grid
{
    /// <summary>
    /// A Grid is a collection of objects stored in a 2d array 
    /// </summary>
    public class Grid
    {
        public System.Numerics.Vector2 zeropoint = new System.Numerics.Vector2(0, 0);
        public int width { get; }
        public int height { get; }
        public Element[,] cells;
        public int cellSize { get; }
        private RenderTexture2D canvas = new RenderTexture2D();
        protected List<Move> changes = new List<Move>();
        private List<Move> options = new List<Move>();
        private static Random rng = new Random();
        public Grid(int width, int height, int cellSize)
        {
            this.width = width;
            this.height = height;
            cells = new Element[width, height];
        }
        public void Update()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //add changes to the list for all cell movement updates,
                    //cell type changes are handled in the individual cell's update functions
                    Element cell = cells[x, y];
                    if (cell != null)
                    {
                        //Console.WriteLine("Updating");
                        cells[x, y].Update(this,x,y);
                    }
                }
            }
            CommitCells();
        }
        public void Draw()
        {
            //Raylib.BeginTextureMode(canvas);
            //Raylib.ClearBackground(Color.BLANK);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var cell = cells[x, y];
                    if (cell != null)
                    {
                        Raylib.DrawPixel(x, y, cell.color);
                        //Raylib.DrawRectangle(x, y, 10, 10, cell.color);
                    }
                }
            }
            //Raylib.EndTextureMode();
            //Raylib.DrawTextureEx(canvas.texture, new System.Numerics.Vector2(0, 0), 0, 1, Color.GREEN);
        }
        public bool isEmpty(int x, int y)
        {
            return cells[WrapInt(x, width), WrapInt(y, height)] == null;
        }
        public Element getCell(int x,int y)
        {
            return cells[WrapInt(x,width),WrapInt(y,height)];
        }
        public void setCell(int x, int y, Element cell)
        {
            cells[WrapInt(x, width), WrapInt(y, height)] = cell;
        }
        public void AddCell(int x, int y, Element cell)
        {
            if (isEmpty(x,y))
            {
                setCell(x, y, cell);
            }
        }
        public void MakeMove(int x, int y,int toX, int toY)
        {
            changes.Add(new Move(x, y, toX, toY));
        }
        private void CommitCells()
        {
            foreach (Move move in changes) //remove any moves trying to write to a filled position
            {
                if (cells[move.to.x, move.to.y] != null)
                {
                    changes.Remove(move);
                }
            }
            changes.OrderBy(o=>(o.to.x*width+o.to.y));
            Console.WriteLine("Moves:"+changes.Count);
            foreach(Move m in changes)
            {
                Console.WriteLine(m.to.x);
            }
            //pick the changes to keep
            //options is an array to keep track of
            //Console.WriteLine(options.Count);
            for (int i = 0; i < changes.Count; i++)
            {
                if (i == 0 || changes[i - 1].to == changes[i].to)
                {
                    //add the current Moves that have the same destination to the options list
                    options.Add(changes[i]);
                    //Console.WriteLine("options has: "+options.Count);
                } else { //pick from all the moves in the current option
                    Move move = options[rng.Next(options.Count)];
                    Console.WriteLine($"options contains {options.Count} elements.");
                    //Move move = options[0];
                    cells[move.to.x, move.to.y] = cells[move.from.x, move.from.y];
                    cells[move.from.x, move.to.y] = null;
                    options.Clear();
                }
            }
            changes.Clear();
        }
        private int WrapInt(int a, int max)
        {
            return ((a % max) + max) % max;
        }
    }
}