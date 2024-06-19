using Core.Models;
using UnityEngine;
using Visual.Extensions;

namespace Visual
{
    public class RectangleView : MonoBehaviour
    {
        public RectangleView Initialize(Rectangle rectangle)
        {
            RectTransform rectTransform = (RectTransform)transform;
            Rect uniRect = rectangle.ToUniRect();
            rectTransform.anchoredPosition = uniRect.position;
            rectTransform.sizeDelta = uniRect.size;
            return this;
        }
    }
}