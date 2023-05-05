using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NextLevelButton : MonoBehaviour
{
    private Button _nextLevelButton;
    private SceneLevelChanger _sceneLevelChanger;

    [SerializeField]
    private GameObject _loadingScreen;

    private GameData _gameData;
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLevelChanger sceneLevelChanger, GameData gameData, SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        _gameData = gameData;
        _sceneLevelChanger = sceneLevelChanger;
    }

    private void Awake() => 
        _nextLevelButton = GetComponent<Button>();

    private void OnEnable() => 
        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    
    private void OnDisable() => 
        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClick);
    
    public void OnNextLevelButtonClick()
    {
        if(_gameData.AllLevelsCompleted())
            _sceneLoader.Load("Menu");
        else
            _sceneLevelChanger.LoadCurrentLevel(_loadingScreen);
    }
}
