using System;

namespace DrawTool.ErrorManagement
{
    /// <summary>
    /// Represents Invalid Input Data Exceptions
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidInputDataException : Exception
    {
        public InvalidInputDataException() : base("Invalid Input")
        {
        }
    }
}