using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI _fpsText;
    private float _deltaTime = 0.0f;

    private void Start()
    {
        _fpsText = GetComponent<TextMeshProUGUI>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
        _fpsText.text = Mathf.RoundToInt(fps).ToString();
    }
}