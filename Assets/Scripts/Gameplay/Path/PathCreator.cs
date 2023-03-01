using System;
using System.Collections.Generic;
using Gameplay.Islands;
using UnityEngine;
using Utility;

namespace Gameplay.Path
{
    public class PathCreator : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;
        public static event Action<bool,Island> OnIslandSelected;

        private Path _path = new();

        private void Start()
        {
            InputDetector.OnIslandTapped += HandleIslandTapped;
        }

        private void HandleIslandTapped(Island tappedIsland)
        {
            if (tappedIsland.IsComplete) return;

            if (_path.StartingIsland is null)
            {
                if (tappedIsland.IsEmpty)
                    return;

                _path.StartingIsland = tappedIsland;
                OnIslandSelected?.Invoke(true, tappedIsland);
                return;
            }

            if (_path.StartingIsland == tappedIsland)
            {
                OnIslandSelected?.Invoke(false, tappedIsland);
                _path.Reset();
                return;
            }

            
            if (tappedIsland.EmptySlotCount == 0)
            {
                OnIslandSelected?.Invoke(false, _path.StartingIsland);
                _path.Reset();
                return;
            }
            
            var areColorsDifferent = IslandHelper.FindColorOfTargetIsland(_path.StartingIsland) !=
                IslandHelper.FindColorOfTargetIsland(tappedIsland) && IslandHelper.FindColorOfTargetIsland(tappedIsland) != SortingColor.Blank;
            
            if (areColorsDifferent)
            {
                OnIslandSelected?.Invoke(false, tappedIsland);
                _path.Reset();
                return;
            }

            OnIslandSelected?.Invoke(false, tappedIsland);

            _path.TargetIsland = tappedIsland;
            var path = CalculatePath(_path.StartingIsland, _path.TargetIsland);
            var lineRenderer = Instantiate(line, Vector3.zero, Quaternion.identity, transform);
            DrawPath(path, lineRenderer);
            TransferItems.MoveItems(_path.StartingIsland, _path.TargetIsland, path, lineRenderer);
            _path.Reset();
        }

        private void DrawPath(List<Transform> path, LineRenderer lineRenderer)
        {
            lineRenderer.positionCount = path.Count;

            for (int i = 0; i < path.Count; i++)
            {
                lineRenderer.SetPosition(i, path[i].position);
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