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

        public MovedGroupsTransforms currentGroup = new();
        public MovedGroupsTransformsLists lastMovedGroupListOfLists = new MovedGroupsTransformsLists();
        public int storedCount;

        public void UndoMovement()
        {
            // foreach (var movedGroupsTransforms in lastMovedGroup.listOfList)
            // {
            //     for (int i = 0; i < movedGroupsTransforms.transformList.Count; i++)
            //     {
            //         movedGroupsTransforms.transformList[i].GetComponent<SortableMovement>().Undo();
            //     }
            // }
            if (storedCount <= 0)
                return;
            for (int i = 0; i < lastMovedGroupListOfLists.movedGroups[storedCount-1].transformList.Count; i++)
            {
                lastMovedGroupListOfLists.movedGroups[storedCount-1].transformList[i].GetComponent<SortableMovement>().Undo();
            }

            lastMovedGroupListOfLists.movedGroups.RemoveAt(storedCount - 1);
            storedCount--;

        }
    }
    
    [System.Serializable]
    public class MovedGroupsTransforms
    {
        public List<Transform> transformList;
    }
 
    [System.Serializable]
    public class MovedGroupsTransformsLists
    {
        public List<MovedGroupsTransforms> movedGroups;
    }
    
}
