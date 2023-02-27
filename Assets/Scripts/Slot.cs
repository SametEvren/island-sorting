using System;
using UnityEngine;

public class Slot : MonoBehaviour
{

    private SortableItem itemOnSlot;
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

