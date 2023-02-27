using System;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    public Island _startingIsland, _targetIsland;
    
    public static event Action<bool,Island> OnIslandSelected;
    
    private void Start()
    {
        InputDetector.OnIslandTapped += HandleIslandTapped;
    }

    private void HandleIslandTapped(Island tappedIsland)
    {
        if (tappedIsland.IsComplete) return;
        
        if (_startingIsland is null)
        {
            if (tappedIsland.emptySlots.Count == 16)
                return;
            
            _startingIsland = tappedIsland;
            OnIslandSelected(true,tappedIsland);
            return;
        }
        
        if (_startingIsland == tappedIsland)
        {  
            OnIslandSelected(false,tappedIsland);
            ResetSelection();
            return;
        }

        var areColorsDifferent = _startingIsland.FindColorOfTargetIsland(_startingIsland) !=
            tappedIsland.FindColorOfTargetIsland(tappedIsland) && tappedIsland.FindColorOfTargetIsland(tappedIsland) != SortingColor.Blank;
        
        if (areColorsDifferent)
        {
            OnIslandSelected(false,tappedIsland);
            ResetSelection();
            return;
        }
        
        OnIslandSelected(false,tappedIsland);

        _targetIsland = tappedIsland;
        var path = CalculatePath(_startingIsland, _targetIsland);
        var lineRenderer = Instantiate(line, Vector3.zero, Quaternion.identity,transform);
        DrawPath(path, lineRenderer);
        _startingIsland.TransferItems(_targetIsland, path, lineRenderer);
        ResetSelection();
    }

    

    private void ResetSelection()
    {
        _startingIsland = null;
        _targetIsland = null;
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