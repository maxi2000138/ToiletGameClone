using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnterShopScene : MonoBehaviour
{
    private Button _button;
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void LoadShop() => 
        _sceneLoader.Load("Shop");

    private void OnEnable() => 
        _button.onClick.AddListener(LoadShop);
    
    private void OnDisable() => 
        _button.onClick.RemoveListener(LoadShop);
    
    
}
