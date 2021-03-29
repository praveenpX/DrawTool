using DrawTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawTool.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class CanvasTests
    {
        /// <summary>
        /// Tests the canvas for consistency.
        /// </summary>
        [TestMethod]
        public void TestCanvasForConsistency()
        {
            //arrange
            int width = 20;
            int height = 4;

            var canvas = new Canvas(width, height);

            Assert.AreEqual(canvas.Width, width);
            Assert.AreEqual(canvas.Height, height);
            Assert.AreEqual(canvas.GetCanvas().GetLength(0), width + 2);
            Assert.AreEqual(canvas.GetCanvas().GetLength(1), height + 2);
        }
    }
}