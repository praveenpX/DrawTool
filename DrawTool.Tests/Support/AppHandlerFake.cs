using DrawTool.Handlers;
using DrawTool.Model;

namespace DrawTool.Tests.Support
{
    /// <summary>
    /// AppHandlerFake
    /// </summary>
    /// <seealso cref="AppHandler" />
    public class AppHandlerFake : AppHandler
    {
        public new Canvas CreateNewCanvas()
        {
            return base.CreateNewCanvas();
        }

        public new void ProcessInput(char input)
        {
            base.ProcessInput(input);
        }
    }
}