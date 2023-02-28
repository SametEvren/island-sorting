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

        public List<Transform> lastMovedGroup;

        public void UndoMovement()
        {
            foreach (var character in lastMovedGroup)
            {
                character.GetComponent<SortableMovement>().Undo();
            }
        }
    }
    
}
