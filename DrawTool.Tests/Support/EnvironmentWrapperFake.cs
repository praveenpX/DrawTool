using DrawTool.Support;

namespace DrawTool.Tests.Support
{
    /// <summary>
    /// EnvironmentWrapperFake
    /// </summary>
    /// <seealso cref="EnvironmentWrapper" />
    public class EnvironmentWrapperFake : EnvironmentWrapper
    {
        public new void Exit(int code)
        {
            return;
        }
    }
}