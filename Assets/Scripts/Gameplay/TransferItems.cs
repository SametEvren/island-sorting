﻿using System.Collections.Generic;
using Gameplay.Islands;
using Gameplay.Slots;
using Gameplay.SortableItems;
using Managers;
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

            MovementManager.instance.lastMovedGroup.Clear();
            for (var i = 0; i < availableSlotCount; i++)
            {
                float delay = i * MoveDelay;
                bool isLastManMoving = false || i == availableSlotCount - 1;
                
                MovementManager.instance.lastMovedGroup.Add(slotsToMove[i].ItemOnSlot.transform);
                slotsToMove[i].ItemOnSlot.GetComponent<SortableMovement>().MoveToIsland(targetIsland, path, delay, lineRenderer, isLastManMoving);
            }
        }
    }
}
