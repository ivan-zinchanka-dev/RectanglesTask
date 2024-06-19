using System;

namespace Core.Models
{
    public class Rectangle : ICloneable
    {
        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }
        
        public double MinX => Math.Min(TopLeft.X, BottomRight.X);
        public double MinY => Math.Min(TopLeft.Y, BottomRight.Y);
        
        public double MaxX => Math.Max(TopLeft.X, BottomRight.X);
        public double MaxY => Math.Max(TopLeft.Y, BottomRight.Y);
        
        public Rectangle(Point topLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public object Clone()
        {
            return new Rectangle((Point)TopLeft.Clone(), (Point)BottomRight.Clone());
        }
    }
}