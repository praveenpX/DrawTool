using DrawTool.Model;
using DrawTool.Processors;
using DrawTool.Renderers;
using DrawTool.Support;

namespace  DrawTool.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICanvasHandler
    {
        Canvas Canvas { get; set; }
        IInputCommandReader InputCommandReader { get; set; }
        ICanvasRenderer CanvasRenderer { get; set; }
        ICanvasProcessor CanvasProcessor { get; set; }
        IInputCommandValidator InputCommandValidator { get; set; }
        void DisplayMenu();
        bool ProcessInput(string input);
    }
}