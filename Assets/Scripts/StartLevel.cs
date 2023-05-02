using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartLevel : MonoBehaviour
{
    private Button _button;
    private SceneLevelChanger _sceneLevelChanger;

    [Inject]
    public void Construct(SceneLevelChanger sceneLevelChanger)
    {
        _sceneLevelChanger = sceneLevelChanger;
    }

    private void Awake() => 
        _button = GetComponent<Button>();

    private void OnEnable() => 
        _button.onClick.AddListener(LoadNewLvl);

    private void OnDisable() => 
        _button.onClick.RemoveListener(LoadNewLvl);
    
    public void LoadNewLvl() =>
        _sceneLevelChanger.LoadCurrentLevel();

}
