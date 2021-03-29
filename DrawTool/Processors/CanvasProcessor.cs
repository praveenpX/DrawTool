using System;
using System.Linq;
using System.Text.RegularExpressions;
using DrawTool.ErrorManagement;
using DrawTool.Extensions;
using DrawTool.Model;
using DrawTool.Renderers;
using DrawTool.Support;

namespace DrawTool.Processors
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICanvasProcessor" />
    public class CanvasProcessor : ICanvasProcessor
    {
        public IInputCommandReader InputCommandReader { get; set; }
        public ICanvasRenderer CanvasRenderer { get; set; }
        public IOutputWriter OutputWriter { get; set; }

        protected CanvasProcessor()
        { }

        public CanvasProcessor(IInputCommandReader inputCommandReader, IOutputWriter outputWriter)
        {
            InputCommandReader = inputCommandReader;
            OutputWriter = outputWriter;
        }

        public CanvasProcessor(IInputCommandReader inputCommandReader, ICanvasRenderer canvasRenderer, IOutputWriter outputWriter)
        {
            InputCommandReader = inputCommandReader;
            OutputWriter = outputWriter;
            CanvasRenderer = canvasRenderer;

        }

        /// <summary>
        /// Creates the canvas.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidInputDataException">
        /// </exception>
        public Canvas CreateCanvas()
        {
            OutputWriter.SendToOutput("Please enter the canvas width and height with a space ex:20 4", true);

            Canvas canvas = null;

            try
            {
                string input = InputCommandReader.ReadCommands();

                string[] commandArgs = Regex.Split(input, @"\s+").Where(s => s != string.Empty).ToArray();

                if (commandArgs.Count() < 2 || commandArgs.Count() > 2)
                {
                    throw new InvalidInputDataException();
                }

                int width = commandArgs[0].AsInt();
                int height = commandArgs[1].AsInt();

                if (width < 1 || height < 1)
                {
                    throw new InvalidInputDataException();
                }

                canvas = new Canvas(width, height);

                // Set the new canvas on the drawer
                if (CanvasRenderer == null)
                {

                    CanvasRenderer = new CanvasRenderer(canvas, OutputWriter);
                }
                else {

                    CanvasRenderer.Canvas = canvas;
                }

                CanvasRenderer.SetTheCanvas();
                CanvasRenderer.Draw();

                OutputWriter.SendToOutput("Canvas created", true);

            }
            catch (Exception ex)
            {
                if (ex is InvalidInputDataException | ex is FormatException)
                    OutputWriter.SendToOutput(ex.Message, true);
            }

            return canvas;
        }

        /// <summary>
        /// Draws the line on canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="tokens">The tokens.</param>
        public void DrawLineOnCanvas(Canvas canvas, string[] tokens)
        {
            try
            {
                Point[] points = GetPoints(canvas, tokens, 2);

                if ((points[0].X == points[1].X) || (points[0].Y == points[1].Y))
                {

                    // Change position of the points if the first one is greater that the first
                    Point.ReOrderPoints(points[0], points[1]);

                    CanvasRenderer.DrawLine(points[0], points[1]);
                    CanvasRenderer.Draw();
                }
                else
                {
                    OutputWriter.SendToOutput("A line can't be drawn with the above points", true);
                }
            }
            catch (InvalidPointException invalidPointException)
            {
                OutputWriter.SendToOutput(invalidPointException.Message, true);
            }
        }

        /// <summary>
        /// Draws a rectangle on canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="tokens">The tokens.</param>
        public void DrawARectangleOnCanvas(Canvas canvas, string[] tokens)
        {
            try
            {
                Point[] points = GetPoints(canvas, tokens, 2);

                Point.ReOrderPoints(points[0], points[1]);

                CanvasRenderer.DrawRectangle(points[0], points[1]);
                CanvasRenderer.Draw();

            }
            catch (InvalidPointException invalidPointException)
            {
                OutputWriter.SendToOutput(invalidPointException.Message, true);
            }
        }

        /// <summary>
        /// Buckets the fill on canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="tokens">The tokens.</param>
        public void BucketFillOnCanvas(Canvas canvas, string[] tokens)
        {
            try
            {
                Point[] points = GetPoints(canvas, tokens, 1);

                char newColor = tokens[tokens.Length - 1][0];

                CanvasRenderer.BucketFillArea(points[0], newColor);

                CanvasRenderer.Draw();
            }
            catch (InvalidPointException invalidPointException)
            {
                OutputWriter.SendToOutput(invalidPointException.Message, true);
            }
        }

        /// <summary>
        /// Gets the points.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="numbeOfPoints">The numbe of points.</param>
        /// <returns></returns>
        /// <exception cref="InvalidPointException"></exception>
        private Point[] GetPoints(Canvas canvas, String[] tokens, int numbeOfPoints)
        {

            Point[] points = new Point[numbeOfPoints];
            int j = 0;
            int pointPosition = 1;

            for (int i = 0; i < numbeOfPoints; i++)
            {
                Point point = new Point((tokens[pointPosition].AsInt()), tokens[++pointPosition].AsInt());

                bool isInsideCanvas = point.IsValidPoint(canvas);

                if (!isInsideCanvas)
                {
                    throw new InvalidPointException();
                }

                points[j] = point;
                j++;
                pointPosition++;
            }
            return points;
        }
    }
}