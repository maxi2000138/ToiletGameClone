using UnityEngine;
using UnityEngine.UI;

public class OnLearnPanelClick : MonoBehaviour
{
    [SerializeField]
    private GameObject _drawController;

    [SerializeField] 
    private Button _button;

    private void OnEnable() => 
        _button.onClick.AddListener(DisablePanelAndEnableDrawing);
    
    private void OnDisable() => 
        _button.onClick.RemoveListener(DisablePanelAndEnableDrawing);
    
    public void DisablePanelAndEnableDrawing()
    {
        _drawController.SetActive(true);
        gameObject.SetActive(false);
    }
}
