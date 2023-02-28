using System.Collections.Generic;
using System.Linq;
using Gameplay.Slots;
using UnityEngine;
using UnityEngine.Assertions;
using Utility;


namespace Gameplay.Islands
{
    public class Island : MonoBehaviour
    {
        private const uint RowLength = 4;

        [SerializeField] private List<Slot> slots;
        [SerializeField] private Transform accessPoint;
        [SerializeField] private Transform islandBorder;
        [SerializeField] private Flag flagOfIsland;

        
        private bool _isComplete;
    
        public List<Slot> Slots => slots;
        public Transform AccessPoint => accessPoint;
        public Transform IslandBorder => islandBorder;
        public bool IsComplete => _isComplete;
        public List<Slot> emptySlots = new();
        public int EmptySlotCount => Slots.Count(slot => slot.IsEmpty);
        public bool IsEmpty => EmptySlotCount >= Slots.Count;

        public void OnValidate()
        {
            DoAssertions();
        }

        private void Start()
        {
            //These are for making path drawing more clear. No need to be child after the game starts.
            accessPoint.transform.parent = null;
            islandBorder.transform.parent = null;
        
            foreach (var slot in Slots)
            {
                slot.OnItemChanged += CheckIfComplete;
                slot.OnItemChanged += ChangeEmpty;
            
                if(slot.IsEmpty) emptySlots.Add(slot);
            }
        }

        private void CheckIfComplete()
        {
            if (Slots.Any(slot => slot.ItemOnSlot is null || slot.ItemOnSlot.SortingColor != Slots[0].ItemOnSlot.SortingColor))
            {
                _isComplete = false;
                flagOfIsland.gameObject.SetActive(false);
                return;
            }
            flagOfIsland.gameObject.SetActive(true);
            flagOfIsland.flag.material.color = ColorDictionary.GetColor(Slots[0].ItemOnSlot.SortingColor);
            _isComplete = true;
        }

        public void ChangeEmpty()
        {
            emptySlots.Clear();
            foreach (var slot in Slots)
            {
                if(slot.IsEmpty)
                    emptySlots.Add(slot);
            }
        }

        private void DoAssertions()
        {
            Assert.IsNotNull(AccessPoint);
            Assert.IsTrue(Slots.Count > 0,"Slots can not be empty.");

            if (Slots.Count <= 0) return;

            Assert.IsTrue(Slots.Count % RowLength == 0, $"Slot count should be dividable by row length: {RowLength}");
            foreach (var slot in Slots)
            {
                Assert.IsNotNull(slot); 
            }
        }
        
        private void OnDestroy()
        {
            foreach (var slot in Slots)
            {
                slot.OnItemChanged -= CheckIfComplete;
                slot.OnItemChanged -= ChangeEmpty;
            }
        }
    }
}
