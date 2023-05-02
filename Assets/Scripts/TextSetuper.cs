using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class TextSetuper : MonoBehaviour
{
    private GameData _gameData;
    
    [SerializeField] 
   private TextMeshProUGUI _levelText;

   [Inject]
   public void Construct(GameData gameData)
   {
       _gameData = gameData;
   }

   public void SetupLevelText() => 
      _levelText.text = $"{_gameData.CurrentLevel}/{_gameData.AmountLevels}";
}
