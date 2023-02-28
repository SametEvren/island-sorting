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

        public virtual void SetSortingColor(SortingColor newColor) => sortingColor = newColor;
    }
}
