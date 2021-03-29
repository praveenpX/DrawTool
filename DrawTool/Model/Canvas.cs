using System;

namespace DrawTool.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Canvas
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private Char[,] _canvas;

        private const int CanvasMargin = 2;

        protected Canvas()
        {

        }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;

            _canvas = new char[width + CanvasMargin, height + CanvasMargin];
        }

        /// <summary>
        /// Gets the canvas.
        /// </summary>
        /// <returns></returns>
        public virtual Char[,] GetCanvas()
        {
            return _canvas;
        }

        /// <summary>
        /// Sets the canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <returns></returns>
        public Char[,] SetCanvas(Char[,] canvas)
        {
            return this._canvas = canvas;
        }
    }
}