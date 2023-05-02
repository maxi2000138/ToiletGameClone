using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Game Data Installer/Create")]
public class DataInstaller : ScriptableObjectInstaller<DataInstaller>
{
    [SerializeField]
    private GameStaticData _gameStaticData;

    public override void InstallBindings()
    {
        Container.Bind<GameStaticData>().FromInstance(_gameStaticData).AsSingle();
    }
}
