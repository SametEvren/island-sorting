using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortablePerson : SortableItem
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    private const float Speed = 3;

    private void Start()
    {
        UpdateMaterialColor();
    }

    public override void SetSortingColor(SortingColor newColor)
    {
        base.SetSortingColor(newColor);
        ChangeCharacterColor(ColorDictionary.GetColor(newColor));
    }

    public override void MoveToIsland(Island island, List<Transform> pathPoints, float delay, LineRenderer lineRenderer, bool isLastManMoving)
    {
        if (island.emptySlots.Count == 0)
            return;
        var targetSlot = island.emptySlots[0];
        island.emptySlots.RemoveAt(0);
        targetSlot.SetItemOnSlot(this);
        GetComponentInParent<Slot>().ClearSlot();
        
        StartCoroutine(FollowPath(pathPoints, targetSlot,delay,lineRenderer,isLastManMoving));
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


    private void ChangeCharacterColor(Color newColor)
    {
        var newMaterial = new Material(skinnedMeshRenderer.material);
        newMaterial.color = newColor;
        skinnedMeshRenderer.material = newMaterial;
    }
    
    private void UpdateMaterialColor()
    {
        var correspondingColor = ColorDictionary.GetColor(SortingColor);
        var currentMaterialColor = skinnedMeshRenderer.material.color;
        
        if (currentMaterialColor != correspondingColor)
            ChangeCharacterColor(correspondingColor);
    }
}
