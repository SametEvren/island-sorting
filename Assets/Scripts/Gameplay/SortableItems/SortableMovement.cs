using System.Collections;
using System.Collections.Generic;
using Gameplay.Islands;
using Gameplay.Slots;
using UnityEngine;

namespace Gameplay.SortableItems
{
    public class SortableMovement : MonoBehaviour
    {
        private const float Speed = 3;
        private Coroutine _followCoroutine;
        private Slot _currentSlot;
        private LineRenderer _lineRenderer;

        private void Start()
        {
            _currentSlot = GetComponentInParent<Slot>();
        }

        public void MoveToIsland(Island island, List<Transform> pathPoints, float delay, LineRenderer lineRenderer, bool isLastManMoving)
        {
            if (island.emptySlots.Count == 0)
                return;

            if (_followCoroutine != null)
            {
                Destroy(_lineRenderer.gameObject);
                StopCoroutine(_followCoroutine);
            }
        
            _lineRenderer = lineRenderer;
            _currentSlot.ClearSlot();
            var targetSlot = island.emptySlots[0];
            island.emptySlots.RemoveAt(0);
            GetComponent<SortablePerson>().SetItem(targetSlot);
            _currentSlot = targetSlot;
        
            _followCoroutine = StartCoroutine(FollowPath(pathPoints, targetSlot,delay,lineRenderer,isLastManMoving));
        }

        private IEnumerator FollowPath(List<Transform> pathPoints, Slot targetSlot, float delay, LineRenderer lineRenderer, bool isLastManMoving)
        {
            yield return new WaitForSeconds(delay);
            GetComponent<Animator>().SetBool("isRunning",true);
            int targetWaypointIndex = 1;
            Vector3 targetWaypoint = pathPoints[targetWaypointIndex].position;
            while (true)
            {
                if (transform.position == targetSlot.transform.position)
                {
                    if(isLastManMoving)
                        Destroy(lineRenderer);
                
                    GetComponent<Animator>().SetBool("isRunning",false);
                    transform.rotation = transform.parent.parent.rotation;
                    _lineRenderer = null;
                    _followCoroutine = null;
                    break;
                }

                Vector3 positionToLookAt = new Vector3(targetWaypoint.x, transform.position.y, targetWaypoint.z);
                transform.LookAt(positionToLookAt);
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, Speed * Time.deltaTime);
                if (transform.position == targetWaypoint)
                {
                    if(targetWaypointIndex == 1)
                        transform.parent = null;
                    if (targetWaypointIndex == 3)
                    {
                        targetWaypoint = targetSlot.transform.position;
                    }
                    else
                    {
                        targetWaypointIndex += 1;
                        targetWaypoint = pathPoints[targetWaypointIndex].position;
                        transform.parent = targetSlot.transform;
                    }
                }
                yield return null;
            }
        }
    }
}