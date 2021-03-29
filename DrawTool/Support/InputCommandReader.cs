using System;

namespace DrawTool.Support
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DrawTool.Support.IInputCommandReader" />
    public class InputCommandReader : IInputCommandReader
    {
        /// <summary>
        /// Reads the commands.
        /// </summary>
        /// <returns></returns>
        public string ReadCommands()
        {
            return Console.ReadLine();
        }
    }
}