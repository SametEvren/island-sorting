using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class Flag : MonoBehaviour
    {
        public MeshRenderer flag;
        private void OnEnable()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -1, transform.localPosition.z);
            transform.DOLocalMoveY(0, 1);
        }
    }
}
