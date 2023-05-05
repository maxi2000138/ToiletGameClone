using System;
using UnityEngine;
using Zenject;

public class MenuRunner : MonoBehaviour
{
    [SerializeField]
    private TextSetuper _textSetuper;

    private StaticDataService _staticDataService;


    [Inject]
    public void Construct(TextSetuper textSetuper, StaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
        _textSetuper = textSetuper;
    }
    
    private void Awake()
    {
        _textSetuper.SetupLevelText();
    }

    private void Start()
    {
        _staticDataService.LoadGlentSkins();
        _staticDataService.LoadVladSkins();
    }
}
