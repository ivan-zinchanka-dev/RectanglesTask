using Core.Models;
using UnityEngine;

namespace Visual
{
    public class Drawer : MonoBehaviour
    {
        [SerializeField] private RectangleView _rectanglePrefab;
        [SerializeField] private Transform _sourceArea;
        
        public void DrawExample(Example example)
        {
            foreach (var rectangle in example.SecondaryRectangles)
            {
                DrawRectangle(rectangle);
            }
            
            DrawRectangle(example.PrimaryRectangle);
        }

        public void DrawRectangle(Rectangle rectangle)
        {
            Instantiate<RectangleView>(_rectanglePrefab, _sourceArea, false).Initialize(rectangle);
        }

    }
}