using System;
using System.Collections.Generic;
using System.Linq;
using DrawTool.Model;
using DrawTool.Processors;
using DrawTool.Support;

namespace DrawTool.Handlers
{
    /// <summary>
    /// AppHandler for the Draw Tool to draw canvas, shapes, bucket fill and exit
    /// </summary>
    public class AppHandler
    {
        public IList<Canvas> Canvases { get; set; }
        public IInputCommandReader InputCommandReader { get; set; }
        public IInputCommandValidator InputCommandValidator { get; set; }
        public ICanvasProcessor CanvasProcessor { get; set; }
        public IEnvironmentWrapper EnvironmentWrapper { get; set; }
        public IOutputWriter OutputWriter { get; set; }

        protected AppHandler()
        {
            Canvases = new List<Canvas>();
        }

        public AppHandler(IInputCommandReader inputCommandReader, IInputCommandValidator inputCommandValidator, IOutputWriter outputWriter)
        {
            Canvases = new List<Canvas>();
            this.EnvironmentWrapper = new EnvironmentWrapper();
            this.OutputWriter = outputWriter;
            this.InputCommandReader = inputCommandReader;
            this.InputCommandValidator = inputCommandValidator;
            this.CanvasProcessor = new CanvasProcessor(inputCommandReader, OutputWriter);
        }

        /// <summary>
        /// Displays the menu.
        /// </summary>
        public void DisplayMenu()
        {
            while (true)
            {
                if (this.Canvases.Count > 0)
                {
                    OutputWriter.SendToOutput("1. Create a new canvas, (If you already have one canvas created, select option 2 to start drawing shapes and paint over it)", true);
                }
                else
                {
                    OutputWriter.SendToOutput("1. Create a new canvas in order to draw shapes and paint over it", true);
                }
                OutputWriter.SendToOutput("2. Start drawing on the canvas by issuing various commands", true);
                OutputWriter.SendToOutput("3. Quit", true);

                char input = '0';
                try
                {
                    input = InputCommandReader.ReadCommands()[0];
                }
                catch (Exception)
                {
                    OutputWriter.SendToOutput("Press Enter...", true);
                }

                ProcessInput(input);
            }
        }

        /// <summary>
        /// Processes the input
        /// </summary>
        /// <param name="input">The input.</param>
        protected void ProcessInput(Char input)
        {
            switch (input)
            {
                case '1':
                    Canvas canvas = CreateNewCanvas();

                    this.Canvases.Clear();

                    if (canvas != null)
                    {
                        this.Canvases.Add(canvas);
                        OutputWriter.SendToOutput("Enter 2 to start drawing shapes", true);
                    }
                    else
                    {
                       OutputWriter.SendToOutput("Please select option 1 and then start creating shapes", true);
                    }

                    break;
                case '2':
                    if (this.Canvases.Count > 0)
                    {
                        CanvasHandler canvasHandler = new CanvasHandler(Canvases.First(), InputCommandReader, InputCommandValidator, OutputWriter);
                        canvasHandler.DisplayMenu();
                    }
                    else
                    {
                        OutputWriter.SendToOutput("There is no canvas created, please select option 1 and then start creating shapes", true);
                    }
                    break;
                case '3':
                    OutputWriter.SendToOutput("Quitting the application now", true);
                    AppExit();
                    break;
                default:
                    OutputWriter.SendToOutput("Invalid input, please select the options from the menu below", true);
                    break;
            }
        }

        /// <summary>
        /// Creates the new canvas.
        /// </summary>
        /// <returns></returns>
        protected Canvas CreateNewCanvas()
        {
            return CanvasProcessor.CreateCanvas();
        }

        /// <summary>
        /// Applications the exit.
        /// </summary>
        protected void AppExit()
        {
            EnvironmentWrapper.Exit(0);
        }
    }
}