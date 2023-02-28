using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UndoButtonView undoButtonView;

        private void Start()
        {
            RenderUndo();
            MovementManager.OnUndo += RenderUndo;
        }

        private void RenderUndo()
        {
            undoButtonView.Render(MovementManager.instance.RemainingUndos);
        }

        public void RestartTheLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnDestroy()
        {
            MovementManager.OnUndo -= RenderUndo;
        }
    }
}
