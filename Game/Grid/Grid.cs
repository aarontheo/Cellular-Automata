using Raylib_cs;
using Cellular_Automata.Game.Grid.Elements;
using System;

namespace Cellular_Automata.Game.Grid
{
    public enum DrawMode{
        PIXELS, CIRCLES, NULL
    }
    /// <summary>
    /// A Grid is a collection of objects stored in a 2d array 
    /// </summary>
    public class Grid
    {
        public System.Numerics.Vector2 zeropoint = new System.Numerics.Vector2(0, 0);
        public int width { get; }
        public int height { get; }
        public Element[,] cells;
        //public List<Element> celllist = new List<Element>();
        public int cellSize { get; }
        protected List<Move> changes = new List<Move>();
        private List<Move> options = new List<Move>();
        private static Random rng = new Random(8);
        private DrawMode drawMode = DrawMode.PIXELS;
        public bool wraparound;
        public Grid(int width, int height, int cellSize, bool wraparound = false)
        {
            this.wraparound = wraparound;
            this.cellSize = cellSize;
            this.width = width / cellSize;
            this.height = height / cellSize + 1;
            cells = new Element[this.width, this.height];
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
                        if (y >= height - 1 & !wraparound)
                        {
                            RemoveCell(x, y);
                        }
                        else
                        {
                            cells[x, y].Update(this, x, y);
                        }
                    }
                }
            }
            CommitCells();
        }
        public void Draw()
        {
            //Raylib.BeginTextureMode(canvas);
            //Raylib.ClearBackground(Color.BLANK);
            //Raylib.DrawRectangle(40, 50, 40, 40, Color.BLUE);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var cell = cells[x, y];
                    if (cell != null)
                    {
                        switch (drawMode)
                        {
                            case DrawMode.PIXELS:
                                Raylib.DrawRectangle(x * cellSize, y * cellSize, cellSize, cellSize, cell.color);
                                break;
                            case DrawMode.CIRCLES:
                                Raylib.DrawCircle(x * cellSize, y * cellSize, cellSize + 1, cell.color);
                                break;

                        };
                    }
                }
            }
            //Raylib.EndTextureMode();
            //Raylib.DrawTextureEx(canvas.texture, new System.Numerics.Vector2(0, 0), 0, 1, Color.GREEN);
        }
        public void SwitchMode()
        {
            drawMode++;
            if (drawMode == DrawMode.NULL)
            {
                drawMode = DrawMode.PIXELS;
            }
        } //there's a lot of functions here lol
        public bool isEmpty(int x, int y)
        {
            return cells[WrapInt(x, width), WrapInt(y, height)] == null;
        }
        public bool isEmpty(Point pos)
        {
            return isEmpty(pos.x, pos.y);
        }
        public bool isSolid(int x, int y)
        {
            return getCell(x, y) is Solid;
        }
        public Element getCell(int x, int y)
        {
            return cells[WrapInt(x, width), WrapInt(y, height)];
        }
        public Element getCell(Point pos)
        {
            return getCell(pos.x, pos.y);
        }
        public void setCell(int x, int y, Element cell)
        {
            cells[WrapInt(x, width), WrapInt(y, height)] = cell;
        }
        public void setCell(Point pos, Element cell)
        {
            setCell(pos.x, pos.y, cell);
        }
        public void AddCell(int x, int y, Element cell)
        {
            if (isEmpty(x, y))
            {
                setCell(x, y, cell);
            }
            if (cell == null)
            {
                RemoveCell(x, y);
            }
        }
        public void AddCell(Point pos, Element cell)
        {
            AddCell(pos.x, pos.y, cell);
        }
        public void RemoveCell(int x, int y)
        {
            setCell(x, y, null);
        }
        public void RemoveCell(Point pos)
        {
            RemoveCell(pos.x, pos.y);
        }
        public void SwapCells(int x1, int y1, int x2, int y2)
        {
            var temp = getCell(x1, y1);
            setCell(x1, y1, getCell(x2, y2));
            setCell(x2, y2, temp);
        }
        public void SwapCells(Point pos1, Point pos2)
        {
            SwapCells(pos1.x, pos1.y, pos2.x, pos2.y);
        }
        public void AddCells(Point pos, int radius, Element cell)
        {
            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    AddCell(pos.x - radius/2 + x, pos.y - radius/2 + y, cell);
                }
            }
        }
        public void AddCells(Point pos, int radius, Element type, bool isSpecial)
        {
            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    //AddCell(pos.x - radius/2 + x, pos.y - radius/2 + y, Activator.CreateInstance(null,type.GetType()));
                }
            }
        }
        public void RemoveCells(Point pos, int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    RemoveCell(pos.x - x / 2, pos.y - y / 2);
                }
            }
        }
        public void MakeMove(int x, int y, int toX, int toY)
        {
            changes.Add(new Move(x, y, toX, toY));
        }
        private void CommitCells()
        {
            if (changes.Count == 0)
            {
                return;
            }
            changes = changes.OrderBy(o => (o.to.x * width + o.to.y)).ToList<Move>();
            for (int i = 0; i < changes.Count; i++)
            {
                Move move = changes[i];
                options.Add(move);
                if (i == changes.Count - 1 || changes[i + 1].to != move.to) //if the move has a different destination from the next
                { //maybe you could turn options into a list of lists, and deal with all option sets at once?
                    move = options[rng.Next(options.Count)];
                    // setCell(move.to, getCell(move.from));
                    // RemoveCell(move.from);
                    SwapCells(move.to, move.from);
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
    public class Move
    {
        public Point from { get; }
        public Point to { get; }

        public Move(Point from, Point to)
        {
            this.from = from;
            this.to = to;
        }
        public Move(int x, int y, int xto, int yto)
        {
            this.from = new Point(x, y);
            this.to = new Point(xto, yto);
        }
    }
}