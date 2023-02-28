using Utility;

namespace Gameplay.Islands
{
    public static class IslandHelper
    {
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