using System.Net.NetworkInformation;
using UnityEngine;
using Zenject;

public class LocatorInstaller : MonoInstaller
{
    [SerializeField]
    private LevelRunner _levelRunner;

    public override void InstallBindings()
    {
        Container.Bind<LevelRunner>().FromInstance(_levelRunner).AsSingle();
    }
}
