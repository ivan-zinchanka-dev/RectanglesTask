using System;

namespace Core.Models
{
    public class Rectangle : ICloneable
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        
        public double MinX => Math.Min(Start.X, End.X);
        public double MinY => Math.Min(Start.Y, End.Y);
        
        public double MaxX => Math.Max(Start.X, End.X);
        public double MaxY => Math.Max(Start.Y, End.Y);
        
        public Rectangle(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public object Clone()
        {
            return new Rectangle((Point)Start.Clone(), (Point)End.Clone());
        }
    }
}