using System;
using UnityEditor;

[Serializable]
public class GameData
{
    private bool _isAllLevelsCompleted = false;
    private int _currentLevel = 1;
    private int _amountLevels = 1;

    private int _currentVladSkinNum = 0;
    private int _currentGlentSkinNum = 0;
    
    public int CurrentLevel => 
        _currentLevel;

    public int AmountLevels => 
        _amountLevels;

    public bool AllLevelsCompleted() =>
        _isAllLevelsCompleted;

    public GameData(GameStaticData gameStaticData)
    {
        _currentLevel = gameStaticData.ReachedLevel;
        _amountLevels = gameStaticData.AmountLevels;
    }

    public void SetNewLevel()
    {
        if (_currentLevel < _amountLevels)
            _currentLevel++;
        else
            _isAllLevelsCompleted = true;

    }

    public void SetVladSkin(int num) => 
        _currentVladSkinNum = num;
    
    public void SetGlentSkin(int num) => 
        _currentGlentSkinNum = num;

    public int SkinNum(SkinTypeID skinTypeID)
    {
        switch (skinTypeID)
        {
            case (SkinTypeID.Vlad):
                return _currentVladSkinNum;
            case (SkinTypeID.Glent):
                return _currentGlentSkinNum;
        }

        return -1;
    }
}