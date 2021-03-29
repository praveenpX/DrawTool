using DrawTool.Model;
using DrawTool.Renderers;
using DrawTool.Support;

namespace DrawTool.Processors
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICanvasProcessor
    {
        IInputCommandReader InputCommandReader { get; set; }
        ICanvasRenderer CanvasRenderer { get; set; }
        Canvas CreateCanvas();
        void DrawLineOnCanvas(Canvas canvas, string[] tokens);
        void DrawARectangleOnCanvas(Canvas canvas, string[] tokens);
        void BucketFillOnCanvas(Canvas canvas, string[] tokens);
    }
}