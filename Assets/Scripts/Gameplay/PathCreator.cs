using System;
using System.Collections.Generic;
using Gameplay.Islands;
using UnityEngine;
using Utility;

namespace Gameplay
{
    public class PathCreator : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;
        public Island startingIsland, targetIsland;
    
        public static event Action<bool,Island> OnIslandSelected;
    
        private void Start()
        {
            InputDetector.OnIslandTapped += HandleIslandTapped;
        }

        private void HandleIslandTapped(Island tappedIsland)
        {
            if (tappedIsland.IsComplete) return;
        
            if (startingIsland is null)
            {
                if (tappedIsland.emptySlots.Count == 16)
                    return;
            
                startingIsland = tappedIsland;
                OnIslandSelected?.Invoke(true, tappedIsland);
                return;
            }
        
            if (startingIsland == tappedIsland)
            {
                OnIslandSelected?.Invoke(false, tappedIsland);
                ResetSelection();
                return;
            }

            var areColorsDifferent = startingIsland.FindColorOfTargetIsland(startingIsland) !=
                tappedIsland.FindColorOfTargetIsland(tappedIsland) && tappedIsland.FindColorOfTargetIsland(tappedIsland) != SortingColor.Blank;
        
            if (areColorsDifferent)
            {
                OnIslandSelected?.Invoke(false, tappedIsland);
                ResetSelection();
                return;
            }

            OnIslandSelected?.Invoke(false, tappedIsland);

            targetIsland = tappedIsland;
            var path = CalculatePath(startingIsland, targetIsland);
            var lineRenderer = Instantiate(line, Vector3.zero, Quaternion.identity,transform);
            DrawPath(path, lineRenderer);
            startingIsland.TransferItems(targetIsland, path, lineRenderer);
            ResetSelection();
        }
        
        private void ResetSelection()
        {
            startingIsland = null;
            targetIsland = null;
        }
    
        private void DrawPath(List<Transform> path, LineRenderer lineRenderer)
        {
            lineRenderer.positionCount = path.Count;

            for (int i = 0; i < path.Count; i++)
            {
                lineRenderer.SetPosition(i,path[i].position);
            }
        }
        
        private List<Transform> CalculatePath(Island from, Island to)
        {
            return new List<Transform>
            {
                from.IslandBorder,
                from.AccessPoint,
                to.AccessPoint,
                to.IslandBorder
            };
        }

        private void OnDestroy()
        {
            InputDetector.OnIslandTapped -= HandleIslandTapped;
        }
    }
}