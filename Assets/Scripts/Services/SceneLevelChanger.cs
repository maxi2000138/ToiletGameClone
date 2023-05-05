using UnityEngine;

public class SceneLevelChanger
{
    private const string _levelSceneKey = "Level";
    private readonly SceneLoader _sceneLoader;
    private readonly GameData _gameData;


    public SceneLevelChanger(SceneLoader sceneLoader, GameData gameData)
    {
        _sceneLoader = sceneLoader;
        _gameData = gameData;
    }

    public void UpgradeLevel() => 
        _gameData.SetNewLevel();

    public void LoadCurrentLevel(GameObject loadingScreen = null)
    {
        LoadLevel(loadingScreen);
    }

    private void LoadLevel(GameObject loadingScreen = null)
    {
        if(loadingScreen != null)
            loadingScreen.SetActive(true);
        _sceneLoader.Load(_levelSceneKey + _gameData.CurrentLevel);
    }
}
