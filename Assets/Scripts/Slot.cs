using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private SortableItem itemOnSlot;
    public SortableItem ItemOnSlot => itemOnSlot;

    public void SetItemOnSlot(SortableItem newItem) => itemOnSlot = newItem;
    
}

