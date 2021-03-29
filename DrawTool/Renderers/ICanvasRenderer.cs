using DrawTool.Model;

namespace DrawTool.Renderers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICanvasRenderer
    {
        Canvas Canvas { get; set; }
        void Draw();
        void SetTheCanvas();
        void DrawLine(Point from, Point to);
        void DrawRectangle(Point from, Point to);
        void BucketFillArea(Point point, char replacementColor);
    }
}