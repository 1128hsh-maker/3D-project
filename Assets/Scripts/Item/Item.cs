using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class Item : ScriptableObject
{ 
    [Header("Info")]
    public string displayName;
    public string description; 
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Equip")]
    public GameObject equipPrefab;
}
