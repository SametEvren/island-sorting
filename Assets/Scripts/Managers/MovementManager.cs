using System;
using System.Collections.Generic;
using Gameplay.SortableItems;
using UnityEngine;

namespace Managers
{
    public class MovementManager : MonoBehaviour
    {
        #region Singleton
        public static MovementManager instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }
        #endregion

        public MovementInfo currentMovement = new();
        public List<MovementInfo> undoableMoves = new();
        public int RemainingUndos { get; private set; } = 5;
        
        private int UndoableMoveCount => undoableMoves.Count;
        public static event Action OnUndo;

        private void Start()
        {
            currentMovement.movements.Clear();
            undoableMoves.Clear();
        }

        public void UndoMovement()
        {
            if (UndoableMoveCount <= 0 || RemainingUndos <= 0)
                return;
            
            foreach (var sortableMovement in undoableMoves[^1].movements)
            {
                sortableMovement.Undo();
            }

            undoableMoves.RemoveAt(UndoableMoveCount - 1);
            RemainingUndos--;
            OnUndo?.Invoke();
        }
    }
    
    [System.Serializable]
    public class MovementInfo
    {
        public List<SortableMovement> movements;
    }
}
