using TMPro;
using UnityEngine;

public class SetLevelText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _levelText;

    public void SetText(string text)
    {
        _levelText.text = text;
    }
}
