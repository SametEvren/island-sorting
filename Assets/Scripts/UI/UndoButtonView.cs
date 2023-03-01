using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UndoButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI remainingText;
        [SerializeField] private Button button;

        public void Render(int remainingAmount)
        {
            remainingText.SetText(remainingAmount.ToString());
        }

        public void Disable()
        {
            button.interactable = false;
        }
    }
}