using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class SortableItem : MonoBehaviour
{
    [SerializeField] private SortingColor sortingColor;
    public SortingColor SortingColor => sortingColor;

    public virtual void SetSortingColor(SortingColor newColor) => sortingColor = newColor;

    public abstract void MoveToIsland(Island island, List<Transform> pathPoints, float delay, LineRenderer lineRenderer, bool isLastManMoving);
}
