using System;
using DG.Tweening;
using UnityEngine;

public class AnimateLearnTaskPanelText : MonoBehaviour
{
    public Transform _textObject;
    private Sequence _sequence;

    private void OnEnable()
    {
        _sequence = DOTween.Sequence()
            .Append(_textObject.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 1).SetEase(Ease.InQuad))
            .Append(_textObject.DOScale(new Vector3(1, 1, 1), 1).SetEase(Ease.OutQuad))
            .SetLoops(-1);
    }

    private void OnDisable()
    {
        _sequence.Kill();
    }
}
