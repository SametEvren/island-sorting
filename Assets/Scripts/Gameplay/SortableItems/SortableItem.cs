using System.Collections.Generic;
using Gameplay.Islands;
using UnityEngine;
using Utility;

namespace Gameplay.SortableItems
{
    public abstract class SortableItem : MonoBehaviour
    {
        [SerializeField] private SortingColor sortingColor;
        public SortingColor SortingColor => sortingColor;
        [SerializeField]private SortableMovement sortableMovement;
        public SortableMovement SortableMovement => sortableMovement;

        public virtual void SetSortingColor(SortingColor newColor) => sortingColor = newColor;
    }
}
