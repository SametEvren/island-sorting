using System.Collections.Generic;
using Gameplay.Islands;
using Gameplay.SortableItems;
using UnityEngine;
using UnityEngine.Assertions;
using Utility;

namespace Gameplay.Slots
{
    public class SlotPopulator : MonoBehaviour
    {
        [SerializeField] private Slot slot;
        
        [Header("Item Properties")]
        [SerializeField] private SortableItem itemPrefab;
        [SerializeField] private SortingColor itemColor = SortingColor.Blank;
        [SerializeField] private bool isHidden;
        
        [Header("Barricade")]
        [SerializeField] private List<GameObject> barricadePrefabs;
        [SerializeField] private bool isBarricade;
        
        private void OnValidate()
        {
            Assert.IsNotNull(slot);
            Assert.IsNotNull(itemPrefab);
            Assert.IsNotNull(barricadePrefabs);
        }
        
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

            if (isBarricade)
            {
                var choosenPrefab = barricadePrefabs[Random.Range(0, barricadePrefabs.Count)];
                var barricade = Instantiate(choosenPrefab, transform.position, transform.parent.rotation, transform.parent);
                slot.transform.parent.parent.GetComponent<Island>().Slots.Remove(slot);
                Destroy(slot.gameObject);
            }

            if (itemColor == SortingColor.Blank)
                return;
        
            var item = Instantiate(itemPrefab, transform.position, transform.parent.rotation, transform);
            item.SetSortingColor(itemColor);
            slot.SetItemOnSlot(item);
            slot.ItemOnSlot.SetHidden(isHidden);
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
}