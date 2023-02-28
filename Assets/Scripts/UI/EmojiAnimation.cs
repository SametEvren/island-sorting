using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class EmojiAnimation : MonoBehaviour
    {
        public float duration = 1f;
        public float scaleAmount = 1.2f;

        private void Start()
        {
            transform.DOScale(scaleAmount, duration)
                .SetEase(Ease.OutElastic).SetLoops(-1);
        }
    }
}
