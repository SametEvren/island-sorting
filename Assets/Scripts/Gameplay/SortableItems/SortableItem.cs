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
        public bool IsHidden { get; protected set; }
        
        public virtual void SetHidden(bool hidden)
        {
            IsHidden = hidden;
        }
        
        public virtual void SetSortingColor(SortingColor newColor) => sortingColor = newColor;
        
        
    }
}
