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
        
        public void DrawBlueprint(Blueprint blueprint)
        {
            DrawPrimaryRectangle(blueprint);
            DrawSecondaryRectangles(blueprint);
        }

        public void ClearAllAreas()
        {
            ClearArea(_sourceArea);
            ClearArea(_resultArea);
        }

        public void ClearAreaFor(BlueprintType blueprintType)
        {
            ClearArea(GetArea(blueprintType));
        }

        private void DrawPrimaryRectangle(Blueprint blueprint)
        {
            Instantiate<RectangleView>(_primaryRectanglePrefab, GetArea(blueprint.Type), false)
                .Initialize(blueprint.PrimaryRectangle);
        }

        private void DrawSecondaryRectangles(Blueprint blueprint)
        {
            Transform area = GetArea(blueprint.Type);
            
            foreach (var rectangle in blueprint.SecondaryRectangles)
            {
                Instantiate<RectangleView>(_secondaryRectanglePrefab, area, false).Initialize(rectangle);
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