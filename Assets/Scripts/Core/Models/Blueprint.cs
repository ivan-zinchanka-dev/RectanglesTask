using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Models
{
    public class Blueprint : ICloneable
    {
        [JsonProperty] private ResolveSettings _resolveSettings;
        public Rectangle PrimaryRectangle { get; private set; }
        [JsonProperty] 
        private List<Rectangle> _secondaryRectangles = new List<Rectangle>();
        [JsonIgnore] 
        public List<Rectangle> SecondaryRectangles => new List<Rectangle>(_secondaryRectangles);

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

            List<Rectangle> filteredSecondaries = new List<Rectangle>(_secondaryRectangles);

            /*if (_resolveSettings.ExcludeByColors.Count > 0)
            {
                filteredSecondaries.RemoveAll(secondary =>
                {
                    return secondary.MinX < PrimaryRectangle.MinX
                           || secondary.MinY < PrimaryRectangle.MinY
                           || secondary.MaxX > PrimaryRectangle.MaxX
                           || secondary.MaxY > PrimaryRectangle.MaxY;
                });
            }*/
            
            if (filteredSecondaries.Count == 0)
            {
                return CloneInternal(BlueprintType.Solution);
            }

            List<Point> points = ToPointList(filteredSecondaries);

            Debug.Log("Source count: " + points.Count);
            
            if (_resolveSettings.ExcludeOuterPoints)
            {
                points.RemoveAll(IsOuterPoint);
            }
            
            Debug.Log("Filtered count: " + points.Count);
            
            if (points.Count < 2)
            {
                return CloneInternal(BlueprintType.Solution);
            }
            
            //Rectangle newPrimary = (Rectangle)filteredSecondaries[0].Clone();

            Rectangle newPrimary = new Rectangle(PrimaryRectangle.ColorType, 
                (Point)points[0].Clone(), 
                (Point)points[1].Clone());
            
            
            foreach (var point in points)
            {
                newPrimary.Start.X = Math.Min(newPrimary.Start.X, point.X);
                newPrimary.Start.Y = Math.Min(newPrimary.Start.Y, point.Y);
                newPrimary.End.X = Math.Max(newPrimary.End.X, point.X);
                newPrimary.End.Y = Math.Max(newPrimary.End.Y, point.Y);
            }
            
            
            /*Rectangle newPrimary = (Rectangle)filteredSecondaries[0].Clone();

            foreach (var secondary in filteredSecondaries)
            {
                newPrimary.Start.X = Math.Min(newPrimary.Start.X, secondary.MinX);
                newPrimary.Start.Y = Math.Max(newPrimary.Start.Y, secondary.MaxY);
                newPrimary.End.X = Math.Max(newPrimary.End.X, secondary.MaxX);
                newPrimary.End.Y = Math.Min(newPrimary.End.Y, secondary.MinY);
            }*/
            
            
            Blueprint solution = CloneInternal(BlueprintType.Solution);
            solution.PrimaryRectangle = newPrimary;
            
            return solution;
        }

        private bool IsOuterPoint(Point point)
        {
            return point.X < PrimaryRectangle.MinX || 
                   point.Y < PrimaryRectangle.MinY || 
                   point.X > PrimaryRectangle.MaxX || 
                   point.Y > PrimaryRectangle.MaxY;
        }

        private static List<Point> ToPointList(List<Rectangle> rectangles)
        {
            List<Point> points = new List<Point>(rectangles.Count * 2);

            foreach (var rectangle in rectangles)
            {
                points.Add(rectangle.Start);
                points.Add(rectangle.End);
                points.Add(new Point(rectangle.Start.X, rectangle.End.Y));
                points.Add(new Point(rectangle.End.X, rectangle.Start.Y));
            }

            return points;
        }

        public object Clone() => CloneInternal(_type);
        private Blueprint CloneInternal(BlueprintType type) => new(type, PrimaryRectangle, SecondaryRectangles);
    }
}