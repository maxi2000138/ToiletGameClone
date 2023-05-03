using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NextLevelButton : MonoBehaviour
{
    private Button _nextLevelButton;
    private SceneLevelChanger _sceneLevelChanger;

    [Inject]
    public void Construct(SceneLevelChanger sceneLevelChanger)
    {
        _sceneLevelChanger = sceneLevelChanger;
    }

    private void Awake() => 
        _nextLevelButton = GetComponent<Button>();

    private void OnEnable() => 
        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    
    private void OnDisable() => 
        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClick);
    
    public void OnNextLevelButtonClick() => 
        _sceneLevelChanger.LoadCurrentLevel();
}
