using System;
using DG.Tweening;
using UnityEngine;

public class LearnHandMover : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    [SerializeField] private Transform _hand;

    private Sequence _sequence;

    private void OnEnable()
    {
        _sequence = DOTween.Sequence()
            .Append(_hand.DOMove(_endPoint.position, 2f).SetEase(Ease.Linear))
            .Append(_hand.DOMove(_startPoint.position,0f))
            .SetLoops(-1);
    }

    private void OnDisable()
    {
        _sequence.Kill();
    }
}
