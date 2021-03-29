using DrawTool.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DrawTool.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class PointTests
    {
        /// <summary>
        /// Tests the point for consistency.
        /// </summary>
        [TestMethod]
        public void TestPointForConsistency()
        {
            //arrange
            int x = 5;
            int y = 10;

            var point = new Point(x, y);

            Assert.AreEqual(point.X, x);
            Assert.AreEqual(point.Y, y);
        }

        [TestMethod]
        public void IsValidPointReturnsTrueForAPointWithInCanvas()
        {
            //arrange
            var mockCanvas = Mock.Of<Canvas>(c => c.Width == 20 && c.Height == 4);

            int x = 10;
            int y = 2;

            var point = new Point(x, y);

            var result = point.IsValidPoint(mockCanvas);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPointReturnsFalseForAPointWhoseWidthOutsideOfCanvas()
        {
            //arrange
            var mockCanvas = Mock.Of<Canvas>(c => c.Width == 20 && c.Height == 4);

            int x = 21;
            int y = 2;

            var point = new Point(x, y);

            var result = point.IsValidPoint(mockCanvas);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPointReturnsFalseForAPointWhoseHeightOutsideOfCanvas()
        {
            //arrange
            var mockCanvas = Mock.Of<Canvas>(c => c.Width == 20 && c.Height == 4);

            int x = 18;
            int y = 5;

            var point = new Point(x, y);

            var result = point.IsValidPoint(mockCanvas);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestReOrderPointsForConsistencyOnXAxis()
        {
            int x = 6;
            int y = 4;

            Point pointA = new Point(x, y);
            Point pointB = new Point(x - 3, y);

            Point.ReOrderPoints(pointA, pointB);

            Assert.AreEqual(pointA.X, x - 3);
            Assert.AreEqual(pointA.Y, y);
            Assert.AreEqual(pointB.X, x);
            Assert.AreEqual(pointB.Y, y);
        }

        [TestMethod]
        public void TestReOrderPointsForConsistencyOnYAxis()
        {
            int x = 6;
            int y = 4;

            Point pointA = new Point(x, y);
            Point pointB = new Point(x, y - 2);

            Point.ReOrderPoints(pointA, pointB);

            Assert.AreEqual(pointA.X, x);
            Assert.AreEqual(pointA.Y, y - 2);
            Assert.AreEqual(pointB.X, x);
            Assert.AreEqual(pointB.Y, y);
        }

        [TestMethod]
        public void TestReOrderPointsWhenPointBIsFartherThanPointA()
        {
            int x = 6;
            int y = 4;

            Point pointA = new Point(x, y);
            Point pointB = new Point(x + 2, y);

            Point.ReOrderPoints(pointA, pointB);

            Assert.AreEqual(pointA.X, x);
            Assert.AreEqual(pointA.Y, y);
            Assert.AreEqual(pointB.X, x + 2);
            Assert.AreEqual(pointB.Y, y);
        }

        [TestMethod]
        public void TestReOrderPointsWhenPointBIsHigherThanPointA()
        {
            int x = 6;
            int y = 4;

            Point pointA = new Point(x, y);
            Point pointB = new Point(x, y + 2);

            Point.ReOrderPoints(pointA, pointB);

            Assert.AreEqual(pointA.X, x);
            Assert.AreEqual(pointA.Y, y);
            Assert.AreEqual(pointB.X, x);
            Assert.AreEqual(pointB.Y, y + 2);
        }
    }
}