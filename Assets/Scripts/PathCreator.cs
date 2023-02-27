using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    private Island _startingIsland, _targetIsland;
    
    
    private void Start()
    {
        InputDetector.OnIslandTapped += HandleIslandTapped;
    }

    private void HandleIslandTapped(Island tappedIsland)
    {
        if (tappedIsland.IsComplete) return;
        
        if (_startingIsland is null)
        {
            _startingIsland = tappedIsland;
            return;
        }
        
        if (_startingIsland == tappedIsland)
        {  
            ResetSelection();
            return;
        }
        
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
}