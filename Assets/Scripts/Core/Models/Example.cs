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
                newPrimaryRectangle.TopLeft.X = Math.Min(newPrimaryRectangle.TopLeft.X, rectangle.MinX);
                newPrimaryRectangle.TopLeft.Y = Math.Max(newPrimaryRectangle.TopLeft.Y, rectangle.MaxY);
                newPrimaryRectangle.BottomRight.X = Math.Max(newPrimaryRectangle.BottomRight.X, rectangle.MaxX);
                newPrimaryRectangle.BottomRight.Y = Math.Min(newPrimaryRectangle.BottomRight.Y, rectangle.MinY);
            }

            Example solution = CloneInternal();
            solution.PrimaryRectangle = newPrimaryRectangle;
            
            return  solution;
        }

        private Example CloneInternal() => new Example(PrimaryRectangle, SecondaryRectangles);

        public object Clone() => CloneInternal();
    }
}