using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class IslandMovement : MonoBehaviour
{
    private const float UpYValue = 0.4f;
    private const float DownYValue = 0;
    private Sequence _movingSequence;
    private bool startingIsland;

    private void Start()
    {
        PathCreator.OnIslandSelected += MoveIsland;
    }

    public void MoveIsland(bool isMovingUp, Island island)
    {
        if (island != GetComponent<Island>() && startingIsland == false) return;

        startingIsland = isMovingUp;
        
        _movingSequence.Kill();
        _movingSequence = DOTween.Sequence(transform.DOMoveY(isMovingUp ? UpYValue : DownYValue, 0.5f));
    }
}
