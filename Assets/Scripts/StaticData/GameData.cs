using System;

[Serializable]
public class GameData
{
    private int _currentLevel = 1;
    private int _amountLevels = 1;
    
    public int CurrentLevel => _currentLevel;
    public int AmountLevels => _amountLevels;

    public GameData(GameStaticData gameStaticData)
    {
        _currentLevel = gameStaticData.ReachedLevel;
        _amountLevels = gameStaticData.AmountLevels;
    }

    public void SetNewLevel() => 
        _currentLevel++;
    
    
}