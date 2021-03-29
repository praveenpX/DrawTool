using System;
using DrawTool.Handlers;
using DrawTool.Support;

namespace DrawTool
{
    /// <summary>
    /// Draw Tool Console Application 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// App Entry point
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            var outputWriter = new StandardInputOutputWriter();

            //Greetings
            outputWriter.SendToOutput("Hello, Welcome to the Drawing Tool", true);
            outputWriter.SendToOutput("Your window size is " + Console.WindowWidth +' ' + Console.WindowHeight, true);
            outputWriter.SendToOutput("Please limit your canvas size to " + (Console.WindowWidth - 2) + ' ' + (Console.WindowHeight - 2) + " for optimal experience, Select from the following options", true);
            outputWriter.SendToOutput("", true);

            AppHandler appHandler = new AppHandler(new InputCommandReader(), new InputCommandValidator(), outputWriter);

            appHandler.DisplayMenu();
        }
    }
}
