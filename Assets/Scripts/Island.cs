using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Island : MonoBehaviour
{
    private const uint RowLength = 4;
    [SerializeField] private List<SlotRow> rows;
    [SerializeField] private Transform accessPoint;
    
    public List<SlotRow> Rows => rows;
    public Transform AccessPoint => accessPoint;

    public void OnValidate()
    {
        DoAssertions();
    }

    private void DoAssertions()
    {
        Assert.IsNotNull(AccessPoint);
        Assert.IsTrue(Rows.Count > 0,"Rows can not be empty.");

        if (Rows.Count <= 0) return;
        
        foreach (var row in Rows)
        {
            Assert.IsNotNull(row);
            Assert.IsTrue(row.slots.Count > 0, "Slots can not be empty in a row");
            
            if (row.slots.Count <= 0) continue;
            
            Assert.IsTrue(row.slots.Count == RowLength, $"Slot count should be equal to row length: {RowLength}");
                    
            foreach (var slot in row.slots)
            {
                Assert.IsNotNull(slot);
            }
        }
    }
}

[System.Serializable]
public class SlotRow
{ 
    public List<Slot> slots;
}
