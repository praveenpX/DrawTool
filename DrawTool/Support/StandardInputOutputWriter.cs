using System;

namespace DrawTool.Support
{
    /// <summary>
    /// Console IO
    /// </summary>
    /// <seealso cref="DrawTool.Support.IOutputWriter" />
    public class StandardInputOutputWriter : IOutputWriter
    {
        public void SendToOutput(string output, bool newLine = false)
        {
            if (newLine)
            {
                Console.WriteLine(output);
            }
            else
            {
                Console.Write(output);
            }
        }

        public void SendToOutput(Char value)
        {
            Console.Write(value);
        }
    }
}