using System;
using System.Linq;
using System.Text.RegularExpressions;
using DrawTool.ErrorManagement;
using DrawTool.Model;
using DrawTool.Processors;
using DrawTool.Renderers;
using DrawTool.Support;

namespace DrawTool.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ICanvasHandler" />
    public class CanvasHandler : ICanvasHandler
    {
        public Canvas Canvas { get; set; }
        public IInputCommandReader InputCommandReader { get; set; }
        public ICanvasRenderer CanvasRenderer { get; set; }
        public ICanvasProcessor CanvasProcessor { get; set; }
        public IInputCommandValidator InputCommandValidator { get; set; }
        public IOutputWriter OutputWriter { get; set; }

        public CanvasHandler(Canvas canvas, IInputCommandReader inputCommandReader, IInputCommandValidator inputCommandValidator, IOutputWriter outputWriter)
        {
            this.Canvas = canvas;
            this.InputCommandReader = inputCommandReader;
            this.InputCommandValidator = inputCommandValidator;
            this.OutputWriter = outputWriter;
            this.CanvasRenderer = new CanvasRenderer(canvas, OutputWriter);
            this.CanvasProcessor = new CanvasProcessor(inputCommandReader, CanvasRenderer, OutputWriter);
        }

        /// <summary>
        /// Displays the menu.
        /// </summary>
        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                OutputWriter.SendToOutput("enter command ", true);
                OutputWriter.SendToOutput("ex: L 1 2 6 2, L 6 3 6 4 for drawing a line, R 16 1 20 3 to draw a rectangle, B 10 3 o to paint or", true);
                OutputWriter.SendToOutput("q to go back to main menu", true);

                String input = null;
                try
                {
                    input = InputCommandReader.ReadCommands();
                }
                catch (Exception exception)
                {
                    OutputWriter.SendToOutput("Enter...", true);
                }

                exit = ProcessInput(input);
            }
        }

        /// <summary>
        /// Processes the input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        /// <exception cref="InvalidInputDataException">
        /// </exception>
        public bool ProcessInput(string input)
        {
            string[] commandArgs = Regex.Split(input, @"\s+").Where(s => s != string.Empty).ToArray();

            bool isInputValid = false;
            bool result = false;

            try
            {
                switch (commandArgs[0])
                {
                    case "L":
                    case "l":
                        isInputValid = InputCommandValidator.ValidateInput(commandArgs, 5);

                        if (!isInputValid)
                        {
                            throw new InvalidInputDataException();
                        }
                        this.CanvasProcessor.DrawLineOnCanvas(Canvas, commandArgs);
                        break;

                    case "R":
                    case "r":
                        isInputValid = InputCommandValidator.ValidateInput(commandArgs, 5);

                        if (!isInputValid)
                        {
                            throw new InvalidInputDataException();
                        }

                        this.CanvasProcessor.DrawARectangleOnCanvas(Canvas, commandArgs);
                        break;
                    case "B":
                    case "b":
                        isInputValid = InputCommandValidator.ValidateInput(commandArgs, 4);

                        if (!isInputValid)
                        {
                            throw new InvalidInputDataException();
                        }

                        this.CanvasProcessor.BucketFillOnCanvas(Canvas, commandArgs);
                        break;
                    case "Q":
                    case "q":
                        OutputWriter.SendToOutput("Returning to Main Menu", true);
                        result = true;
                        break;
                    default:
                        OutputWriter.SendToOutput("Invalid Option, please choose from below", true);
                        break;
                }
            }
            catch (InvalidInputDataException exception)
            {
                OutputWriter.SendToOutput(exception.Message, true);
            }
            return result;
        }
    }
}