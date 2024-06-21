using UnityEngine;
using UnityEngine.UI;

namespace Services.Render
{
    public class RectangleView : MonoBehaviour
    {
        [SerializeField] 
        private Image _image;
        
        public RectangleView Initialize(Rect rectangle)
        {
            RectTransform rectTransform = (RectTransform)transform;
            rectTransform.anchoredPosition = rectangle.position;
            rectTransform.sizeDelta = rectangle.size;
            return this;
        }
        
        public RectangleView SetColor(Color color)
        {
            _image.color = color;
            return this;
        }
    }
}