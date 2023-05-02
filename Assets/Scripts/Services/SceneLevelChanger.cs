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

    public void ChangeLevelAndLoad()
    {
        _gameData.SetNewLevel();
        LoadLevel();
    }

    public void LoadCurrentLevel()
    {
        LoadLevel();
    }

    private void LoadLevel() => 
        _sceneLoader.Load(_levelSceneKey + _gameData.CurrentLevel);
}
