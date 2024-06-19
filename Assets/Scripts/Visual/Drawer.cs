using Core.Models;
using UnityEngine;

namespace Visual
{
    public class Drawer : MonoBehaviour
    {
        [SerializeField] private RectangleView _rectanglePrefab;
        [SerializeField] private Transform _sourceArea;
        [SerializeField] private Transform _resultArea;
        
        public void DrawExample(Example example, bool isSolution = false)
        {
            foreach (var rectangle in example.SecondaryRectangles)
            {
                DrawRectangle(rectangle, isSolution);
            }
            
            DrawRectangle(example.PrimaryRectangle, isSolution);
        }

        public void DrawRectangle(Rectangle rectangle, bool isSolution)
        {
            Instantiate<RectangleView>(_rectanglePrefab, isSolution ? _resultArea : _sourceArea, false)
                .Initialize(rectangle);
        }

    }
}