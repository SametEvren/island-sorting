using TMPro;
using UnityEngine;

namespace UI
{
    public class UndoButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI remainingText;

        public void Render(int remainingAmount)
        {
            remainingText.SetText(remainingAmount.ToString());
        }
    }
}