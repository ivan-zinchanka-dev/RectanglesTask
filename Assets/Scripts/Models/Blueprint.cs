using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Models.Enums;
using Newtonsoft.Json;
using UnityEngine;

namespace Models
{
    public class Blueprint : ICloneable
    {
        [JsonIgnore]
        public BlueprintType Type => _type;
        [JsonProperty] 
        private ResolveSettings _resolveSettings;
        public Rectangle PrimaryRectangle { get; private set; }
        [JsonProperty] 
        private List<Rectangle> _secondaryRectangles = new List<Rectangle>();
        [JsonIgnore] 
        public List<Rectangle> SecondaryRectangles => new List<Rectangle>(_secondaryRectangles);
        
        private readonly BlueprintType _type = BlueprintType.Example;
        public bool IsCorrupted { get; }

        public Blueprint(bool isCorrupted)
        {
            IsCorrupted = isCorrupted;
        }

        [JsonConstructor]
        public Blueprint(BlueprintType type, Rectangle primaryRectangle, List<Rectangle> secondaryRectangles)
        {
            _type = type;
            PrimaryRectangle = primaryRectangle;
            _secondaryRectangles = secondaryRectangles;
        }

        [Pure]
        public Blueprint Resolve()
        {
            Debug.Log("Example resolving started");
            Debug.Log("Source secondary rectangles count: " + _secondaryRectangles.Count);
            
            if (IsCorrupted || _secondaryRectangles.Count == 0)
            {
                return CloneInternal(BlueprintType.Solution);
            }

            List<Rectangle> filteredSecondaries = FilterSecondaryRectangles();
            
            Debug.Log("Filtered secondary rectangles count: " + filteredSecondaries.Count);
            
            if (filteredSecondaries.Count == 0)
            {
                return CloneInternal(BlueprintType.Solution);
            }

            List<Point> points = ToPointList(filteredSecondaries);

            Debug.Log("Source points count: " + points.Count);
            
            if (_resolveSettings.ExcludeOuterPoints)
            {
                points.RemoveAll(IsOuterPoint);
            }
            
            Debug.Log("Filtered points count: " + points.Count);
            
            if (points.Count < 2)
            {
                return CloneInternal(BlueprintType.Solution);
            }

            Rectangle newPrimary = CreatePrimaryFromPoints(points);
            
            Debug.Log("Example resolving completed");
            
            Blueprint solution = CloneInternal(BlueprintType.Solution);
            solution.PrimaryRectangle = newPrimary;
            
            return solution;
        }
        
        public object Clone() => CloneInternal(_type);

        private Blueprint CloneInternal(BlueprintType type)
        {
            return IsCorrupted ? new Blueprint(true) : new Blueprint(type, PrimaryRectangle, SecondaryRectangles);
        } 
        
        private List<Rectangle> FilterSecondaryRectangles()
        {
            List<Rectangle> filteredSecondaries = new List<Rectangle>(_secondaryRectangles);

            var includedColors = _resolveSettings.IncludedColors;
            
            if (includedColors != null && includedColors.Count > 0)
            {
                filteredSecondaries.RemoveAll(secondary =>
                    !includedColors.Contains(secondary.ColorType));
            }

            return filteredSecondaries;
        }

        private Rectangle CreatePrimaryFromPoints(List<Point> points)
        {
            Rectangle newPrimary = new Rectangle(PrimaryRectangle.ColorType, points[0], points[1]);
            
            foreach (var point in points)
            {
                newPrimary.Start.X = Math.Min(newPrimary.Start.X, point.X);
                newPrimary.Start.Y = Math.Min(newPrimary.Start.Y, point.Y);
                newPrimary.End.X = Math.Max(newPrimary.End.X, point.X);
                newPrimary.End.Y = Math.Max(newPrimary.End.Y, point.Y);
            }

            return newPrimary;
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
        
    }
}