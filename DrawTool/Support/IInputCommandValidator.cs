using System;

namespace DrawTool.Support
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInputCommandValidator
    {
        bool ValidateInput(String[] tokens, int numberOfTokens);
    }
}