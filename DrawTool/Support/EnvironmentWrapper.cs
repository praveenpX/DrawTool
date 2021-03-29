using System;

namespace DrawTool.Support
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DrawTool.Support.IEnvironmentWrapper" />
    public class EnvironmentWrapper : IEnvironmentWrapper
    {
        public void Exit(int code)
        {
            Environment.Exit(code);
        }
    }
}