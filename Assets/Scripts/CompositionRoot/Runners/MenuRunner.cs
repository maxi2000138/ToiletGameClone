using System;
using TMPro;
using UnityEngine;
using Zenject;

public class MenuRunner : MonoBehaviour
{
    [SerializeField]
    private TextSetuper _textSetuper;
    
    [Inject]
    public void Construct(TextSetuper textSetuper)
    {
        _textSetuper = textSetuper;
    }
    private void Awake()
    {
        _textSetuper.SetupLevelText();
    }
}
