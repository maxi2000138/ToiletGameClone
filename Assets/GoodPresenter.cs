using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodPresenter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    
    [SerializeField]
    private Image _image;
    
    public void Construct(Sprite sprite, string name)
    {
        _text.text = name;
        _image.sprite = sprite;
    }
}
