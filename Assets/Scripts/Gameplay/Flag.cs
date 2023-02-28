using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class Flag : MonoBehaviour
    {
        public MeshRenderer flag;
        private void OnEnable()
        {
            transform.DOLocalMoveY(0, 1);
        }
    }
}
