using DG.Tweening;
using Gameplay.Path;
using Managers;
using UnityEngine;

namespace Gameplay.Islands
{
    public class IslandMovement : MonoBehaviour
    {
        private const float UpYValue = 0.4f;
        private const float DownYValue = 0;
        private Sequence _movingSequence;
        private bool _startingIsland;

        private void Start()
        {
            PathCreator.OnIslandSelected += MoveIsland;
            MovementManager.OnUndo += SnapIsland;
        }

        public void MoveIsland(bool isMovingUp, Island island)
        {
            if (island != GetComponent<Island>() && _startingIsland == false) return;

            _startingIsland = isMovingUp;
        
            _movingSequence.Kill();
            _movingSequence = DOTween.Sequence(transform.DOMoveY(isMovingUp ? UpYValue : DownYValue, 0.5f));
        }

        private void SnapIsland()
        {
            _movingSequence.Kill();
            transform.position = new Vector3(transform.position.x, DownYValue, transform.position.z);
        }
        
        private void OnDestroy()
        {
            PathCreator.OnIslandSelected -= MoveIsland;
            MovementManager.OnUndo -= SnapIsland;
        }
    }
}
