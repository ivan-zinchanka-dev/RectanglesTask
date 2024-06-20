using System;
using UnityEngine;

namespace Controls
{
    public class InputManager : MonoBehaviour
    {
        private const string ResolveActionName = "Resolve";
        private const string LeftExampleActionName = "ToLeftExample";
        private const string RightExampleActionName = "ToRightExample";

        public event Action Resolve;
        public event Action<int> SwitchBlueprint;
        
        private void Update()
        {
            if (Input.GetButtonDown(ResolveActionName))
            {
                Resolve?.Invoke();
            }
            else if (Input.GetButtonDown(LeftExampleActionName))
            {
                SwitchBlueprint?.Invoke(-1);
            }
            else if (Input.GetButtonDown(RightExampleActionName))
            {
                SwitchBlueprint?.Invoke(1);
            }
        }
    }
}