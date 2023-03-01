using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class QuestionMarkAnimation : MonoBehaviour
    {
        void Start()
        {
            transform.DOLocalRotate(new Vector3(0, 360, 0), 3f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
            transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1.5f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
