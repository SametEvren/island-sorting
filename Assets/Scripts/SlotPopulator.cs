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

    private void PopulateSlot()
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
        var color = !Application.isPlaying 
            ? itemColor  
            : slot.IsEmpty 
                ? SortingColor.Blank 
                : slot.ItemOnSlot.SortingColor;
        
        Gizmos.color = ColorDictionary.GetColor(color);
        
        Gizmos.DrawSphere(transform.position,0.03f);
    }
    
}