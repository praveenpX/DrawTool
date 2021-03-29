using System;

namespace DrawTool.Support
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutputWriter
    {
        void SendToOutput(string output, bool newLine);
        void SendToOutput(Char value);
    }
}