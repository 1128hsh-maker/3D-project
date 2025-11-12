using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    public TextMeshProUGUI itemName;
    
    
    void Awake()
    {
        instance = this;
    }
}
