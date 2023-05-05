using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSkinData", menuName = "SkinData/Create")]             
public class SkinData : ScriptableObject
{
    public int ID;
    public Sprite Image;
    public String Name;
    public GameObject Prefab;
}
