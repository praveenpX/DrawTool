using System;
using DrawTool.Extensions;

namespace DrawTool.Support
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DrawTool.Support.IInputCommandValidator" />
    public class InputCommandValidator : IInputCommandValidator
    {
        /// <summary>
        /// Validates the input.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="numberOfTokens">The number of tokens.</param>
        /// <returns></returns>
        public bool ValidateInput(String[] tokens, int numberOfTokens)
        {
            bool result = tokens.Length == numberOfTokens;

            for (int i = 1; i < tokens.Length; i++)
            {

                if ((i == (numberOfTokens - 1)) && (tokens[0].Equals("B") || tokens[0].Equals("b")))
                {
                    if (tokens[i].Length != 1)
                    {
                        result = false;
                        break;
                    }
                    continue;
                }

                try
                {
                    tokens[i].AsInt();
                }
                catch (FormatException formatException)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}