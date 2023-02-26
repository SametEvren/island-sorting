using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class SortableItem : MonoBehaviour
{
    [SerializeField] private SortingColor sortingColor;
    public SortingColor SortingColor => sortingColor;

    public virtual void SetSortingColor(SortingColor newColor) => sortingColor = newColor;

    public abstract void MoveToSlot(Slot slot, List<Transform> pathPoints);
}
