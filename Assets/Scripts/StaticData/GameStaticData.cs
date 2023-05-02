using UnityEngine;

[CreateAssetMenu(fileName = "NewGameStaticData", menuName = "Game Static Data/Create")]
public class GameStaticData : ScriptableObject
{
    public int ReachedLevel = 1;
    public int AmountLevels = 1;
}
