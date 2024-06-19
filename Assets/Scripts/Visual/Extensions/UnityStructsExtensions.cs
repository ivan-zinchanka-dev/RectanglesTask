using Core.Models;
using UnityEngine;

namespace Visual.Extensions
{
    public static class UnityStructsExtensions
    {
        public static Vector2 ToUniVector2(this Point point)
        {
            return new Vector2((float)point.X, (float)point.Y);
        }
        
        public static Rect ToUniRect(this Rectangle rectangle)
        {
            return new Rect(rectangle.TopLeft.ToUniVector2(), rectangle.BottomRight.ToUniVector2());
        }
    }
}