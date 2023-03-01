using System;
using Gameplay.SortableItems;
using UnityEngine;

namespace Gameplay.Slots
{
    public class Slot : MonoBehaviour
    {
        public SortableItem itemOnSlot;
        public SortableItem ItemOnSlot => itemOnSlot;
        public bool IsEmpty => ItemOnSlot is null;
        public event Action OnItemChanged;


        public void SetItemOnSlot(SortableItem newItem)
        {
            itemOnSlot = newItem;
            OnItemChanged?.Invoke();
        }

        public void ClearSlot()
        {
            itemOnSlot = null;
            OnItemChanged?.Invoke();
        }

    }
}

