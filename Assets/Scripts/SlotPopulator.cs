using System;
using UnityEngine;

public class SlotPopulator : MonoBehaviour
{
    [SerializeField] private SortableItem itemPrefab;
    [SerializeField] private SortingColor itemColor = SortingColor.Blank;
    [SerializeField] private Slot slot;

    private void Start()
    {
        PopulateSlot();
    }

    public void PopulateSlot()
    {
        if (slot.ItemOnSlot is not null)
        {
            Destroy(slot.ItemOnSlot.gameObject);
            slot.SetItemOnSlot(null);
        }

        if (itemColor == SortingColor.Blank)
            return;
        
        var item = Instantiate(itemPrefab, transform.position, transform.parent.rotation, transform);
        item.SetSortingColor(itemColor);
        slot.SetItemOnSlot(item);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = ColorDictionary.GetColor(itemColor);
        
        Gizmos.DrawSphere(transform.position,0.03f);
    }
    
}