using System.Collections.Generic;
using Gameplay.Islands;
using Gameplay.Slots;
using Gameplay.SortableItems;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using Utility;

namespace Gameplay
{
    public static class TransferItems
    {
        private const float MoveDelay = 0.2f;
        public static void MoveItems(Island sourceIsland, Island targetIsland, List<Transform> path, LineRenderer lineRenderer)
        {
            List<Slot> slotsToMove = new List<Slot>();

            var colorToMove = SortingColor.Blank;

            for (var i = sourceIsland.Slots.Count - 1; i >= 0; i--)
            {
                var slot = sourceIsland.Slots[i];

                if (slot.IsEmpty)
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

            var colorOfTargetIsland = IslandHelper.FindColorOfTargetIsland(targetIsland);

            if (colorOfTargetIsland != colorToMove && colorOfTargetIsland != SortingColor.Blank)
            {
                slotsToMove.Clear();
                return;
            }

            var availableSlotCount = Mathf.Min(slotsToMove.Count, targetIsland.EmptySlotCount);

            var movementManager = MovementManager.instance;
            // if(movementManager.lastMovedGroupListOfLists.movedGroups.Count > 0)
            //     movementManager.lastMovedGroupListOfLists.movedGroups[movementManager.storedCount].transformList.Clear();
            
            movementManager.currentGroup.transformList.Clear();
            for (var i = 0; i < availableSlotCount; i++)
            {
                float delay = i * MoveDelay;
                bool isLastManMoving = false || i == availableSlotCount - 1;
                
                movementManager.currentGroup.transformList.Add(slotsToMove[i].ItemOnSlot.transform);
                //movementManager.lastMovedGroupListOfLists.movedGroups[movementManager.storedCount].transformList.Add(slotsToMove[i].ItemOnSlot.transform);
                slotsToMove[i].ItemOnSlot.GetComponent<SortableMovement>().MoveToIsland(targetIsland, path, delay, lineRenderer, isLastManMoving);
            }

            if (movementManager.lastMovedGroupListOfLists.movedGroups.Count == 5)
            {
                movementManager.lastMovedGroupListOfLists.movedGroups.RemoveAt(0);
            }

            MovedGroupsTransforms newTr = new MovedGroupsTransforms()
            {
                transformList = new List<Transform>(movementManager.currentGroup.transformList)
            };
            
            movementManager.lastMovedGroupListOfLists.movedGroups.Add(newTr);
            
            if (movementManager.storedCount != 5)
                movementManager.storedCount++;
        }
    }
}
