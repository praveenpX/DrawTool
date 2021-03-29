using DrawTool.Handlers;
using DrawTool.Model;
using DrawTool.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DrawTool.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CanvasHandlerTests
    {
        private IInputCommandReader _inputCommandReader;
        private Canvas _canvas;
        private int x = 20, y = 4;
        private ICanvasHandler _canvasHandler;
        private InputCommandValidator _inputCommandValidator;
        private IOutputWriter _outputWriter;

        [TestInitialize]
        public void Setup()
        {
            _inputCommandValidator = new InputCommandValidator();
            _outputWriter = new StandardInputOutputWriter();
        }

        [TestCleanup]
        public void TearDown()
        {
            _inputCommandValidator = null;
            _outputWriter = null;
        }

        /// <summary>
        /// Validates the input for correct ness.
        /// </summary>
        [TestMethod]
        public void ValidateInputForCorrectNess()
        {
            string[] command = new[] { "L", "1", "2", "6", "2" };

            bool processed = _inputCommandValidator.ValidateInput(command, 5);

            Assert.IsTrue(processed);
        }

        /// <summary>
        /// Corrects the input needs to be handled consistently.
        /// </summary>
        [TestMethod]
        public void CorrectInputNeedsToBeHandledConsistently()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4");

            _canvas = new Canvas(x, y);

            _canvasHandler = new CanvasHandler(_canvas, _inputCommandReader, _inputCommandValidator, _outputWriter);

            string command = "Q";

            bool processed = _canvasHandler.ProcessInput(command);

            Assert.IsTrue(processed);
        }
    }
}