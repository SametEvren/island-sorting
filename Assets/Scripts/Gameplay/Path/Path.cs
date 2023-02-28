using Gameplay.Islands;

namespace Gameplay.Path
{
    public class Path
    {
        public Island StartingIsland { get; set; }
        public Island TargetIsland { get; set; }

        public void Reset()
        {
            StartingIsland = null;
            TargetIsland = null;
        }
    }
}