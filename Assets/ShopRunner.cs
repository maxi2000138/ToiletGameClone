using System;
using UnityEngine;
using Zenject;

public class ShopRunner : MonoBehaviour
{
    private GameData _gameData;

    [SerializeField]
    private SnapScrolling _vladScrolling;

    [SerializeField]
    private SnapScrolling _glentScrolling;

    //private StaticDataService _staticDataService;

    [Inject]
    public void Construct(GameData gameData)
    {
        _gameData = gameData;
    }
    
    private void OnEnable()
    {
        _vladScrolling.OnSelectedGoodChanged += OnVladSkinChanged;
        _glentScrolling.OnSelectedGoodChanged += OnGlentSkinChanged;
    }
    
    
    private void OnDisable()
    {
        _vladScrolling.OnSelectedGoodChanged -= OnVladSkinChanged;
        _glentScrolling.OnSelectedGoodChanged -= OnGlentSkinChanged;
    }

    public void OnVladSkinChanged(int num)
    {
        _gameData.SetVladSkin(num);
    }
    
    public void OnGlentSkinChanged(int num)
    {
        _gameData.SetGlentSkin(num);
    }
    
    
}
