using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCollector : MonoBehaviour
{
    public List<Transform> noofSack = new List<Transform>();
    public Transform playerTargetTransform, factoryTransform;
    public Action<Transform> onCompleteCallback;
    [SerializeField] private float spacing;
    public float animationDuration = 0.5f;
    int row = 0;
    int column = 0;

    public Ease easeType = Ease.OutQuad;
    
    public void MoveSackToPlayer()
    {
        Sequence mySequence = DOTween.Sequence();
        foreach (Transform t in noofSack)
        {
            SetGarbageParent(t, playerTargetTransform);
            Vector3 newPos = GetPosition(row, column);
            column++;

            if (column > 3)
            {
                column = 0;
                row++;
            }
            mySequence.Append(t.DOMove(newPos, animationDuration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                onCompleteCallback?.Invoke(t);
            }));
        }
        SetGarbageParent(transform, playerTargetTransform);
        transform.GetComponent<BoxCollider>().enabled = false;
    }
    void SetGarbageParent(Transform transform, Transform target)
    {
        transform.SetParent(target);
    }

    private Vector3 GetPosition(int row, int column)
    {
        Vector3 startOffset = Vector3.zero;
        if (column < 3 && row <= 5)
        {
            startOffset = new Vector3(column * spacing, 0, -row * spacing);
        }
        return playerTargetTransform.position + startOffset;
    }
}
