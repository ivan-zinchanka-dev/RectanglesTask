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
            Vector2 topLeft = rectangle.Start.ToUniVector2();
            Vector2 bottomRight = rectangle.End.ToUniVector2();
            
            Vector2 centerPoint = (topLeft + bottomRight) / 2f;
            Vector2 size = new Vector2(Mathf.Abs(topLeft.x - bottomRight.x), Mathf.Abs(topLeft.y - bottomRight.y));
            Rect result = new Rect(centerPoint, size);
            
            return result;
        }
    }
}