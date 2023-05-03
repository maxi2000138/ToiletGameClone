using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPointsChanger : MonoBehaviour
{
    private TextMeshProUGUI _text;
    
    [SerializeField] 
    private float _deltaTime;


    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(pointsChanger());
    }

    private IEnumerator pointsChanger()
    {
        while (true)
        {
            ChangePoints(3);
            yield return new WaitForSeconds(_deltaTime);
            ChangePoints(2);
            yield return new WaitForSeconds(_deltaTime);
            ChangePoints(1);
            yield return new WaitForSeconds(_deltaTime);
        }
    }

    public string ChangePoints(int pointsAmount) => 
        _text.text = "Loading" + new string('.', pointsAmount);
}


