using System;
using DG.Tweening;
using Gameplay;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UndoButtonView undoButtonView;
        [SerializeField] private CanvasGroup winScreen;

        private void Start()
        {
            RenderUndo();
            MovementManager.OnUndo += RenderUndo;
            LevelController.OnWin += ShowWinScreen;
        }

        public void RestartTheLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ShowWinScreen() => RevealCanvasGroup(winScreen);
        
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
            });
        }
        
        private void OnDestroy()
        {
            MovementManager.OnUndo -= RenderUndo;
            LevelController.OnWin -= ShowWinScreen;
        }
    }
}
