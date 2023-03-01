using System;
using System.Linq;
using Gameplay.Islands;
using Gameplay.SortableItems;
using UnityEngine;

namespace Level
{
    public class LevelController : MonoBehaviour
    {
        public static event Action OnWin; 
        private Island[] _islands;
        private bool _isCompleted;

        private void Start()
        {
            GetIslands();
            SortableMovement.OnGroupMoved += CheckForWin;
        }

        private void GetIslands()
        {
            _islands = FindObjectsOfType<Island>();
        }

        private void CheckForWin()
        {
            if (_isCompleted) return;
            if (_islands.Any(island => !island.IsComplete && !island.IsEmpty)) return;
            
            _isCompleted = true;
            OnWin?.Invoke();
        }

        private void OnDestroy()
        {
            SortableMovement.OnGroupMoved -= CheckForWin;
        }
    }
}