using System.Collections.Generic;
using DrawTool.Model;
using DrawTool.Support;

namespace DrawTool.Renderers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICanvasRenderer" />
    public class CanvasRenderer : ICanvasRenderer
    {
        public Canvas Canvas { get; set; }
        public IOutputWriter OutputWriter { get; set; }

        protected CanvasRenderer()
        {

        }

        public CanvasRenderer(Canvas canvas, IOutputWriter outputWriter)
        {
            OutputWriter = outputWriter;
            Canvas = canvas;
        }

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            char[,] canvas = Canvas.GetCanvas();

            for (int y = 0; y <= Canvas.Height + 1; y++)
            {
                for (int x = 0; x <= Canvas.Width + 1; x++)
                {
                    OutputWriter.SendToOutput(canvas[x, y]);
                }
                OutputWriter.SendToOutput("", true);
            }
        }

        /// <summary>
        /// Sets the canvas.
        /// </summary>
        public void SetTheCanvas()
        {
            char[,] canvas = Canvas.GetCanvas();

            for (int x = 0; x <= Canvas.Width + 1; x++)
            {
                canvas[x, 0] = '-';
                canvas[x, Canvas.Height + 1] = '-';
            }
            for (int y = 1; y <= Canvas.Height; y++)
            {
                canvas[0, y] = '|';
                canvas[Canvas.Width + 1, y] = '|';
            }

            Canvas.SetCanvas(canvas);
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public void DrawLine(Point from, Point to)
        {
            char[,] canvas = Canvas.GetCanvas();

            for (int x = from.X; x <= to.X; x++)
            {
                canvas[x, from.Y] = 'x';
            }
            for (int y = from.Y; y <= to.Y; y++)
            {
                canvas[from.X, y] = 'x';
            }

            Canvas.SetCanvas(canvas);

        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public void DrawRectangle(Point from, Point to)
        {
            char[,] canvas = Canvas.GetCanvas();

            for (int x = from.X; x <= to.X; x++)
            {
                canvas[x, from.Y] = 'x';
                canvas[x, to.Y] = 'x';
            }
            for (int y = from.Y; y <= to.Y; y++)
            {
                canvas[from.X, y] = 'x';
                canvas[to.X, y] = 'x';
            }

            Canvas.SetCanvas(canvas);
        }

        /// <summary>
        /// paints the fill area.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="replacementColor">Color of the replacement.</param>
        public void BucketFillArea(Point point, char replacementColor)
        {
            var existingColor = Canvas.GetCanvas()[point.X, point.Y];

            if (replacementColor == existingColor)
                return;

            FloodFill(Canvas, point.X, point.Y, replacementColor);
        }

        /// <summary>
        /// Flood fill algorithm, Stack based 4-way algorithm
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="replacementColor">Color of the replacement.</param>
        private void FloodFill(Canvas canvas, int x, int y, char replacementColor)
        {
            Stack<Point> nodes = new Stack<Point>();
            var targetColor = Canvas.GetCanvas()[x, y];
            nodes.Push(new Point(x, y));

            while (nodes.Count > 0)
            {
                Point a = nodes.Pop();
                if (a.X < canvas.Width + 1 && a.X > 0 && a.Y < canvas.Height + 1 && a.Y > 0)
                {
                    if (canvas.GetCanvas()[a.X, a.Y] == targetColor)
                    {
                        canvas.GetCanvas()[a.X, a.Y] = replacementColor;
                        nodes.Push(new Point(a.X - 1, a.Y));
                        nodes.Push(new Point(a.X + 1, a.Y));
                        nodes.Push(new Point(a.X, a.Y - 1));
                        nodes.Push(new Point(a.X - 1, a.Y + 1));
                    }
                }
            }
        }

        ///// <summary>
        ///// Flood Fill, Scan Fill, needs tweaking
        ///// </summary>
        ///// <param name="canvas">The canvas.</param>
        ///// <param name="x">The x.</param>
        ///// <param name="y">The y.</param>
        ///// <param name="replacementColor">Color of the replacement.</param>
        //private void FloodFill1(Canvas canvas, int x, int y, char replacementColor)
        //{
        //    Stack<Point> pixels = new Stack<Point>();

        //    var targetColor = canvas.GetCanvas()[x, y];

        //    int y1;
        //    bool spanLeft, spanRight;
        //    pixels.Push(new Point(x, y));

        //    while (pixels.Count != 0)
        //    {
        //        Point temp = pixels.Pop();
        //        y1 = temp.Y;
        //        while (y1 > 0 && canvas.GetCanvas()[temp.X, y1] == targetColor)
        //        {
        //            y1--;
        //        }
        //        y1++;
        //        spanLeft = false;
        //        spanRight = false;
        //        while (y1 < canvas.Height && canvas.GetCanvas()[temp.X, y1] == targetColor)
        //        {
        //            canvas.GetCanvas()[temp.X, y1] = replacementColor;

        //            if (!spanLeft && temp.X > 0 && canvas.GetCanvas()[temp.X - 1, y1] == targetColor)
        //            {
        //                pixels.Push(new Point(temp.X - 1, y1));
        //                spanLeft = true;
        //            }
        //            else if (spanLeft && temp.X - 1 == 0 && canvas.GetCanvas()[temp.X - 1, y1] != targetColor)
        //            {
        //                spanLeft = false;
        //            }
        //            if (!spanRight && temp.X < canvas.Width - 1 && canvas.GetCanvas()[temp.X + 1, y1] == targetColor)
        //            {
        //                pixels.Push(new Point(temp.X + 1, y1));
        //                spanRight = true;
        //            }
        //            else if (spanRight && temp.X < canvas.Width - 1 && canvas.GetCanvas()[temp.X + 1, y1] != targetColor)
        //            {
        //                spanRight = false;
        //            }
        //            y1++;
        //        }

        //    }
        //}
    }
}