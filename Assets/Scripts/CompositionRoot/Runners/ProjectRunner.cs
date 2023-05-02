using UnityEngine;
using Zenject;

public class ProjectRunner : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        _sceneLoader.Load("Menu");
    }
}