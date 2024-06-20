using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;

namespace Core.Models
{
    public class Example : ICloneable
    {
        public Rectangle PrimaryRectangle { get; private set; }
        [JsonProperty] private List<Rectangle> _secondaryRectangles = new List<Rectangle>();
        [JsonIgnore] public List<Rectangle> SecondaryRectangles => new List<Rectangle>(_secondaryRectangles);


        public Example(Rectangle primaryRectangle, List<Rectangle> secondaryRectangles)
        {
            PrimaryRectangle = primaryRectangle;
            _secondaryRectangles = secondaryRectangles;
        }

        [Pure]
        public Example Resolve()
        {
            Rectangle newPrimaryRectangle = (Rectangle)_secondaryRectangles[0].Clone();

            for (int i = 1; i < _secondaryRectangles.Count; i++)        // TODO check array size
            {
                Rectangle rectangle = _secondaryRectangles[i];
                newPrimaryRectangle.Start.X = Math.Min(newPrimaryRectangle.Start.X, rectangle.MinX);
                newPrimaryRectangle.Start.Y = Math.Max(newPrimaryRectangle.Start.Y, rectangle.MaxY);
                newPrimaryRectangle.End.X = Math.Max(newPrimaryRectangle.End.X, rectangle.MaxX);
                newPrimaryRectangle.End.Y = Math.Min(newPrimaryRectangle.End.Y, rectangle.MinY);
            }

            Example solution = CloneInternal();
            solution.PrimaryRectangle = newPrimaryRectangle;
            
            return  solution;
        }

        private Example CloneInternal() => new Example(PrimaryRectangle, SecondaryRectangles);

        public object Clone() => CloneInternal();
    }
}