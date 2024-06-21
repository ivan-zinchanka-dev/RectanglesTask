using Extensions;
using Models;
using UnityEngine;

namespace Services.Render
{
    public class Drawer : MonoBehaviour
    {
        [SerializeField] 
        private Transform _sourceArea;
        [SerializeField] 
        private Transform _resultArea;
        [SerializeField] 
        private GameObject _corruptedMessage;
        [SerializeField] 
        private RectangleConfig _rectangleConfig;
        
        public void DrawBlueprint(Blueprint blueprint)
        {
            if (blueprint.IsCorrupted)
            {
                _corruptedMessage.SetActive(true);
            }
            else
            {
                DrawSecondaryRectangles(blueprint);
                DrawPrimaryRectangle(blueprint);
            }
        }

        public void ClearAllAreas()
        {
            _corruptedMessage.SetActive(false);
            ClearArea(_sourceArea);
            ClearArea(_resultArea);
        }

        public void ClearAreaFor(BlueprintType blueprintType)
        {
            ClearArea(GetArea(blueprintType));
        }

        private void DrawPrimaryRectangle(Blueprint blueprint)
        {
            Instantiate<RectangleView>(_rectangleConfig.PrimaryRectanglePrefab, GetArea(blueprint.Type), false)
                .Initialize(blueprint.PrimaryRectangle.ToUniRect());
        }

        private void DrawSecondaryRectangles(Blueprint blueprint)
        {
            Transform area = GetArea(blueprint.Type);
            
            foreach (var rectangle in blueprint.SecondaryRectangles)
            {
                Instantiate<RectangleView>(_rectangleConfig.SecondaryRectanglePrefab, area, false)
                    .Initialize(rectangle.ToUniRect())
                    .SetColor(_rectangleConfig.GetColorByType(rectangle.ColorType));
            }
        }

        private Transform GetArea(BlueprintType blueprintType)
        {
            if (blueprintType == BlueprintType.Solution)
            {
                return _resultArea;
            }
            else
            {
                return _sourceArea;
            }
        }

        private static void ClearArea(Transform area)
        {
            foreach (Transform child in area)
            {
                Destroy(child.gameObject);
            }
        }

    }
}