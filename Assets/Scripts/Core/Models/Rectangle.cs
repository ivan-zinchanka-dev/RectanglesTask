using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Core.Models
{
    public class Rectangle : ICloneable
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(ColorType.Green)]
        public ColorType ColorType { get; private set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        
        public double MinX => Math.Min(Start.X, End.X);
        public double MinY => Math.Min(Start.Y, End.Y);
        
        public double MaxX => Math.Max(Start.X, End.X);
        public double MaxY => Math.Max(Start.Y, End.Y);
        
        public Rectangle(ColorType colorType, Point start, Point end)
        {
            ColorType = colorType;
            Start = start;
            End = end;
        }

        public object Clone()
        {
            return new Rectangle(ColorType, (Point)Start.Clone(), (Point)End.Clone());
        }
    }
}