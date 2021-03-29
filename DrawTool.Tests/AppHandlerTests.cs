using DrawTool.Processors;
using DrawTool.Support;
using DrawTool.Tests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DrawTool.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class AppHandlerTests
    {
        private IInputCommandReader _inputCommandReader;
        private IOutputWriter _outputWriter;

        [TestInitialize]
        public void Setup()
        {
            _outputWriter = new StandardInputOutputWriter();
        }

        [TestCleanup]
        public void TearDown()
        {
            _outputWriter = null;
        }

        /// <summary>
        /// Tests the create canvas.
        /// </summary>
        [TestMethod]
        public void TestCreateCanvas()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4");

            var appHandler = new AppHandlerFake();

            appHandler.CanvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);

            var canvas = appHandler.CreateNewCanvas();

            Assert.AreEqual(canvas.Width, 20);
            Assert.AreEqual(canvas.Height, 4);
        }

        /// <summary>
        /// Canvases the should not be created with invalid input.
        /// </summary>
        [TestMethod]
        public void CanvasShouldNotBeCreatedWithInvalidInput()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4 10");

            var appHandler = new AppHandlerFake();

            appHandler.CanvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);

            var canvas = appHandler.CreateNewCanvas();

            Assert.IsNull(canvas);
        }

        /// <summary>
        /// Tests for canvas input.
        /// </summary>
        [TestMethod]
        public void TestForCanvasInput()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "1");

            var appHandler = new AppHandlerFake();
            appHandler.OutputWriter = _outputWriter;

            appHandler.CanvasProcessor =
                new CanvasProcessor(Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4"), _outputWriter);

            appHandler.ProcessInput('1');
        }

        /// <summary>
        /// Tests for canvas exit.
        /// </summary>
        [TestMethod]
        public void TestForCanvasExit()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "3");

            var appHandler = new AppHandlerFake();

            appHandler.OutputWriter = _outputWriter;

            appHandler.CanvasProcessor =
                new CanvasProcessor(Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4"), _outputWriter);

            var environmentWrapper = new Mock<IEnvironmentWrapper>();

            environmentWrapper.Setup(x => x.Exit(0)).Verifiable();

            appHandler.EnvironmentWrapper = environmentWrapper.Object;

            appHandler.ProcessInput('3');

            environmentWrapper.VerifyAll();
        }
    }
}