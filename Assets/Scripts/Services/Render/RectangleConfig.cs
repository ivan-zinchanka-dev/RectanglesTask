using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Services.Render
{
    [CreateAssetMenu(fileName = "rectangle_config", menuName = "Configs/RectangleConfig", order = 0)]
    public class RectangleConfig : ScriptableObject
    {
        [field: SerializeField] 
        public RectangleView PrimaryRectanglePrefab { get; private set; }
        [field:SerializeField] 
        public RectangleView SecondaryRectanglePrefab { get; private set; }
        [SerializeField] 
        private List<Pair<ColorType, Color>> _colors;
        
        public Color GetColorByType(ColorType colorType)
        {
            return _colors.Find(pair => pair.First == colorType).Second;
        }
    }
}