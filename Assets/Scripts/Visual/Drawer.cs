using Core.Models;
using UnityEngine;

namespace Visual
{
    public class Drawer : MonoBehaviour
    {
        [SerializeField] private RectangleView _primaryRectanglePrefab;
        [SerializeField] private RectangleView _secondaryRectanglePrefab;
        [SerializeField] private Transform _sourceArea;
        [SerializeField] private Transform _resultArea;
        
        public void DrawExample(Example example, bool isSolution = false)
        {
            DrawPrimaryRectangle(example.PrimaryRectangle, isSolution);
            
            foreach (var rectangle in example.SecondaryRectangles)
            {
                DrawSecondaryRectangle(rectangle, isSolution);
            }
        }

        public void DrawPrimaryRectangle(Rectangle rectangle, bool isSolution)
        {
            Instantiate<RectangleView>(_primaryRectanglePrefab, isSolution ? _resultArea : _sourceArea, false)
                .Initialize(rectangle);
        }

        public void DrawSecondaryRectangle(Rectangle rectangle, bool isSolution)
        {
            Instantiate<RectangleView>(_secondaryRectanglePrefab, isSolution ? _resultArea : _sourceArea, false)
                .Initialize(rectangle);
        }
        
    }
}