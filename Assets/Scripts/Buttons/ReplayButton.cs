using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class ReplayButton : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    private Button _replayButton;
    private LevelRunner _levelRunner;

    [Inject]
    public void Construct(SceneLoader sceneLoader, LevelRunner levelRunner)
    {
        _levelRunner = levelRunner;
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        _replayButton = GetComponent<Button>();
    }

    private void OnEnable() => 
        _replayButton.onClick.AddListener(OnReplayButtonClick);

    private void OnDisable() => 
        _replayButton.onClick.RemoveListener(OnReplayButtonClick);

    private void OnReplayButtonClick()
    {
        _levelRunner.OnGameReplay();
    }
}
