using Gameplay.Slots;
using UnityEngine;
using Utility;

namespace Gameplay.SortableItems
{
    public class SortablePerson : SortableItem
    {
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

        private void Start()
        {
            UpdateMaterialColor();
        }

        public void SetItem(Slot targetSlot)
        {
            targetSlot.SetItemOnSlot(this);
        }
        public override void SetSortingColor(SortingColor newColor)
        {
            base.SetSortingColor(newColor);
            ChangeCharacterColor(ColorDictionary.GetColor(newColor));
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
}
