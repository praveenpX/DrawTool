using System;

namespace DrawTool.Extensions
{
    /// <summary>
    /// StringExtensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the string representation of a number to its int equivalent
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int AsInt(this string value)
        {
            int result;
            Int32.TryParse(value, out result);
            return result;
        }
    }
}