using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;

namespace Core.Models
{
    public class Blueprint : ICloneable
    {
        public Rectangle PrimaryRectangle { get; private set; }
        [JsonProperty] private List<Rectangle> _secondaryRectangles = new List<Rectangle>();
        [JsonIgnore] public List<Rectangle> SecondaryRectangles => new List<Rectangle>(_secondaryRectangles);

        private readonly BlueprintType _type = BlueprintType.Example;
        public BlueprintType Type => _type;
        
        public Blueprint(BlueprintType type, Rectangle primaryRectangle, List<Rectangle> secondaryRectangles)
        {
            _type = type;
            PrimaryRectangle = primaryRectangle;
            _secondaryRectangles = secondaryRectangles;
        }

        [Pure]
        public Blueprint Resolve()
        {
            if (_secondaryRectangles.Count == 0)
            {
                return CloneInternal(BlueprintType.Solution);
            }

            Rectangle newPrimary = (Rectangle)_secondaryRectangles[0].Clone();

            foreach (var secondary in _secondaryRectangles)
            {
                newPrimary.Start.X = Math.Min(newPrimary.Start.X, secondary.MinX);
                newPrimary.Start.Y = Math.Max(newPrimary.Start.Y, secondary.MaxY);
                newPrimary.End.X = Math.Max(newPrimary.End.X, secondary.MaxX);
                newPrimary.End.Y = Math.Min(newPrimary.End.Y, secondary.MinY);
            }

            Blueprint solution = CloneInternal(BlueprintType.Solution);
            solution.PrimaryRectangle = newPrimary;
            
            return solution;
        }
        
        public object Clone() => CloneInternal(_type);
        private Blueprint CloneInternal(BlueprintType type) => new(type, PrimaryRectangle, SecondaryRectangles);
    }
}