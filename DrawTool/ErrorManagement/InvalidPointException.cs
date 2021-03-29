using System;

namespace DrawTool.ErrorManagement
{
    /// <summary>
    /// Represents Invalid Point Exceptions
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidPointException : Exception
    {
        public InvalidPointException() : base("Please enter a valid point within the boundaries of the canvas")
        {
        }
    }
}