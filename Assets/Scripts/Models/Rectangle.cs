using System;
using System.ComponentModel;
using Models.Enums;
using Newtonsoft.Json;

namespace Models
{
    public struct Rectangle
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)] [DefaultValue(ColorType.Green)]
        public ColorType ColorType;
        public Point Start;
        public Point End;
        
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
    }
}