using System.Collections.Generic;
using System.Linq;
using Gameplay.Slots;
using UnityEngine;
using Utility;

namespace Gameplay.Islands
{
    public static class TransferItems
    {
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

            var colorOfTargetIsland = FindColorOfTargetIsland(targetIsland);

            if (colorOfTargetIsland != colorToMove && colorOfTargetIsland != SortingColor.Blank)
            {
                slotsToMove.Clear();
                return;
            }

            var availableSlotCount = Mathf.Min(slotsToMove.Count, targetIsland.EmptySlotCount);

            for (var i = 0; i < availableSlotCount; i++)
            {
                float delay = i / 5f;
                bool isLastManMoving = false || i == availableSlotCount - 1;

                slotsToMove[i].ItemOnSlot.MoveToIsland(targetIsland, path, delay, lineRenderer, isLastManMoving);
            }
        }

        public static SortingColor FindColorOfTargetIsland(Island targetIsland)
        {
            for (var i = targetIsland.Slots.Count - 1; i >= 0; i--)
            {
                var slot = targetIsland.Slots[i];

                if (slot.IsEmpty)
                    continue;

                var targetColor = slot.ItemOnSlot.SortingColor;
                return targetColor;
            }

            return SortingColor.Blank;
        }
    }
}
