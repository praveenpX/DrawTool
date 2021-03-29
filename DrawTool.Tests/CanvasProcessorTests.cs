using System;
using DrawTool.Model;
using DrawTool.Processors;
using DrawTool.Renderers;
using DrawTool.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DrawTool.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CanvasProcessorTests
    {
        private IInputCommandReader _inputCommandReader;
        private Mock<ICanvasRenderer> _canvasRenderer;
        private Canvas _canvas;
        private ICanvasProcessor _canvasProcessor;
        private int x = 20, y = 4;
        private IOutputWriter _outputWriter;


        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            _outputWriter = new StandardInputOutputWriter();
            _canvas = Mock.Of<Canvas>();
            _canvasRenderer = new Mock<ICanvasRenderer>();
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            _canvas = null;
            _canvasRenderer = null;
            _outputWriter = null;
        }

        /// <summary>
        /// Creates the canvas should draw canvas for correct input.
        /// </summary>
        [TestMethod]
        public void CreateCanvasShouldDrawCanvasForCorrectInput()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4");
            _canvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);
            _canvasProcessor.CanvasRenderer = _canvasRenderer.Object;

            _canvasProcessor.CreateCanvas();

            _canvasRenderer.Verify(x => x.SetTheCanvas(), Times.Once());
            _canvasRenderer.Verify(x => x.Draw(), Times.Once());
        }

        /// <summary>
        /// Creates the canvas should not create a canvas for incorrect input.
        /// </summary>
        [TestMethod]
        public void CreateCanvasShouldNotCreateACanvasForIncorrectInput()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "draw a canvas");
            _canvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);
            _canvasProcessor.CanvasRenderer = _canvasRenderer.Object;

            Assert.IsNull(_canvasProcessor.CreateCanvas());
        }

        /// <summary>
        /// Shoulds the draw a line for valid input.
        /// </summary>
        [TestMethod]
        public void ShouldDrawALineForValidInput()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4");
            _canvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);
            _canvasProcessor.CanvasRenderer = _canvasRenderer.Object;

            string[] tokens = { "L", "1", "2", "6", "2" };
            Point from = new Point(1, 2);
            Point to = new Point(6, 2);

            _canvas = Mock.Of<Canvas>(m =>
                m.Width == x &&
                m.Height == y);

            _canvasProcessor.DrawLineOnCanvas(_canvas, tokens);

            _canvasRenderer.Verify(x => x.DrawLine(from, to), Times.Once());
            _canvasRenderer.Verify(x => x.Draw(), Times.Once());
        }

        /// <summary>
        /// Shoulds the not draw a line for invalid points.
        /// </summary>
        [TestMethod]
        public void ShouldNotDrawALineForInvalidPoints()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4");
            _canvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);
            _canvasProcessor.CanvasRenderer = _canvasRenderer.Object;

            string[] tokens = { "L", "1", "1", "4", "4" };

            _canvas = Mock.Of<Canvas>(m =>
                m.Width == x &&
                m.Height == y);

            _canvasProcessor.DrawLineOnCanvas(_canvas, tokens);

            _canvasRenderer.Verify(v => v.DrawLine(new Point(1, 1), new Point(4, 4)), Times.Never());
            _canvasRenderer.Verify(v => v.Draw(), Times.Never());
        }

        /// <summary>
        /// Shoulds the draw a rectangle for valid input.
        /// </summary>
        [TestMethod]
        public void ShouldDrawARectangleForValidInput()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4");
            _canvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);
            _canvasProcessor.CanvasRenderer = _canvasRenderer.Object;


            String[] tokens = { "R", "16", "1", "20", "3" };
            Point from = new Point(16, 1);
            Point to = new Point(20, 3);

            _canvas = Mock.Of<Canvas>(m =>
                m.Width == x &&
                m.Height == y);

            _canvasProcessor.DrawARectangleOnCanvas(_canvas, tokens);

            _canvasRenderer.Verify(v => v.DrawRectangle(from, to), Times.Once());
            _canvasRenderer.Verify(v => v.Draw(), Times.Once());
        }

        /// <summary>
        /// Shoulds the not draw a rectangle for out of bounds input.
        /// </summary>
        [TestMethod]
        public void ShouldNotDrawARectangleForOutOfBoundsInput()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4");
            _canvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);
            _canvasProcessor.CanvasRenderer = _canvasRenderer.Object;


            String[] tokens = { "R", "16", "1", "20", "5" };
            Point from = new Point(16, 1);
            Point to = new Point(20, 3);

            _canvas = Mock.Of<Canvas>(m =>
                m.Width == x &&
                m.Height == y);

            _canvasProcessor.DrawARectangleOnCanvas(_canvas, tokens);

            _canvasRenderer.Verify(v => v.DrawRectangle(from, to), Times.Never());
            _canvasRenderer.Verify(v => v.Draw(), Times.Never());
        }

        /// <summary>
        /// Shoulds the bucket fill.
        /// </summary>
        [TestMethod]
        public void ShouldBucketFill()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4");
            _canvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);
            _canvasProcessor.CanvasRenderer = _canvasRenderer.Object;

            String[] tokens = { "B", "10", "3", "o" };
            Point point = new Point(10, 3);

            _canvas = Mock.Of<Canvas>(m =>
                m.Width == x &&
                m.Height == y);

            _canvasProcessor.BucketFillOnCanvas(_canvas, tokens);

            _canvasRenderer.Verify(v => v.BucketFillArea(point, 'o'), Times.Once());
            _canvasRenderer.Verify(v => v.Draw(), Times.Once());
        }

        /// <summary>
        /// Shoulds the not bucket fill for invalid input.
        /// </summary>
        [TestMethod]
        public void ShouldNotBucketFillForInvalidInput()
        {
            _inputCommandReader = Mock.Of<IInputCommandReader>(m => m.ReadCommands() == "20 4");
            _canvasProcessor = new CanvasProcessor(_inputCommandReader, _outputWriter);
            _canvasProcessor.CanvasRenderer = _canvasRenderer.Object;

            String[] tokens = { "B", "21", "3", "o" };
            Point point = new Point(21, 3);

            _canvas = Mock.Of<Canvas>(m =>
                m.Width == x &&
                m.Height == y);

            _canvasProcessor.BucketFillOnCanvas(_canvas, tokens);

            _canvasRenderer.Verify(v => v.BucketFillArea(point, 'o'), Times.Never());
            _canvasRenderer.Verify(v => v.Draw(), Times.Never());
        }
    }
}