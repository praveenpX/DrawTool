using DrawTool.Model;
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
    public class CanvasRendererTests
    {
        private Canvas _mockCanvas;
        private ICanvasRenderer _canvasRenderer;
        private char[,] _createdCanvas;
        private int x = 20, y = 4;
        private IOutputWriter _outputWriter;

        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            //arrange
            _outputWriter = new StandardInputOutputWriter();
            _createdCanvas = new char[x + 2, y + 2];
            _outputWriter = new StandardInputOutputWriter();

            _mockCanvas = Mock.Of<Canvas>(m => m.GetCanvas() == _createdCanvas &&
                                              m.Width == x &&
                                              m.Height == y);

            _canvasRenderer = new CanvasRenderer(_mockCanvas, _outputWriter);
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            _createdCanvas = null;
            _mockCanvas = null;
            _canvasRenderer = null;
            _outputWriter = null;
        }

        /// <summary>
        /// Tests the canvas edges for consistency.
        /// </summary>
        [TestMethod]
        public void TestCanvasEdgesForConsistency()
        {
            _canvasRenderer.SetTheCanvas();

            for (int i = 0; i <= x + 1; i++)
            {
                Assert.AreEqual('-', _createdCanvas[i, 0]);
                Assert.AreEqual('-', _createdCanvas[i, y + 1]);
            }
            for (int j = 1; j <= _mockCanvas.Height; j++)
            {
                Assert.AreEqual('|', _createdCanvas[0, j]);
                Assert.AreEqual('|', _createdCanvas[x + 1, j]);
            }
        }

        /// <summary>
        /// Tests the draw line for consistency.
        /// </summary>
        [TestMethod]
        public void TestDrawLineForConsistency()
        {
            Point from = new Point(1, 2);
            Point to = new Point(6, 2);

            _canvasRenderer.DrawLine(from, to);

            for (int i = from.X; i <= to.X; i++)
            {
                Assert.AreEqual('x', _createdCanvas[i, from.Y]);
            }
            for (int i = from.Y; i <= to.Y; i++)
            {
                Assert.AreEqual('x', _createdCanvas[from.X, i]);
            }
        }

        /// <summary>
        /// Tests the draw rectangle for consistency.
        /// </summary>
        [TestMethod]
        public void TestDrawRectangleForConsistency()
        {
            Point from = new Point(2, 2);
            Point to = new Point(4, 4);

            _canvasRenderer.DrawRectangle(from, to);

            for (int i = from.X; i <= to.X; i++)
            {
                Assert.AreEqual('x', _createdCanvas[i, from.Y]);
                Assert.AreEqual('x', _createdCanvas[i, to.Y]);
            }
            for (int i = from.Y + 1; i <= to.Y - 1; i++)
            {
                Assert.AreEqual('x', _createdCanvas[from.X, i]);
                Assert.AreEqual('x', _createdCanvas[to.X, i]);
            }
        }

        /// <summary>
        /// Tests the bucket fill for consistency.
        /// </summary>
        [TestMethod]
        public void TestBucketFillForConsistency()
        {
            Point from = new Point(2, 2);

            _canvasRenderer.BucketFillArea(from, 'o');

            Assert.AreEqual('o', _createdCanvas[2, 2]);
        }
    }
}