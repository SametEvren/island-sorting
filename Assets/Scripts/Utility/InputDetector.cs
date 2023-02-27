using System;
using Gameplay.Islands;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utility
{
    public class InputDetector : MonoBehaviour
    {
        public static event Action<Island> OnIslandTapped;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            CheckIslandTapped();
        }

        private static void CheckIslandTapped()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit)) return;

            var hitObject = hit.transform;

            if (!hitObject.TryGetComponent(out Island island)) return;

            OnIslandTapped?.Invoke(island);
        }
    }
}