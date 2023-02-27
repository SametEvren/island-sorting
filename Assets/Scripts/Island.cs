using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class Island : MonoBehaviour
{
    private const uint RowLength = 4;
    [SerializeField] private List<Slot> slots;
    [SerializeField] private Transform accessPoint;
    [SerializeField] private Transform islandBorder; 
    private bool _isComplete;
    
    public List<Slot> Slots => slots;
    public Transform AccessPoint => accessPoint;
    public Transform IslandBorder => islandBorder;
    public bool IsComplete => _isComplete;
    public List<Slot> emptySlots = new();
    public int EmptySlotCount => Slots.Count(slot => slot.IsEmpty);

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
            return;
        }

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
    
    public void TransferItems(Island targetIsland, List<Transform> path, LineRenderer lineRenderer)
    {
        List<Slot> slotsToMove = new();

        var colorToMove = SortingColor.Blank;
        
        for (var i = Slots.Count - 1; i >= 0; i--)
        {
            var slot = Slots[i];
            
            if(slot.IsEmpty)
                continue;
            
            if (colorToMove == SortingColor.Blank)
            {
                colorToMove = slot.ItemOnSlot.SortingColor;
                slotsToMove.Add(slot);
                continue;
            }

            if (colorToMove != slot.ItemOnSlot.SortingColor)
                break;
            
            slotsToMove.Add(slot);
        }

        var colorOfTargetIsland = FindColorOfTargetIsland(targetIsland);

        if (colorOfTargetIsland != colorToMove && colorOfTargetIsland != SortingColor.Blank)
        {
            slotsToMove.Clear();
            return;
        }
        var availableSlotCount = Math.Min(slotsToMove.Count, targetIsland.EmptySlotCount);
        
        for (var i = 0; i < availableSlotCount; i++)
        {
            float delay = i / 5f;
            bool islastManMoving = false || i == availableSlotCount - 1;
            
            slotsToMove[i].ItemOnSlot.MoveToIsland(targetIsland,path,delay,lineRenderer, islastManMoving);
        }
    }

    public SortingColor FindColorOfTargetIsland(Island targetIsland)
    {
        for (var i = targetIsland.Slots.Count - 1; i >= 0; i--)
        {
            var slot = targetIsland.Slots[i];
            
            if(slot.IsEmpty)
                continue;
            
            var targetColor = slot.ItemOnSlot.SortingColor;
            return targetColor;
        }
        return SortingColor.Blank;
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

[Serializable]
public class SlotRow
{ 
    public List<Slot> slots;
}
