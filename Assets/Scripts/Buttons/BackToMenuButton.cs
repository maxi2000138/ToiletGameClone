using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackToMenuButton : MonoBehaviour
{
    private Button _backButton;
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private void Awake()
    {
        _backButton = GetComponent<Button>();
    }

    private void OnEnable() => 
        _backButton.onClick.AddListener(OnBackButtonClick);

    private void OnDisable() => 
        _backButton.onClick.AddListener(OnBackButtonClick);

    public void OnBackButtonClick() => 
        _sceneLoader.Load("Menu");
}
