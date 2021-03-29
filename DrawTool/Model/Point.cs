namespace DrawTool.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Point
    {
        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Determines whether [is valid point] [the specified canvas].
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <returns>
        ///   <c>true</c> if [is valid point] [the specified canvas]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidPoint(Canvas canvas)
        {
            if (this.X > 0 && this.Y > 0 && this.X <= canvas.Width && this.Y <= canvas.Height)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Re orders the order points.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        public static void ReOrderPoints(Point p1, Point p2)
        {
            if ((p1.X <= p2.X) && p1.Y <= p2.Y) return;

            Point temp = new Point(p1.X, p1.Y);

            p1.X = p2.X;
            p1.Y = p2.Y;

            p2.X = temp.X;
            p2.Y = temp.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point)obj);
        }
    }
}