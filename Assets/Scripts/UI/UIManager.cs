using DG.Tweening;
using Level;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UndoButtonView undoButtonView;
        [SerializeField] private CanvasGroup winScreen;
        [SerializeField] private Button restartButton;
        [SerializeField] private TextMeshProUGUI levelText;

        private void Start()
        {
            RenderUndo();
            RenderLevelText();
            MovementManager.OnUndo += RenderUndo;
            LevelController.OnWin += ShowWinScreen;
        }

        private void RenderLevelText()
        {
            levelText.SetText("Level " + (LevelLoader.Level + 1));
        }

        public void HandleRestartPressed()
        {
            LevelLoader.RestartLevel();
        }

        public void HandleNextPressed()
        {
            LevelLoader.NextLevel();    
        }
        
        public void ShowWinScreen()
        {
            undoButtonView.Disable();
            restartButton.interactable = false;
            RevealCanvasGroup(winScreen);
        } 
        
        private void RenderUndo()
        {
            undoButtonView.Render(MovementManager.instance.RemainingUndos);
        }
        
        private void RevealCanvasGroup(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1f, 1f).OnComplete(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }).SetDelay(1f);
        }
        
        private void OnDestroy()
        {
            MovementManager.OnUndo -= RenderUndo;
            LevelController.OnWin -= ShowWinScreen;
        }
    }
}
