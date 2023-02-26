using System.Collections.Generic;
using UnityEngine;

public class SortablePerson : SortableItem
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        UpdateMaterialColor();
    }

    public override void SetSortingColor(SortingColor newColor)
    {
        base.SetSortingColor(newColor);
        ChangeCharacterColor(ColorDictionary.GetColor(newColor));
    }

    public override void MoveToSlot(Slot slot, List<Transform> pathPoints)
    {
        
    }

    private void ChangeCharacterColor(Color newColor)
    {
        var newMaterial = new Material(skinnedMeshRenderer.material);
        newMaterial.color = newColor;
        skinnedMeshRenderer.material = newMaterial;
    }
    
    private void UpdateMaterialColor()
    {
        var correspondingColor = ColorDictionary.GetColor(SortingColor);
        var currentMaterialColor = skinnedMeshRenderer.material.color;
        
        if (currentMaterialColor != correspondingColor)
            ChangeCharacterColor(correspondingColor);
    }
}
