using UnityEngine.InputSystem;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    public CoroutineRunner CoroutineRunner;

    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().AsSingle();
        Container.Bind<ICoroutineRunner>().FromComponentInNewPrefab(CoroutineRunner).AsSingle();
        Container.Bind<SceneLoader>().AsSingle();
        Container.Bind<GameData>().AsSingle();
        Container.Bind<SceneLevelChanger>().AsSingle();
    }
}
